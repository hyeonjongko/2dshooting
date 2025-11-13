using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

//플레이어 이동
public class PlayerMove : MonoBehaviour
{
    PlayerFire _playerFire;

    private Animator _animator;
    Enemy _enemy;

    //목표
    //"키보드 입력"에 따라 "방향"을 구하고 그 방향으로 이동시키고 싶다

    // 구현 순서 : 
    // 1. 키보드 입력
    // 2. 방향 구하는 방법
    // 3. 이동

    [Header("능력치")]
    private float _speed = 3;

    //public float _speed => _speed; //get,set 문법

    [Header("이동범위")]
    public float MinX = -2;
    public float MaxX = 2;
    public float MinY = -5;
    public float MaxY = 0;

    public float LeftEnd = -2.9f;
    public float RightEnd = 2.9f;
    public float TopEnd = 0.5f;
    public float BottomEnd = -5.5f;

    [Header("속도")]
    public float MaxSpeed = 10;
    public float MinSpeed = 1;

    public float Run = 5;

    [Header("시작위치")]
    private Vector2 _origin; //은닉화

    [Header("카운트")]
    private int Count = 3;

    [Header("자동 모드")]
    public bool AutoMode;
    public float time;
    public float attackRange = 0.2f;              // 적에게 접근할 거리
    public float safeDistanceX = 0.2f;             // 너무 가까워지면 회피 시작 거리
    public float safeDistanceY = 0.5f;
    public float evadeSpeedMultiplier = 1.5f;   // 회피 시 속도 배수
    public float neutralZone = 0.05f;

    private Transform EnemyPosition;            // 현재 추적 중인 적
    private float findInterval = 0.1f;          // 적 탐색 주기
    private float findTimer = 0f;               // 탐색 타이머


    [Header("자동 시야")]
    public float rotateSpeed = 5f;


    //게임 오브젝트가 게임을 시작할 때
    void Start()
    {
        _playerFire = GetComponent<PlayerFire>();

        _animator = GetComponent<Animator>();

       

        //처음 시작 위치 저장
        _origin = transform.position;
    }

    public void SpeedUp(int value)
    {
        _speed += value;
        //_speed = Mathf.Min(_speed, MaxSpeed);
    }

    // 게임 오브젝트가 게임을 시작 후 최대한 많이
    void Update()
    {
        if (_playerFire.auto == true)
        {
            AutoMove();
            AutoSight();
        }
        else
        {

            PassMode();
            AutoSight();
        }

        _speed = Mathf.Clamp(_speed, MinSpeed, MaxSpeed);

        if (Count == 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void AutoSight()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject nearestEnemy = null; // 가장 가까운 적 찾기
        float nearestDistance = Mathf.Infinity; //무한의 양수

        foreach (GameObject enemy in enemies)
        {
            _enemy = enemy.GetComponent<Enemy>();
            if (_enemy.Type == EEnemyType.Trace)
            {
                float distance = Vector2.Distance(enemy.transform.position, this.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestEnemy = enemy;
                }
            }
        }
        if (nearestEnemy != null)
        {
            Vector2 dir = nearestEnemy.transform.position - this.transform.position;
            //Mathf.Rad2Deg : 라디안을 도로 변환하는 상수
            //90을 빼주는 이유
            //-> 혹시 “플레이어가 반대로 뒤집히거나 180도 틀어지는” 현상이 있다면,
            //스프라이트 방향이 오른쪽이 아닌 왼쪽 기준일 가능성이 높습니다.
            //이럴 땐 angle 계산 부분에 보정값을 더하거나 빼면 됩니다.
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
            //오일러 각(x, y, z 회전 각도)을 입력받아 쿼터니언(Quaternion) 형태의 회전 값을 생성하는 함수
            this.transform.rotation = Quaternion.Euler(0f, 0f, angle);

            //Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
            //this.transform.rotation = Quaternion.Lerp(
            //    this.transform.rotation,
            //    targetRotation,
            //    Time.deltaTime * rotateSpeed
            //);
        }
        else
        {
            this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
    public void AutoMove()
    {
        time += Time.deltaTime;
        if (findTimer >= findInterval)
        {
            FindClosestEnemy();
            findTimer = 0f;
        }
        GameObject EnemyObject = GameObject.FindWithTag("Enemy");

        // 적이 없으면 아무 것도 안 함
        if (EnemyObject == null)
            return;

        // 적과 플레이어의 X축 거리 계산
        float directionX = (EnemyObject.transform.position.x - transform.position.x);
        float directionY = EnemyObject.transform.position.y - transform.position.y;
        float distanceX = Mathf.Abs(directionX); //math.Abs ->절대값
        float distanceY = Mathf.Abs(directionY);


        float moveDirX = 0f;

        bool _isEvading = false;

        if (distanceX < safeDistanceX && distanceY < safeDistanceY)
        {
            _isEvading = true;
            // 너무 가까움 → 반대 방향으로 회피
            moveDirX = -Mathf.Sign(directionX) * evadeSpeedMultiplier;

            // 회피 시 약간 랜덤성 추가 (멈추지 않게)
            //moveDirX += Random.Range(-0.2f, 0.2f);
        }
        else if (!_isEvading && distanceX > safeDistanceX && distanceX > attackRange)
        {
            moveDirX = Mathf.Sign(directionX);
        }

        Vector2 newPosition = transform.position;
        newPosition.x += moveDirX * _speed * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, MinX, MaxX);
        newPosition.y = Mathf.Clamp(newPosition.y, MinY, MaxY);



        transform.position = newPosition;
    }
    public void FindClosestEnemy()
    {
        //GameObject EnemyObject = GameObject.FindWithTag("Enemy");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); //Hierarchy

        float shortestDistance = Mathf.Infinity; // Mathf.Infinity => 양의 무한대를 나타낸다.
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position); //Vector2.Distance : 벡터 길이 구하기

            //새로 측정한 거리가 전에 기록된 거리보다 작으면 더 가까운 적을 찾음
            if (distance < shortestDistance) //양의 무한대라 첫 번째 적 무조건 만족
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
            if (nearestEnemy)
            {
                enemy.transform.position = nearestEnemy.transform.position;
            }
        }

        //else
        //{
        //    EnemyObject.transform.position = null;
        //}
    }
    public void PassMode()
    {
        // 1. 키보드 입력을 감지한다.
        // 유니티에서는 Input이라고 하는 모듈이 입력에 관한 모든 것을 담당한다
        //float h = Input.GetAxis("Horizontal");    // 수평 입력에 대한 값을 -1 ~ 1로 가져온다
        //float v = Input.GetAxis("Vertical");      // 수직 입력에 대한 값을 -1 ~ 1로 가져온다

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");


        //GetAxisRaw : -1, 0, 1의 값을 반환

        //Debug.Log($"h : {h}, v: {v}");

        // 실습 2. 스피드 조작

        //if(Input.GetKey("q"))
        //{
        //    _speed += 1;
        //}
        //else if(Input.GetKey("e"))
        //{
        //    _speed -= 1;
        //}

        //if (Input.GetKeyDown("q")) //GetKey : 누르고 있는 동안 계속 실행 / GetKeyDown : 한 번만 실행
        //{
        //    _speed++;
        //    if (_speed > MaxSpeed)
        //    {
        //        _speed = MaxSpeed;
        //    }
        //}
        //else if (Input.GetKeyDown("e"))
        //{
        //    _speed--;
        //    if (_speed < MinSpeed)
        //    {
        //        _speed = MinSpeed;
        //    }
        //}

        //_speed = Mathf.Max(MinSpeed, Mathf.Min(MaxSpeed, _speed));


        float RunSpeed = _speed;

        //실습 4번 : Shift키를 누르는 중에 이동속도가 1.2배 빨라지게
        if (Input.GetKey(KeyCode.LeftShift))
        {
            RunSpeed = RunSpeed * Run;
        }
        //else if (Input.GetKeyUp(KeyCode.LeftShift))
        //{
        //    _speed = _speed / Run;
        //}

        // 2. 입력으로부터 방향을 구한다.
        // 벡터 : 크기와 방향을 표현하는 물리 개념
        Vector2 direction = new Vector2(h, v);

        // 방향을 크기 1로 만드는 정규화를 한다.
        direction.Normalize();

        //첫 번째 방식 : Play 메서드를 이용한 강제 적용
        //if (direction.x < 0) _animator.Play("Left");
        //if (direction.x == 0) _animator.Play("Idle");
        //if (direction.x > 0) _animator.Play("Right");
        //장점 : 빠르게 사용하기 편하다.
        //이 방식의 단점은 Transition, Timing, State가 무시되고, 남요오디기 쉬워서 어디서 애니메이션을 수정하는 지 알 수 없어지게된다.


        // 두 번째 방식
        _animator.SetInteger("X", (int)direction.x);

        //direction = direction.normalized; // 방법은 위와 아래의 방법 2가지가 있다.

        //Debug.Log($"direction : {direction.x},{direction.y}");



        // 3. 그 방향으로 이동한다.
        Vector2 position = this.transform.position; // 현재 위치


        Vector2 distance = direction * RunSpeed * Time.deltaTime;


        //Translate 버전
        //transform.Translate(distanceX);

        // 새로운 위치 = 현재 위치 + (방향 * 속력) * 시간
        // 새로운 위치 = 현재 위치 + 속도 * 시간;
        //      새로운 위치 = 현재 위치 + 방향     * 속력
        //Vector2 newPosition = position + direction * _speed * Time.deltaTime; // 새로운 위치

        Vector2 newPosition = position + distance; // 새로운 위치

        // Time.deltaTime : 이전 프레임으로부터 현재 프레임까지 시간이 얼마나 흘렀는지를 나타내는 값
        //                  1초 / fps 값과 비슷하다.

        // 이동속도 : 10
        // 컴퓨터1  : 50FPS : Update   -> 초당 50번 실행    -> 10 * 50 = 500    * Time.deltaTime 
        // 컴퓨터2  : 100FPS : Update  -> 초당 100번 실행  -> 10 * 100 = 1000  * Time.deltaTime => 두개의 값이 같아진다.
        //1, -1, 0 이 숫자 3개 말고는 다 매직넘버이므로 변수로 빼야된다.

        // 실습 1-1, 포지션 값에 제한을 둔다.
        //if (newPosition.x < MinX)
        //{
        //    newPosition.x = MinX;
        //}
        //else if (newPosition.x > MaxX)
        //{
        //    newPosition.x = MaxX;
        //}
        //else if (newPosition.y < MinY)
        //{
        //    newPosition.y = MinY;
        //}
        //else if (newPosition.y > MaxY)
        //{
        //    newPosition.y = MaxY;
        //}

        // 실습 3. 양쪽 끝으로 가면 반대쪽 끝에서 다시 나오게
        if (newPosition.x < LeftEnd)
        {
            newPosition.x = RightEnd;
        }
        else if (newPosition.x > RightEnd)
        {
            newPosition.x = LeftEnd;
        }
        if (newPosition.y > TopEnd)
        {
            newPosition.y = BottomEnd;
        }
        else if (newPosition.y < BottomEnd)
        {
            newPosition.y = TopEnd;
        }


        transform.position = newPosition; // 새로운 위치로 갱신




        //실습 5. R키를 누르고 있으면 플레이어가 자동으로 원점으로 가게끔(Translate를 사용하세요.)
        if (Input.GetKey("r"))
        {
            TranslateToOrigin();
            //transform.Translate(-destination *_speed * Time.deltaTime );
        }
    }
    private void TranslateToOrigin()
    {
        Vector2 destination = _origin - (Vector2)transform.position;
        //destination.x = newPosition.x - _origin.x;
        //destination.y = newPosition.y - _origin.y;
        transform.Translate(destination * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Count--;
        }
    }
}
