using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("스탯")]
    public float Speed;
    public float Health = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // 게임이 진행되고 있다는 이벤트
    void Update()
    {
        transform.Translate(Vector2.down * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("충돌 시작!");

        //몬스터는 플레이어만 죽인다.
        // 좋지 않은 코드(오브젝트 이름을 바꿀 수 있기 때문에)
        //tag로 비교하는 것이 좋다.
        //if(other.gameObject.name == "Player") //충돌체를 가지고 있는 오브젝트의 이름이 "Player"일때만
        //if (other.gameObject.tag == "Player")
        if (!other.gameObject.CompareTag("Player")) return;//문자열을 오타를 내면 오류를 띄워준다.

        //여기서 게임오브젝트는 스크립트를 가지고 있는 오브젝트를 의미한다.
        Destroy(this.gameObject);
        //여기서 게임오브젝트는 부딪힌 오브젝트를 의미한다.
        Destroy(other.gameObject);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("충돌중");
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("충돌 끝");
    }
}
