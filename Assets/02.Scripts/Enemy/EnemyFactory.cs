using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    private static EnemyFactory _instance = null;
    public static EnemyFactory Instance => _instance;

    [Header("적 프리팹")]
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _traceEnemyPrefab;

    [Header("풀링")]
    public int PoolSize = 30;
    private GameObject[] _enemyObjectPool;     
    private GameObject[] _traceEnemyObjectPool;

    private void Awake()
    {
        if( _instance != null )
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;

        PoolInit();
    }
    private void PoolInit()
    {
        
        _enemyObjectPool = new GameObject[PoolSize];
        _traceEnemyObjectPool = new GameObject[PoolSize];

        
        for (int i = 0; i < PoolSize; ++i)
        {
            
            GameObject bulletObject = Instantiate(_enemyPrefab, transform);
            GameObject assiBulletObject = Instantiate(_traceEnemyPrefab, transform);

            
            _enemyObjectPool[i] = bulletObject;
            _traceEnemyObjectPool[i] = assiBulletObject;

            
            bulletObject.SetActive(false);
            assiBulletObject.SetActive(false);
        }

    }

    public GameObject MakeEnemy(Vector3 position)
    {
        for (int i = 0; i < PoolSize; ++i)
        {
            GameObject enemyObject = _enemyObjectPool[i];

            // 2.비활성화된 총알 하나를 찾아
            if (enemyObject.activeInHierarchy == false)
            {
                // 3. 위치를 수정하고, 활성화시킨다.
                enemyObject.transform.position = position;
                enemyObject.SetActive(true);

                return enemyObject;
            }
        }
        Debug.LogError("enemy가 없습니다.");
        return null;
    }
    public GameObject MakeTraceEnemy(Vector3 position)
    {
        for (int i = 0; i < PoolSize; ++i)
        {
            GameObject traceEnemyObject = _traceEnemyObjectPool[i];

           
            if (traceEnemyObject.activeInHierarchy == false)
            {
                
                traceEnemyObject.transform.position = position;
                traceEnemyObject.SetActive(true);

                return traceEnemyObject;
            }
        }
        Debug.LogError("enemy가 없습니다.");
        return null;
    }
}
