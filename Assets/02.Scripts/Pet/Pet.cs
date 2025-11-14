using UnityEngine;

public class Pet : MonoBehaviour
{
    Player _player;
    
    [SerializeField] private float _speed;
    public Vector2 Direction;
    private Vector3 distance = new Vector3(-1f,0,0);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
