using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //단 하나여야 한다.
    //전역적인 접근점을 제공해야한다.
    //만약에 접근해야하는 곳이 2개 이상이라면 랜덤으로 하나가 접근된다.
    //게임 개발에서는 Manager(관리자) 클래스를 보통 싱글톤 패턴으로 사용하는 것이 관행이다.
    //관리자라는 이름을 가지고 있는 것들만 싱글톤을 사용하는 것을 권장


    // 목표 : 적을 죽일 때마다 점수를 올리고, 현재 점수를 UI에 표시하고 싶다.
    // 필요 속성
    // - 현재 점수 UI(Text 컴포넌트) (규칙 : UI 요소는 항상 변수명 뒤에 UI 붙인다.)
    [SerializeField] private Text _currentScoreTextUI; //[SerializeField] : 필드를 유니티가 알 수 있도록 직렬화
    [SerializeField] private Text _bestScoreTextUI;
    // - 현재 점수를 기억할 변수
    private int _currentScore = 0;
    private int _startScore = 0;
    //private const string ScoreKey = "Score";


    [Header("최고 점수 ")]
    private int _bestScore = 0;
    private const string BestScoreKey = "BestScore";

    [Header("Text 애니메이션")]
    private Animator _scoreAnimator;
    private float _lastAnimTime = 0f;
    private const float ANIM_COOLDOWN = 0.05f;

    // Pet용 이벤트 (1000, 2000, 3000)
    public static event System.Action<int> OnScoreThousand;

    // Boss용 이벤트 (5000)
    public static event System.Action OnBossScoreReached;

    [Header("스폰 이벤트")]
    private int _maxCount = 5;
    private bool _bossEventTriggered = false;
    void Start()
    {
        _scoreAnimator = _currentScoreTextUI.GetComponent<Animator>();
        _currentScore = _startScore;

        Load();
        Refresh();
    }

    
    //1. 하나의 메서드는 하나의 일만 잘 하면 된다.
    public void AddScore(int score)
    {
        if (score < 0) return;

        int previousScore = _currentScore;
        _currentScore += score;

        // 1000단위를 넘어갔는지 체크
        int currentThousand = _currentScore / 1000;
        int previousThousand = previousScore / 1000;

        // 천 단위가 증가했고, 3000 이하일 때만
        if (currentThousand > previousThousand && currentThousand <= _maxCount)
        {
            OnScoreThousand?.Invoke(currentThousand);
        }

        if (_currentScore > _bestScore)
        {
            _bestScore = _currentScore;
        }

        // 일정 시간이 지났을 때만 애니메이션 재생
        if (Time.time - _lastAnimTime >= ANIM_COOLDOWN)
        {
            _scoreAnimator.SetTrigger("Gain");
            _lastAnimTime = Time.time;
        }



        Refresh();
        
        Save();
    }

    private void Refresh()
    {
        //_currentScore.ToString("#,##0") int를 세자리에 한번씩 ,를 찍는 방법
        //=> :N0가 있다
        _currentScoreTextUI.text = $"현재 점수 : {_currentScore:N0}";
        _bestScoreTextUI.text = $"최고 점수 : {_bestScore:N0}";
        //if (_currentScore >= _bestScore)
        //{
        //    _bestScoreTextUI.text = $"최고 점수 : {_currentScore:N0}";

        _bestScoreTextUI.text = $"최고 점수 : {_bestScore:N0}";
        //if (_currentScore >= _bestScore)
        //{
        //    _bestscoreTextUI.text = $"최고 점수 : {_currentScore:N0}";
        //}

    }

    private void Save()
    {
        //if (_currentScore > _bestScore)
        //{
        //    _bestScore = _currentScore;
        //}
        PlayerPrefs.SetInt(BestScoreKey, _bestScore);
        
    }
    private void Load()
    {
        _bestScore = PlayerPrefs.GetInt(BestScoreKey, _startScore);
    }
    
    //private void TestSave()
    //{
    //    //유니티에서느 값을 저장할때 ;PlayerPrefs' 모듈을 사용한다.
    //    //저장 가능한 자료형은 : int, float, string
    //    //저장을 할 때는 저장할 이름(key)과 값(value)이 이 두 형태로 저장을 한다.
    //    //저장 :Set
    //    //로드 : Get

    //    PlayerPrefs.SetInt("age", _currentScore);
    //    PlayerPrefs.SetString("name", "김홍일");
    //    Debug.Log("저장됐습니다.");
    //}

    //private void TestLoad()
    //{
    //    //유니티에서느 값을 저장할때 ;PlayerPrefs' 모듈을 사용한다.
    //    //저장 가능한 자료형은 : int, float, string
    //    //저장을 할 때는 저장할 이름(key)과 값(value)이 이 두 형태로 저장을 한다.
    //    //저장 :Set
    //    //로드 : Get

    //    int age = 17;
    //    if(PlayerPrefs.HasKey("age")) //검사
    //    {
    //        age = PlayerPrefs.GetInt("age");
    //    }
    //    string name = PlayerPrefs.GetString("name", "티모"); //default 인자
    //    Debug.Log($"{name} : {age}");
    //}

    //void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.Alpha9))
    //    {
    //        TestSave();
    //    }
    //    else if(Input.GetKeyDown(KeyCode.Alpha0))
    //    {
    //        TestLoad();
    //    }
    //}
}
