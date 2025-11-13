using UnityEngine;
using UnityEngine.UIElements;

public class BulletFactory : MonoBehaviour
{
    private static BulletFactory _instance = null;

    public static BulletFactory Instance => _instance;

    //인스턴스가 이미 생성(참조)된게 있다면
    //후발주자들은 삭제해버린다.
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this; //this는 BulletFactory를 가리킨다.
    }

    [Header("총알 프리팹")]
    public GameObject BulletPrefab;

    [Header("보조 총알 프리팹")]
    public GameObject AssiBulletPrefab;

    public GameObject MakeBullet(Vector3 position)
    {
        return Instantiate(BulletPrefab, position, Quaternion.identity, transform);
    }

    public GameObject MakeAssiBullet(Vector3 position)
    {
        return Instantiate(AssiBulletPrefab, position, Quaternion.identity, transform);
    }
}
