using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    Enemy enemy;
    [Header("아이템 프리팹")]
    public GameObject[] ItemPrefab;

    //[Header("아이템 드랍 위치")]
    //public Transform DropperPosition;

    //public int DropPercent = 50;
    //public int DropType;
    //public int HealDrop = 70;
    //public int SpeedUpDrop = 20;
    //public int AttackSpeedUpDrop = 10;

    [Header("드랍 확률")]
    public int[] ItemWeights; //가중치 배열


    void Start()
    {

    }


    void Update()
    {

    }
    public void DropItem(Vector3 position)
    {
        // 50% 확률로 리턴
        if (Random.Range(0, 2) == 0) return;

        // 가중치의 합
        // ItemWeights [70, 20, 10]
        int weightSum = 0;  // 100
        for (int i = 0; i < ItemWeights.Length; ++i) //가중치의 합을 구한다.
        {
            weightSum += ItemWeights[i];
        }

        // 0 ~ 100 가중치의 합
        int randomValue = UnityEngine.Random.Range(0, weightSum); // 80
        //0~weightSum 중 하나를 랜덤으로 뽑음


        // 가중치 값을 더해가며 구간을 비교한다.
        // <           70 -> 0번째 아이템 생성되고
        // < (70+20)   90 -> 1번째 아이템 생성되고
        // < (90+10) 105 -> 2번째 아이템이 생성된다.
        int sum = 0;
        for (int i = 0; i < ItemWeights.Length; ++i)
        {
            // 각 아이템의 가중치를 누적하면서 randomValue가 그보다 작아지는 순간을 찾는다.
            //아이템인덱스 가중치 누적합(sum)    구간        랜덤값이 해당 구간에 있으면 선택
            //    0           70      70         0~69            70 % 확률
            //    1           20      90         70~89           20 % 확률
            //    2           10      100        90~99           10 % 확률
            sum += ItemWeights[i];
            if (randomValue < sum)
            {
                //아이템 생성 break로 아이템 하나가 생성되면 중지(안하면 아이템 3개가 동시 생성)
                //Quaternion.identity : 회전값이 없다는걸 나타냄
                Instantiate(ItemPrefab[i], position, Quaternion.identity);
                break;
            }
        }
    }

}

