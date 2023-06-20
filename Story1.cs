using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// 처음 게임 시작 할때 실행됨
public class Story1 : MainStoryManagement
{
    public override void Start()
    {
        base.Start();
        main.SetActive(true);
        partner.SetActive(false);
        nameText.text = "궁퓨리";
        mainImage.sprite = changeMainImage[0];
    }

    public override void NextButton()
    {
        storyCount++;
        storyText.text = mainStory[storyCount];
        if (storyCount == 2)
            mainImage.sprite = changeMainImage[1];
        if (storyCount == 4)
            mainImage.sprite = changeMainImage[2];
        if (storyCount == 6)
            mainImage.sprite = changeMainImage[5];
        if (storyCount == 8)
            mainImage.sprite = changeMainImage[3];
        if (storyCount == 15)
            mainImage.sprite = changeMainImage[6];
        if (storyCount == 7)
            mainImage.sprite = changeMainImage[7];

        // 터치 명령이 있을때
        if(storyCount == 11)
        {
            GameManager.instance.isTouch = true;
            mainImage.sprite = changeMainImage[4];
        }

        // 스토리가 모두 진행되었을때
        if (storyCount == mainStory.Length-1)
        {
            nextButton.SetActive(false);
            exitButton.SetActive(true);
        }
    }

    public override void ExitButton()
    {
        GameManager.instance.isTouch = true;
        GameManager.instance.everyStoryManagement.story[0].SetActive(false);
        GameManager.instance.buttonControler.infoBG[0].SetActive(true);
        GameManager.instance.everyStoryManagement.mainBG.SetActive(false);
        storyCount = 0;
    }
}
