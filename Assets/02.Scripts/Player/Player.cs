using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("상태")]
    private float _health = 3;
    public void Hit(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void Heal(float value)
    {
        _health += value;

        if (_health > 3)
        {
            _health = 3;
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
