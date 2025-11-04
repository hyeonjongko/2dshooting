using UnityEngine;

public class AssiBullet : MonoBehaviour
{
    [Header("이동속도")]
    public float AssStartSpeed = 2.0f;
    public float AssMaxSpeed = 10.0f;
    private float _speed;

    [Header("시간")]
    public float Asstime = 0.0f;
    public float AssDuration = 1.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _speed = AssStartSpeed;
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
        //    time += Time.deltaTime;

        //    _speed = Mathf.Lerp(StartSpeed, MaxSpeed, time / Duration);
        //    //time / Duration을 하는 이유는
        //    //전체 구간 중 지금이 몇 퍼센트쯤 왔는지"를 계산해서,
        //    //그 비율에 따라 StartSpeed → MaxSpeed로 자연스럽게 보간하기 위해서입니다.
        //}

        // 실습 1번 풀이
        {
            float acce = (AssMaxSpeed - AssStartSpeed) / AssDuration;
            _speed += Time.deltaTime * acce;
            _speed = Mathf.Min(_speed, AssMaxSpeed);
            //              ㄴ 어떤 속성과 어떤 메서드를 가지고 있는지 톺아볼 필요가 있다.
        }


        transform.position = newPosition;
        //transform.Translate(direction * Speed * Time.deltaTime);
    }
}
