using UnityEngine;

public enum EEnemyType
{
    Directional,             // 0
    Trace,                   // 1
}

public class Enemy : MonoBehaviour
{
    [Header("스탯")]
    public float Speed;
    public float Damage = 1.0f;
    private float _health = 100.0f;
    
    public Vector2 Direction;

    [Header("적 타입")]
    public EEnemyType Type;

    //[Header("적 타입")]
    //public float TraceRange;
    //public float Percent = 0.7f;

    void Start()
    {
        Debug.Log(_health);
    }

    // 게임이 진행되고 있다는 이벤트
    void Update()
    {
        // 두가지 타입
        if (Type == EEnemyType.Directional)
        {
            Move();
        }
        else if (Type == EEnemyType.Trace)
        {
            MoveTrace();
        }

        // 0. 타입에 따라 동작이 다르네?              -> 함수로 쪼개자..
        // 1. 함수가 너무 많아질거 같네?  (OCP위반)    -> 클래스로 쪼개자..
        // 2. 쪼개고 나니까 똑같은 기능/속성이 있네     -> 상속
        // 3. 상속을 하자니 책임이 너무 크네(SRP위반)   -> 조합   

    }

    public void Move()
    {
        //아래로 내려가는 이동
        transform.Translate(Vector2.down * Speed * Time.deltaTime);
    }
    public void MoveTrace()
    {
        // 1. 플레이어의 위치를 구한다
        //GameObject.Find("Player"); //추천하지 않는 방식
        GameObject PlayerObject = GameObject.FindWithTag("Player");
        // 2. 위치에 따라 방향을 구한다
        Direction = PlayerObject.transform.position - this.transform.position;
        // 3. 방향에 맞게 이동한다.
        transform.Translate(Direction * Speed * Time.deltaTime);
    }

    public void Hit(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 몬스터는 플레이어와만 충돌처리할 것이다.
        if (!other.gameObject.CompareTag("Player")) return;

        Player player = other.gameObject.GetComponent<Player>();
        if (player == null) return;

        player.Hit(Damage);

        Destroy(gameObject);    // 나죽자.

        //if (other.CompareTag("Player") == false) return;

        //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        ////foreach 문은 컬렉션의 첫 번째 요소부터 시작하여, 컬렉션의 각 요소를 순차적으로 변수명에 할당하고,
        ////반복문의 본문을 실행합니다.
        ////이 과정은 컬렉션의 모든 요소가 처리될 때까지 계속됩니다.
        ////foreach 문은 내부적으로 컬렉션의 GetEnumerator 메서드를 호출하여 순회를 수행합니다.
        //foreach (GameObject enemy in enemies) //foreach (타입 변수명 in 컬렉션명)
        //{
        //    Destroy(enemy); //변수명을 사용한 작업 수행
        //}
    }
        //    Debug.Log("충돌 시작!");

        //    //몬스터는 플레이어만 죽인다.
        //    // 좋지 않은 코드(오브젝트 이름을 바꿀 수 있기 때문에)
        //    //tag로 비교하는 것이 좋다.
        //    //if(other.gameObject.name == "Player") //충돌체를 가지고 있는 오브젝트의 이름이 "Player"일때만
        //    //if (other.gameObject.tag == "Player")
        //    if (!other.gameObject.CompareTag("Player")) return;//문자열을 오타를 내면 오류를 띄워준다.

        //    //여기서 게임오브젝트는 스크립트를 가지고 있는 오브젝트를 의미한다.
        //    Destroy(this.gameObject);
        //    //여기서 게임오브젝트는 부딪힌 오브젝트를 의미한다.
        //    Destroy(other.gameObject);
        //}
        //private void OnTriggerStay2D(Collider2D other)
        //{
        //    Debug.Log("충돌중");
        //}
        //private void OnTriggerExit2D(Collider2D other)
        //{
        //    Debug.Log("충돌 끝");
        
    }
