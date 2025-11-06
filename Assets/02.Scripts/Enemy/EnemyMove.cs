using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float Speed;
    public Vector2 Direction;
    private float _random = Random.Range(0f, 100f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // 목표 : 플레이어를 쫓아가는 적을 만들고 싶다.
    void Update()
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
}
