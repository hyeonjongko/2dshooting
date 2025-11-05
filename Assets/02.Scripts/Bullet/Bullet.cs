using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("이동속도")]
    public float StartSpeed = 1.0f;
    public float MaxSpeed = 7.0f;
    public float _speed;

    [Header("시간")]
    public float time = 0.0f;
    public float Duration = 1.2f;

    [Header("데미지")]
    public int Damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _speed = StartSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //방향을 구한다.
        Vector2 direction = Vector2.up;

        //방향에 따라 이동한다. 
        Vector2 position = transform.position;
        Vector2 newPosition = position + direction * _speed * Time.deltaTime;

        ////실습 1번 
        //{
        //    _time += Time.deltaTime;

        //    _speed = Mathf.Lerp(StartSpeed, MaxSpeed, _time / Duration);
        //    //_time / Duration을 하는 이유는
        //    //전체 구간 중 지금이 몇 퍼센트쯤 왔는지"를 계산해서,
        //    //그 비율에 따라 StartSpeed → MaxSpeed로 자연스럽게 보간하기 위해서입니다.
        //}

        // 실습 1번 풀이
        {
            float acce = (MaxSpeed - StartSpeed) / Duration;
            _speed += Time.deltaTime * acce;
            _speed = Mathf.Min(_speed, MaxSpeed);
            //              ㄴ 어떤 속성과 어떤 메서드를 가지고 있는지 톺아볼 필요가 있다.
        }


        transform.position = newPosition;
        //transform.Translate(direction * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //총알은 Enemy와만 충돌 이벤트를 처리한다.
        if (other.CompareTag("Enemy") == false) return;

        //GetComponent는 게임오브젝트에 붙어있는 컴포넌트를 가져올 수 있다.
        Enemy enemy = other.gameObject.GetComponent<Enemy>();

       //객체간의 상호작용을 할 때 : 묻지말고 시켜라(디미터의 법칙)
       enemy.Hit(Damage);

        //if (other.gameObject.CompareTag("Enemy"))
        //{
        //    enemy._health -= Damage;
        //}

        Destroy(this.gameObject);
        //Destroy(other.gameObject);

        //Debug.Log(enemy._health);
    }
}
