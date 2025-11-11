using System;
using UnityEngine;

public enum EItemType
{
    SpeedUp,
    Heal,
    AttackSpeedUp
}
public class Item : MonoBehaviour
{
    // 충돌 트리거가 일어났을 때 
    //만약 플레이어 태그라면
    //플레이어 게임오브젝트의 플레이어무브 컴포넌트를 읽어온다.
    //스피드를 +N 해준다.
    //나를 삭제한다.

    //Player player;
    //PlayerMove playerMove;
    //PlayerFire playerFire;

    Animator _animator;

    [Header("아이템 타입")]
    public EItemType ItemType;

    [Header("아이템 효과 증가 수치")]
    private int _speedUp = 1;
    private float _heal = 1.0f;
    private float _attackSpeed = 2.0f;

    [Header("아이템 자석효과")]
    public float time;
    public float MagnetTime = 2.0f;
    public float Speed;
    public Vector2 Direction;

    [Header("아이템 획득 프리팹")]
    public GameObject GainPrefab;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetTrigger("Play");
        //_animator.Play("HealthUp",0);
        //_animator.Play("SpeedUp", 0);
        //_animator.Play("AttackSpeedUp", 0);

        time += Time.deltaTime;
        if (time >= MagnetTime)
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

    private void MakeGainEffect()
    {
        Instantiate(GainPrefab, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") == false) return;

        Apply(other);

        MakeGainEffect();

        Destroy(this.gameObject);
    }
    private void Apply(Collider2D other)
    {
        if (ItemType == EItemType.SpeedUp)
        {
            PlayerMove playerMove = other.gameObject.GetComponent<PlayerMove>();
            playerMove.SpeedUp(_speedUp);
        }
        else if (ItemType == EItemType.Heal)
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.Heal(_heal);
        }
        else if (ItemType == EItemType.AttackSpeedUp)
        {
            PlayerFire playerFire = other.gameObject.GetComponent<PlayerFire>();
            playerFire.AttackSpeedUp(_attackSpeed);
        }
    }
}
