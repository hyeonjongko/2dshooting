using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("시간")]
    public float time;
    public float NextSpawn = 0.0f;
    public float Duration = 3.0f;

    [Header("적 프리팹")]
    public GameObject EnemyPrefab;

    [Header("적 스포너 위치")]
    public Transform SpawnerPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > NextSpawn)
        {
            GameObject EnemySpaw = Instantiate(EnemyPrefab);
            EnemySpaw.transform.position = SpawnerPosition.position;

            NextSpawn = time + Duration;

        }


    }
}
