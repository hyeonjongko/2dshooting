using UnityEngine;

public class PetMove : MonoBehaviour
{
    [Header("플레이어 추종")]
    [SerializeField] private float _followSpeed;
    [SerializeField] private float _distance;
    private float _stopDistance = 0.5f;
    //private Transform _player;

    private Transform _target; // 따라갈 대상
    void Update()
    {
        //플레이어의 뒤쪽 목표 위치 계산
        Vector3 targetPosition = _target.position - _target.forward * _distance;

        float distance = Vector3.Distance(transform.position, targetPosition);

        if (distance > _stopDistance)
        {
            // 부드럽게 따라가기
            transform.position = Vector3.Lerp(transform.position, targetPosition, _followSpeed * Time.deltaTime);
        }
    }

    public void Initialize(Transform target)
    {
        _target = target;
    }

}