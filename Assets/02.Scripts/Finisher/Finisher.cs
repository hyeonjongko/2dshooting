using UnityEngine;

public class Finisher : MonoBehaviour
{
    PlayerFire _playerFire;
    Animator _animator;

    [Header("파티클 프리팹")]
    public GameObject ParticlePrefab;

    [Header("데미지")]
    private float _damage = 999999999.0f;

    [Header("지속시간")]
    private float MaxTime = 3.0f;
    public float Reset = 0.0f;
    private bool _hasSpawned = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        _playerFire = playerObject.GetComponent<PlayerFire>();

        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetTrigger("Play");

        if (!_hasSpawned)
        {
            //MakeParticleEffect();
            _hasSpawned = true; // 실행 후 true로 바꿔서 다시 실행되지 않도록
        }

        if (_playerFire.DurationTime > MaxTime)
        {
            Destroy(this.gameObject);
            _playerFire.isFinisherActive = false;
            _playerFire.DurationTime = Reset;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") == false) return;

        Enemy enemy = other.gameObject.GetComponent<Enemy>();

        enemy.Hit(_damage);
    }
    //private void MakeParticleEffect()
    //{
    //    Instantiate(ParticlePrefab, transform.position, Quaternion.identity);
    //    _hasSpawned = false;
    //}
}
