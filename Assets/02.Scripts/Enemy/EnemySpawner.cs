using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("시간")]
    private float _time;
    public float StartTime = 1.0f;
    public float EndTime = 4.0f;
    public float Duration = 0.0f;



    [Header("적 스포너 위치")]
    public Transform SpawnerPosition;

    [Header("스폰 확률")]
    public int SpawnPercent = 70;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 쿨타임을 1과 2사이로 랜덤하게 지정한다.
        float randomCoolTime = UnityEngine.Random.Range(StartTime, EndTime);
        //UnityEngine을 위에 using으로 선언해줘서 생략이 가능하지만
        //생략하면 다른 에디터의 있는 것을 사용할 수도 있어서 생략하지 않는 것을 권장한다.
        Duration = randomCoolTime;
    }

    void Update()
    {
        //1. 시간이 흐르다가 
        _time += Time.deltaTime;

        //2. 쿨타임이 되면
        if (_time >= Duration)
        {
            _time = 0f;

            //3. 에너미프리팹으로부터 생성
            if (UnityEngine.Random.Range(0, 100) < SpawnPercent)
            {
                EnemyFactory enemyFactory = GameObject.Find("EnemyFactory").GetComponent<EnemyFactory>();

                enemyFactory.MakeEnemy(SpawnerPosition.position);
                //GameObject EnemySpawn = Instantiate(EnemyPrefab[(int)EEnemyType.Directional]);
                //EnemySpawn.transform.position = SpawnerPosition.position;
            }
            else
            {
                EnemyFactory enemyFactory = GameObject.Find("EnemyFactory").GetComponent<EnemyFactory>();

                enemyFactory.MakeTraceEnemy(SpawnerPosition.position);
                //GameObject EnemySpawn = Instantiate(EnemyPrefab[(int)EEnemyType.Trace]);
                //EnemySpawn.transform.position = SpawnerPosition.position;
            }

                Duration = UnityEngine.Random.Range(StartTime, EndTime);
        }
            //transform은 GetComponent()로 하지 않는 이유는 transform은 항상 component에 있기 때문에.


    }
}
