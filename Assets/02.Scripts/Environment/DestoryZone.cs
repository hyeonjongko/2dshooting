using UnityEngine;

public class DestoryZone : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            other.gameObject.SetActive(false);
            return;
        }
        Destroy(other.gameObject);
    }

}

