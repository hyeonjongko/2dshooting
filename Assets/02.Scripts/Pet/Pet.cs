using UnityEngine;

public class Pet : MonoBehaviour
{
    Player player;

    [SerializeField] private GameObject _petPrefab;
    private Transform _spawnPoint;

    void Start()
    {
        player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        // Pet은 1000, 2000, 3000에만 반응
        ScoreManager.OnScoreThousand += HandleScoreMilestone;
    }

    private void OnDisable()
    {
        ScoreManager.OnScoreThousand -= HandleScoreMilestone;
    }

    private void HandleScoreMilestone(int thousandCount)
    {
        SpawnPet();
    }
    private void SpawnPet()
    {
        GameObject PlayerObject = GameObject.FindWithTag("Player");
        Vector3 spawnPosition = PlayerObject.transform.position;
        Instantiate(_petPrefab, spawnPosition, Quaternion.identity);
    }
}
