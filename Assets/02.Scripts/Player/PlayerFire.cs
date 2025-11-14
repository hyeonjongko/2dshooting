using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //목표 : 스페이스바를 누르면 총알을 만들어서 발사하고 싶다.

    //필요 속성
    
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
    public float MaxLoad = 0.2f;

    [Header("모드")]
    public bool auto = false;
    public float autoShoot = 0.6f;
    public float NextShoot = 0.0f;

    [Header("필살기")]
    public GameObject FinisherPrefab;
    private GameObject _finisher;
    private float _durationTime;
    public float Reset = 0.0f;
    private const float MaxDuration = 3.0f;
    private bool _timeResetValue = false;

    [Header("사운드")]
    public AudioSource FireSound;
    public AudioSource FinisherSound;




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
        else if (Input.GetKeyDown(KeyCode.Alpha3) && !_timeResetValue)
        {
            _timeResetValue = true;
            _finisher = Instantiate(FinisherPrefab);
            _finisher.transform.position = Vector3.zero;
            FinisherSound.Play();
            
        }
        if(_timeResetValue)
        {
            _durationTime += Time.deltaTime;
            if (_durationTime > MaxDuration)
            {
                _durationTime = Reset;
                Destroy(_finisher);
                _timeResetValue = false;
            }

        }

        if (auto == true)
        {
            if (time >= NextShoot)
            {
                Fire();

                NextShoot = time + autoShoot;
            }

        }
        else if (auto == false && Input.GetKeyDown(KeyCode.Space))
        {
            if (Count == 0)
            {
                Count = 1;
                Ptime = time;

                Fire();
            }
        }
        if (Count == 1 && time >= Ptime + Load)
        {
            Count = 0;
        }
    }
    public void Fire()
    {
        FireSound.Play();
        Shoot();
        AssiShoot();
    }
    public void AttackSpeedUp(float value)
    {
        Load /= value;
        autoShoot /= value;
        if(Load < MaxLoad)
        {
            Load = MaxLoad;
        }
        if(autoShoot < MaxLoad)
        {
            autoShoot = MaxLoad;
        }
    }
    //플레이어가 총알 생성(PlayerFire)
        //ㄴ>총알 생성 로직이 바뀔때마다 아래의 모든 코드가 수정되어야한다.
        //ㄴ>총알 생성이라는 행위 자체를 담당하는 클래스를 만들면 편하지 않을까?
        //   총알 생성기.만들어줘(타입, 데미지, 위치)  
    //적이 총알 생성(EnemyFire, Enemy, EnemyController)
    //펫고 총알 생성(PetFire, Pet, PetController)
    public void Shoot()
    {
        BulletFactory bulletFactory = GameObject.Find("BulletFactory").GetComponent<BulletFactory>();
        bulletFactory.MakeBullet(LeftFirePosition.position);
        bulletFactory.MakeBullet(RightFirePosition.position);
        //// 2. 프리팹으로부터 게임 오브젝트를 생성한다.
        //// 유니티에서 게임 오브젝트를 생성할때는 new가 Instantiate라는 메서드를 이용한다.
        ////클래스 -> 객체(속성 + 기능) -> 메모리에 로드된 객체를 인스턴스
        ////                           ㄴ> 인스턴스화
        //GameObject Leftbullet = Instantiate(BulletPrefab);
        //GameObject Rightbullet = Instantiate(BulletPrefab);
        ////3. 총알의 위치를 총구 위치로 바꾸기 
        //Leftbullet.transform.position = LeftFirePosition.position;//생성 후 위치 수정(this는 생략가능)
        //Rightbullet.transform.position = RightFirePosition.position;
    }
    public void AssiShoot()
    {
        BulletFactory bulletFactory = GameObject.Find("BulletFactory").GetComponent<BulletFactory>();
        bulletFactory.MakeAssiBullet(LeftAssiFirePosition.position);
        bulletFactory.MakeAssiBullet(RightAssiFirePosition.position);

        //GameObject Leftbullet = Instantiate(AssiBulletPrefab);
        //GameObject Rightbullet = Instantiate(AssiBulletPrefab);
        
        //Leftbullet.transform.position = LeftAssiFirePosition.position;
        //Rightbullet.transform.position = RightAssiFirePosition.position;
    }
}
