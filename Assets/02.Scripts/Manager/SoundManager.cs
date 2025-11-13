using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource effectSource; //효과음 재생용 AudioSource

    private void Awake()
    {
        Instance = this;
    }
    
    // 외부에서 사운드를 재생하고 싶을 때 사용하는 함수
    public void PlaySound(AudioClip clip)
    {
         // PlayOneShot은 여러 소리를 동시에 재생할 수 있음
        effectSource.PlayOneShot(clip);
    }
}
