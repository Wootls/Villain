using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story_APLV10 : MainStoryManagement
{
    public override void Start()
    {
        base.Start();
        nameText.text = partnerName;
        main.SetActive(false);
        partner.SetActive(true);
        partnerImage.sprite = changePartnerImage[0];
    }

    public override void NextButton()
    {
        storyCount++;
        storyText.text = mainStory[storyCount];

        // 스토리가 모두 진행되었을때
        if (storyCount == mainStory.Length - 1)
        {
            nextButton.SetActive(false);
            exitButton.SetActive(true);
            nameText.text = mainName;
            main.SetActive(true);
            partner.SetActive(false);
            mainImage.sprite = changeMainImage[0];
            return;
        }

        if(storyCount % 2 == 1)
        {
            nameText.text = mainName;
            main.SetActive(true);
            partner.SetActive(false);
            mainImage.sprite = changeMainImage[0];
        }
        else if(storyCount % 2 == 0)
        {
            nameText.text = partnerName;
            main.SetActive(false);
            partner.SetActive(true);
            partnerImage.sprite = changeMainImage[0];
        }
    }

    public override void ExitButton()
    {
        GameManager.instance.isTouch = true;
        // 현재 스토리 페이지
        GameManager.instance.everyStoryManagement.lv10CharacterStory[GameManager.instance.characterButtonController.upgradeNum].SetActive(false);
        storyCount = 0;
    }
}
