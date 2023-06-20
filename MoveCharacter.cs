using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveCharacter : MonoBehaviour
{
    // 시작 지점 및 목적지
    public Transform startSpot;
    public Transform endSpot;

    // 이동속도
    public float moveSpeed;

    // 할머니
    Transform grandMa;

    // 할머니 터치 보상 창
    public GameObject grandMaReward;

    // 보상 창 text
    public TextMeshProUGUI rewardStringText;
    public TextMeshProUGUI rewardText;

    // 보상 목록
    public string[] rewardString;
    public int[] reward;

    int ranNum;

    private void Start()
    {
        grandMa = GetComponent<Transform>();
        grandMa.transform.position = startSpot.position;
    }

    private void Update()
    {
        MoveGrandMa();
    }

    // 캐릭터 이동
    void MoveGrandMa()
    {
        float distance = Vector3.Distance(grandMa.position, endSpot.position);

        if (distance <= 0.1)
        {
            grandMa.position = startSpot.position;
            GameManager.instance.isGrandMa = true;
            this.gameObject.SetActive(false);
        }
        else
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }

    // 할머니 버튼
    public void GrandMaButton()
    {
        // 랜덤 숫자
        ranNum = Random.Range(0,3);

        rewardStringText.text = rewardString[ranNum].ToString();
        rewardText.text = reward[ranNum].ToString();

        GameManager.instance.isGrandMa = true;
        this.transform.position = startSpot.position;
        grandMaReward.SetActive(true);
        this.gameObject.SetActive(false);
    }

    // 보상 완료 버튼
    public void OKButton()
    {
        if (ranNum == 0)
            GameManager.instance.totalMoney += 10000;
        else if (ranNum == 1)
            GameManager.instance.totalHeart += 500;
        else
            GameManager.instance.dia += 1;
        grandMaReward.SetActive(false);
    }
}
