using UnityEngine;

public class PlayertFire : MonoBehaviour
{
    //목표 : 스페이스바를 누르면 총알을 만들어서 발사하고 싶다.

    //필요 속성
    [Header("총알 프리팹")]
    public GameObject BulletPrefab;

    [Header("보조 총알 프리팹")]
    public GameObject AssiBulletPrefab;

    [Header("총구")]
    public Transform LeftFirePosition;
    public Transform RightFirePosition;

    [Header("보조 총구")]
    public Transform LeftAssiFirePosition;
    public Transform RightAssiFirePosition;

    [Header("장전시간")]
    public float time;
    public float Ptime;
    public float Load = 0.6f;
    public int Count = 0;

    [Header("모드")]
    public bool auto = false;
    public float autoShoot = 0.6f;
    public float NextShoot = 0.0f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        // 1. 발사 버튼을 누르면
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            auto = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            auto = false;
        }

        if(auto == true)
        {
            if(time >= NextShoot)
            {
                Shoot();
                AssiShoot();

                NextShoot = time + autoShoot;
            }

        }
        else if (auto == false && Input.GetKeyDown(KeyCode.Space))
        {
            if (Count == 0)
            {
                Count = 1;
                Ptime = time;

                Shoot();
                AssiShoot();
            }
        }
        if (Count == 1 && time >= Ptime + Load)
        {
            Count = 0;
        }
    }

    public void AttackSpeedUp(float value)
    {
        Load /= value;
        autoShoot /= value;
    }

    public void Shoot()
    {
        // 2. 프리팹으로부터 게임 오브젝트를 생성한다.
        // 유니티에서 게임 오브젝트를 생성할때는 new가 Instantiate라는 메서드를 이용한다.
        //클래스 -> 객체(속성 + 기능) -> 메모리에 로드된 객체를 인스턴스
        //                           ㄴ> 인스턴스화
        GameObject Leftbullet = Instantiate(BulletPrefab);
        GameObject Rightbullet = Instantiate(BulletPrefab);
        //3. 총알의 위치를 총구 위치로 바꾸기 
        Leftbullet.transform.position = LeftFirePosition.position;//생성 후 위치 수정(this는 생략가능)
        Rightbullet.transform.position = RightFirePosition.position;
    }
    public void AssiShoot()
    {
        GameObject Leftbullet = Instantiate(AssiBulletPrefab);
        GameObject Rightbullet = Instantiate(AssiBulletPrefab);
        
        Leftbullet.transform.position = LeftAssiFirePosition.position;
        Rightbullet.transform.position = RightAssiFirePosition.position;
    }
}
