using UnityEngine;
using UnityEngine.UIElements;

public class BulletFactory : MonoBehaviour
{
    private static BulletFactory _instance = null;

    public static BulletFactory Instance => _instance;

    //인스턴스가 이미 생성(참조)된게 있다면
    //후발주자들은 삭제해버린다.

    [Header("총알 프리팹")]
    [SerializeField] private GameObject _bulletPrefab;
    //[SerializeField] public GameObject BulletPrefab;

    [Header("보조 총알 프리팹")]
    [SerializeField] private GameObject _assiBulletPrefab; 

    [Header("풀링")]
    public int PoolSize = 30;
    private GameObject[] _bulletObjectPool; //탄창(게임 오브젝트 풀)
    private GameObject[] _assiBulletObjectPool; //탄창(게임 오브젝트 풀)

    private void Awake()
    {
        // 인스턴스가 이미 생성(참조)된게 있다면
        // 후발주자들은 삭제해버린다.
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this; //this는 BulletFactory를 가리킨다.

        PoolInit();
    }


    //Awake vs Start vs Lazy => 한번만 실행하면 된다.
    //풀 탄창 초기화
    private void PoolInit()
    {
        // 1. 탄창에 총알을 담을 수 있는 크기 배열로 만들어준다.
        _bulletObjectPool = new GameObject[PoolSize];
        _assiBulletObjectPool = new GameObject[PoolSize];

        // 2. 탄창에 크기만큼 반복해서 
        for (int i = 0; i < PoolSize; ++i)
        {
            // 3. 총알을 생성한다.
            GameObject bulletObject = Instantiate(_bulletPrefab, transform);
            GameObject assiBulletObject = Instantiate(_assiBulletPrefab, transform);

            // 4. 생성한 총알을 탄창에 담는다
            _bulletObjectPool[i] = bulletObject;
            _assiBulletObjectPool[i] = assiBulletObject;

            // 5. 비활성화 한다.
            bulletObject.SetActive(false);
            assiBulletObject.SetActive(false );
        }

    }

    public GameObject MakeBullet(Vector3 position)
    {
        //1. 탄창 안에 있는 총알들 중에서 
        for (int i = 0; i < PoolSize; ++i)
        {
            GameObject bulletObject = _bulletObjectPool[i];

            // 2.비활성화된 총알 하나를 찾아
            if( bulletObject.activeInHierarchy == false)
            {
                // 3. 위치를 수정하고, 활성화시킨다.
                bulletObject.transform.position = position;
                bulletObject.SetActive (true);

                return bulletObject;
            }
        }
        Debug.LogError("탄창에 총알 개수가 부족합니다.");
        return null;
    }

    public GameObject MakeAssiBullet(Vector3 position)
    {
               //1. 탄창 안에 있는 총알들 중에서 
        for (int i = 0; i < PoolSize; ++i)
        {
            GameObject assiBulletObject = _assiBulletObjectPool[i];

            // 2.비활성화된 총알 하나를 찾아
            if(assiBulletObject.activeInHierarchy == false)
            {
                // 3. 위치를 수정하고, 활성화시킨다.
                assiBulletObject.transform.position = position;
                assiBulletObject.SetActive (true);

                return assiBulletObject;
            }
        }
        Debug.LogError("탄창에 어시총알 개수가 부족합니다.");
        return null;
    }
}
