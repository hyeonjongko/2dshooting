using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

//Enum : 열거형 : 기억하기 어려운 상수들을 기억하기 쉬운 이름 하나로 묶어(그룹) 관리하는 표현 방식
enum EEnemyType
{
    Directional,
    Trace,
}

public class EnemyMove : MonoBehaviour
{
    public float Speed;
    public Vector2 Direction;

    public float TraceRange;
    public float Percent = 0.7f;

    //[Header("적 타입")]
    //public EEnemyType type;

    private GameObject _playerObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //캐싱
        // 1. 플레이어의 위치를 구한다
        //GameObject.Find("Player"); //추천하지 않는 방식
        _playerObject = GameObject.FindWithTag("Player");

        TraceRange = UnityEngine.Random.value;
    }

    // 목표 : 플레이어를 쫓아가는 적을 만들고 싶다.
    void Update()
    {

        if (TraceRange < Percent)
        {
            Move();
        }
        else
        {
            MoveTrace();
        }
           
        //0. Enemy 클래스 안에서 함수로 쪼개자
        //1. 함수가 너무 많아질 것 같다                -> 클래스로 쪼개는게 좋다
        //2. 쪼개고 나니깐 똑같은 기능/속성이 있네     -> 상속
        //3. 상속을 하자니 책임이 너무 크다            -> 조합
    }

    public void MoveTrace()
    {
        //Move();
        // 2. 위치에 따라 방향을 구한다
        Direction = _playerObject.transform.position - this.transform.position;
        // 3. 방향에 맞게 이동한다.
        transform.Translate(Direction * Speed * Time.deltaTime);
    }
    public void Move()
    {
        //아래로 내려가는 이동
        transform.Translate(Vector2.down * Speed * Time.deltaTime);
    }
}
