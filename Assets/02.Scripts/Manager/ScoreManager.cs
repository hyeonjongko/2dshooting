using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // 목표 : 적을 죽일 때마다 점수를 올리고, 현재 점수를 UI에 표시하고 싶다.
    // 필요 속성
    // - 현재 점수 UI(Text 컴포넌트) (규칙 : UI 요소는 항상 변수명 뒤에 UI 붙인다.)
    [SerializeField] private Text _currentScoreTextUI; //[SerializeField] : 필드를 유니티가 알 수 있도록 직렬화
    [SerializeField] private Text _bestscoreTextUI;
    // - 현재 점수를 기억할 변수
    private int _currentScore = 0;
    private int _startScore = 0;
    private const string ScoreKey = "Score";

    [Header("최고 점수 ")]
    private int _bestScore = 0;
    private const string BestScoreKey = "BestScore";

    [Header("Text 애니메이션")]
    private Animator _scoreAnimator;
    private float _lastAnimTime = 0f;
    private const float ANIM_COOLDOWN = 0.05f;
    //private float _time;
    //private float _scaleAmount = 1.3f;
    //private Vector2 _originalScale;
    void Start()
    {
        _scoreAnimator = _currentScoreTextUI.GetComponent<Animator>();
        _currentScore = _startScore;

        Load();
        Refresh();
    }

    void Update()
    {
    }

    //public void ScaleUp()
    //{
    //    _currentScoreTextUI.transform.localScale *= _scaleAmount;
    //    _currentScoreTextUI.rectTransform.localScale *= _scaleAmount;
    //}
    //1. 하나의 메서드는 하나의 일만 잘 하면 된다.
    public void AddScore(int score)
    {
        if (score < 0) return;

        _currentScore += score;

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
        _bestscoreTextUI.text = $"최고 점수 : {_bestScore:N0}";
        if (_currentScore >= _bestScore)
        {
            _bestscoreTextUI.text = $"최고 점수 : {_currentScore:N0}";
        }

    }

    private void Save()
    {
        if (_currentScore > _bestScore)
        {
            _bestScore = _currentScore;
        }
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
