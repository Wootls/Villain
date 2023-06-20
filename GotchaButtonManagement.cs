using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GotchaButtonManagement : MonoBehaviour
{
    //
    public GameObject gotchaChang;
    public GameObject gotchaRewardChang;
    public GameObject notEnoughChang;

    //
    public TextMeshProUGUI rewardText;

    int getNum;

    int[] gotchaNum = new int[100];

    public int[] reward;

    private void Start()
    {
        for(int i = 0; i < gotchaNum.Length; i++)
        {
            if (i < 60)
                gotchaNum[i] = 5;
            else if (i < 90 && i >= 60)
                gotchaNum[i] = 4;
            else if (i < 96 && i >= 90)
                gotchaNum[i] = 3;
            else if (i < 99 && i >= 96)
                gotchaNum[i] = 2;
            else
                gotchaNum[i] = 1;
        }
    }

    public void PutGotChaButton()
    {
        gotchaChang.SetActive(true);
    }

    public void PlayGotChaButton()
    {
        if (GameManager.instance.dia > 10)
        {
            GameManager.instance.dia -= 10;
            getNum = Random.Range(0, gotchaNum.Length);

            rewardText.text = (reward[gotchaNum[getNum]-1]).ToString();
            gotchaChang.SetActive(false);
            gotchaRewardChang.SetActive(true);
        }
        else
        {
            gotchaChang.SetActive(false);
            notEnoughChang.SetActive(true);
        }
    }
    public void ExitButton()
    {
        gotchaChang.SetActive(false);
    }

    public void GetRewardButton()
    {
        gotchaRewardChang.SetActive(false);
        GameManager.instance.dia += (reward[gotchaNum[getNum]-1]);
    }

    public void NotenoughMoneyOkButton()
    {
        notEnoughChang.SetActive(false);
    }

}
