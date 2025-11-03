using UnityEngine;

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
    public float MaxX =  2;
    public float MinY = -5;
    public float MaxY =  0;

    public float LeftEnd = (float)-2.9;
    public float RightEnd = (float)2.9;
    public float TopEnd = (float)0.5;
    public float BottomEnd = (float)-5.5;

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

        // 2. 입력으로부터 방향을 구한다.
        // 벡터 : 크기와 방향을 표현하는 물리 개념
        Vector2 direction = new Vector2(h, v);

        // 방향을 크기 1로 만드는 정규화를 한다.
        direction.Normalize();
        //direction = direction.normalized; // 방법은 위와 아래의 방법 2가지가 있다.

        Debug.Log($"direction : {direction.x},{direction.y}");

        // 3. 그 방향으로 이동한다.
        Vector2 position = this.transform.position; // 현재 위치

        Vector2 distance = direction * Speed * Time.deltaTime;

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

        // 1-1, 포지션 값에 제한을 둔다.
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

        // 2. 스피드 조작
        if(Input.GetKey("q"))
        {
            Speed += 1;
        }
        else if(Input.GetKey("e"))
        {
            Speed -= 1;
        }

        // 3. 양쪽 끝으로 가면 반대쪽 끝에서 다시 나오게
        if (newPosition.x < LeftEnd)
        {
            newPosition.x = RightEnd;
        }
        else if(newPosition.x > RightEnd)
        {
            newPosition.x = LeftEnd;
        }
        if(newPosition.y > TopEnd)
        {
            newPosition.y = BottomEnd;
        }
        else if(newPosition.y < BottomEnd)
        {
            newPosition.y = TopEnd;
        }

        transform.position = newPosition;                   // 새로운 위치로 갱신
    }
}
