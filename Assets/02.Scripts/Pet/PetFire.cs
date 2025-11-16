using UnityEngine;

public class PetFire : MonoBehaviour
{
    [Header("총구")]
    public Transform FirePosition;

    public GameObject BulletPrefab;

    [Header("장전시간")]
    public float time;
    public float NextShoot = 0.0f;
    [SerializeField] private float _duration = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= NextShoot)
        {
            time = 0.0f;
            Shoot();

            NextShoot = time + _duration;
        }
    }
    public void Shoot()
    {
        GameObject PetBullet = Instantiate(BulletPrefab);
        PetBullet.transform.position = FirePosition.position;
    }

}
