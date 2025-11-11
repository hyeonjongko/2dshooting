using UnityEngine;

public class Finisher : MonoBehaviour
{
    Animator _animator;

    [Header("파티클 프리팹")]
    public GameObject ParticlePrefab;

    [Header("데미지")]
    private const float Damage = 999999999.0f;

    //[Header("지속시간")]
    //private float MaxDuration = 3.0f;
    //private bool _hasSpawned = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetTrigger("Play");
    }

    // Update is called once per frame
    void Update()
    {
        


    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") == false) return;

        Enemy enemy = other.gameObject.GetComponent<Enemy>();

        enemy.Hit(Damage);
    }

}
