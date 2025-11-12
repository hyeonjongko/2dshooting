using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // 목표 : 적을 죽일 때마다 점수를 올리고, 현재 점수를 UI에 표시하고 싶다.
    // 필요 속성
    // - 현재 점수 UI(Text 컴포넌트) (규칙 : UI 요소는 항상 변수명 뒤에 UI 붙인다.)
    [SerializeField] private Text _currentScoreTextUI; //[SerializeField] : 필드를 유니티가 알 수 있도록 직렬화
    // - 현재 점수를 기억할 변수
    private int _currentscore = 0;
    //public int CurrentScore => _currentscore;
    void Start()
    {
        Refresh();
    }

    public void AddScore(int score)
    {
        if (score < 0) return;
        _currentscore += score;
        Refresh();
    }

    private void Refresh()
    {
        _currentScoreTextUI.text = $"현재 점수 : {_currentscore.ToString("#,##0")}";
        //_currentscore.ToString("#,##0") int를 세자리에 한번씩 ,를 찍는 방법
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
