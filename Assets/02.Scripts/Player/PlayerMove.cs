using UnityEngine;
using UnityEngine.UIElements;

//플레이어 이동
public class PlayerMove : MonoBehaviour
{

    //목표
    //"키보드 입력"에 따라 "방향"을 구하고 그 방향으로 이동시키고 싶다

    // 구현 순서 : 
    // 1. 키보드 입력
    // 2. 방향 구하는 방법
    // 3. 이동

    [Header("능력치")]
    public float Speed = 3;

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

    [Header("거리")]

    public Vector2 origin = new Vector2(0, 0);
    public Vector2 destination = new Vector2(0, 0);

    //게임 오브젝트가 게임을 시작할 때
    void Start()
    {

    }

    // 게임 오브젝트가 게임을 시작 후 최대한 많이
    void Update()
    {
        // 1. 키보드 입력을 감지한다.
        // 유니티에서는 Input이라고 하는 모듈이 입력에 관한 모든 것을 담당한다
        //float h = Input.GetAxis("Horizontal");    // 수평 입력에 대한 값을 -1 ~ 1로 가져온다
        //float v = Input.GetAxis("Vertical");      // 수직 입력에 대한 값을 -1 ~ 1로 가져온다

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        //GetAxisRaw : -1, 0, 1의 값을 반환

        Debug.Log($"h : {h}, v: {v}");

        // 실습 2. 스피드 조작

        //if(Input.GetKey("q"))
        //{
        //    Speed += 1;
        //}
        //else if(Input.GetKey("e"))
        //{
        //    Speed -= 1;
        //}

        if (Input.GetKeyDown("q")) //GetKey : 누르고 있는 동안 계속 실행 / GetKeyDown : 한 번만 실행
        {
            Speed++;
            //if(Speed > MaxSpeed)
            //{
            //    Speed = MaxSpeed;
            //}
        }
        else if (Input.GetKeyDown("e"))
        {
            Speed--;
            //if(Speed < MinSpeed)
            //{
            //    Speed = MinSpeed;
            //}
        }

        //Speed = Mathf.Max(MinSpeed, Mathf.Min(MaxSpeed, Speed));
        Speed = Mathf.Clamp(Speed, MinSpeed, MaxSpeed);

        float RunSpeed = Speed;

        //실습 4번 : Shift키를 누르는 중에 이동속도가 1.2배 빨라지게
        if (Input.GetKey(KeyCode.LeftShift))
        {
            RunSpeed = RunSpeed * Run;
        }
        //else if (Input.GetKeyUp(KeyCode.LeftShift))
        //{
        //    Speed = Speed / Run;
        //}

        // 2. 입력으로부터 방향을 구한다.
        // 벡터 : 크기와 방향을 표현하는 물리 개념
        Vector2 direction = new Vector2(h, v);

        // 방향을 크기 1로 만드는 정규화를 한다.
        direction.Normalize();
        //direction = direction.normalized; // 방법은 위와 아래의 방법 2가지가 있다.

        Debug.Log($"direction : {direction.x},{direction.y}");



        // 3. 그 방향으로 이동한다.
        Vector2 position = this.transform.position; // 현재 위치


        Vector2 distance = direction * RunSpeed * Time.deltaTime;


        //Translate 버전
        //transform.Translate(distance);

        // 새로운 위치 = 현재 위치 + (방향 * 속력) * 시간
        // 새로운 위치 = 현재 위치 + 속도 * 시간;
        //      새로운 위치 = 현재 위치 + 방향     * 속력
        //Vector2 newPosition = position + direction * Speed * Time.deltaTime; // 새로운 위치

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

        destination.x = newPosition.x - origin.x;
        destination.y = newPosition.y - origin.y;

        //실습 5. R키를 누르고 있으면 플레이어가 자동으로 원점으로 가게끔(Translate를 사용하세요.)
        if (Input.GetKey("r"))
        {
            transform.Translate(-destination *Speed * Time.deltaTime );
        }                        
    }
}
