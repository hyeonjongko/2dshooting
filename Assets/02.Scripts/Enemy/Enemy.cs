using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("스탯")]
    public float Damage = 1.0f;
    private float _health = 100.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(_health);
    }

    // 게임이 진행되고 있다는 이벤트
    void Update()
    {
        

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
        if (other.CompareTag("Player") == false) return;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //foreach 문은 컬렉션의 첫 번째 요소부터 시작하여, 컬렉션의 각 요소를 순차적으로 변수명에 할당하고,
        //반복문의 본문을 실행합니다.
        //이 과정은 컬렉션의 모든 요소가 처리될 때까지 계속됩니다.
        //foreach 문은 내부적으로 컬렉션의 GetEnumerator 메서드를 호출하여 순회를 수행합니다.
        foreach (GameObject enemy in enemies) //foreach (타입 변수명 in 컬렉션명)
        {
            Destroy(enemy); //변수명을 사용한 작업 수행
        }
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
