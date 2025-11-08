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
    //PlayertFire playerFire;

    [Header("아이템 타입")]
    public EItemType ItemType;

    [Header("아이템 효과 증가 수치")]
    private int _speedUp = 1;
    private float _heal = 1.0f;
    private float _attackSpeed = 2.0f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") == false)
        {
            return;
        }

        else
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
                PlayertFire playerFire = other.gameObject.GetComponent<PlayertFire>();
                playerFire.AttackSpeedUp(_attackSpeed);
            }
        }



            Destroy(this.gameObject);
    }
}
