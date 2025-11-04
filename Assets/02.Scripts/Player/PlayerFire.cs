using UnityEngine;

public class PlayertFire : MonoBehaviour
{
    //목표 : 스페이스바를 누르면 총알을 만들어서 발사하고 싶다.

    //필요 속성
    [Header("총알 프리팹")]
    public GameObject BulletPrefab;

    [Header("총구")]
    public Transform FirePosition;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        // 1. 발사 버튼을 누르면
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // 2. 프리팹으로부터 게임 오브젝트를 생성한다.
            // 유니티에서 게임 오브젝트를 생성할때는 new가 Instantiate라는 메서드를 이용한다.
            //클래스 -> 객체(속성 + 기능) -> 메모리에 로드된 객체를 인스턴스
            //                           ㄴ> 인스턴스화
            GameObject bullet = Instantiate(BulletPrefab);
            //3. 총알의 위치를 총구 위치로 바꾸기 
            bullet.transform.position = FirePosition.position;//생성 후 위치 수정(this는 생략가능)
        }
    }
}
