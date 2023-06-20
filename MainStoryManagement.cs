using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainStoryManagement : MonoBehaviour
{
    // 캐릭터 이름
    public string mainName;
    public string partnerName;

    // 캐릭터 이름
    public TextMeshProUGUI nameText;

    [Header("# 이미지 관리")]
    //
    public GameObject main;
    public GameObject partner;
    // 궁퓨리 처음 이미지
    public Image mainImage;
    // 궁퓨리 바뀌는 이미지
    public Sprite[] changeMainImage;
    // 다른 캐릭터 처음 이미지
    public Image partnerImage;
    // 다른 캐릭터 바뀌는 이미지
    public Sprite[] changePartnerImage;

    [Header("# 버튼 관리")]
    // next 버튼
    public GameObject nextButton;
    // exit 버튼
    public GameObject exitButton;

    [Header("# 스토리 관리")]
    // 스토리 text
    public TextMeshProUGUI storyText;
    // 스토리 내용
    public string[] mainStory;
    // 스토리 진행 
    protected int storyCount;


    public virtual void Start()
    {
        GameManager.instance.isTouch = false;
        GameManager.instance.buttonControler.anim.SetBool("MoveDown", true);
        GameManager.instance.buttonControler.anim.SetBool("MoveUp", false);
        storyCount = 0;
        storyText.text = mainStory[storyCount];
    }

    public virtual void NextButton()
    {
        /*
        storyText.text = mainStory[storyCount];
        storyCount++;
        if(storyCount == mainStory.Length-1)
        {
            storyCount = 0;
        }*/
    }

    public virtual void ExitButton()
    {
        
    }
}
