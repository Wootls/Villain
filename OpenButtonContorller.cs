using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OpenButtonContorller : MonoBehaviour
{
    public AchiveManager achiveManager;

    // 매장 오픈 가격
    [Header("# shopPrice Text")]
    public TextMeshProUGUI[] shopText;
    // 매장 레벨
    [Header("# shopLevel Text")]
    public TextMeshProUGUI[] shopLevel;

    // 건물 업그레이드 비용 증가량 // (open비용 / 2) + (open비용 * 건물레벨-1)
    [Header("# Upgrade Plus money")]
    public TextMeshProUGUI[] shopUpgrade;
    // 건물 업그레이드에 따라 시간돈 증가량 표시 // 건물 레벨 * 건물 순서
    [Header("# Upgrade time money Ratio")]
    public TextMeshProUGUI[] shopUpRatio;

    // 건물 오픈 갯수 (매출 배수 증가)
    [HideInInspector]
    public int openCount = 1;

    // 건물 이름
    string[] buildingName = { "CafeLevel", "marketLevel", "pcLevel", "singLevel", "hospitalLevel", "dang9Level" };

    // 구매여부
    [HideInInspector]
    public bool cafeBought = false;
    [HideInInspector]
    public bool marketBought = false;
    [HideInInspector]
    public bool pcBought = false;
    [HideInInspector]
    public bool singBought = false;
    [HideInInspector]
    public bool hospitalBought = false;
    [HideInInspector]
    public bool dang9Bought = false;

    // 구매 버튼 순서
    int bNum;
    // 업글 버튼 순서
    int UpNum;
    
    private void Start()
    {
        // 건물 구매 가격
        for(int i = 0; i < shopText.Length; i++)
        {
            // 건물 구매 가격
            shopText[i].text = GameManager.instance.shopPrice[i].ToString();
            // 시간 돈 증가 비율
            shopUpRatio[i].text = (achiveManager.buildingLevel[i] * (i+1)).ToString();
            // 건물 강화 비용
            shopUpgrade[i].text = ((GameManager.instance.shopPrice[i] / 2) + (GameManager.instance.shopPrice[i] * (achiveManager.buildingLevel[i] - 1))).ToString();
            // 건물 레벨 표시
            shopLevel[i].text = achiveManager.buildingLevel[i].ToString();
        }
    }

    private void Update()
    {
        for(int i = 0; i < achiveManager.buildingLevel.Length; i++)
        {
            // 건물 레벨 표시
            shopLevel[i].text = achiveManager.buildingLevel[i].ToString();
        }

        for (int i = 0; i< achiveManager.lockBuilding.Length; i++)
        {
            // 건물 구매 가능 여부 판단
            if (GameManager.instance.totalMoney < GameManager.instance.shopPrice[i])
                achiveManager.lockBuilding[i].GetComponent<Button>().interactable = false;
            else
                achiveManager.lockBuilding[i].GetComponent<Button>().interactable = true;

            // 업그레이드 가능 여부 판단
            if (GameManager.instance.totalMoney < (GameManager.instance.shopPrice[i] / 2) + (GameManager.instance.shopPrice[i] * i))
                achiveManager.unlockBuilding[i].GetComponent<Button>().interactable = false;
            else
                achiveManager.unlockBuilding[i].GetComponent<Button>().interactable = true;
        }
    }
    // 카페 구매 버튼
    public void CafeOpenButton()
    {
        bNum = 0;
        openCount++;
        cafeBought = true;
        GameManager.instance.buttonControler.anim.SetBool("MoveDown", true);
        GameManager.instance.buttonControler.anim.SetBool("MoveUp", false);
        GameManager.instance.everyStoryManagement.story[1].SetActive(true);
        GameManager.instance.everyStoryManagement.mainBG.SetActive(true);
        OpenManagement();
    }
    // 편의점 구매 버튼
    public void MarketButton()
    {
        bNum = 1;
        marketBought = true;
        OpenManagement();
    }
    // PC방 구매 버튼
    public void PCButton()
    {
        bNum = 2;
        pcBought = true;
        OpenManagement();
    }
    // 노래방 구매 버튼 
    public void SingButton()
    {
        bNum = 3;
        singBought = true;
        OpenManagement();
    }
    // 병원 구매 버튼
    public void HospitalButton()
    {
        bNum = 4;
        hospitalBought = true;
        OpenManagement();
    }
    // 당구장 구매 버튼
    public void Dang9Button()
    {
        bNum = 5;
        dang9Bought = true;
        OpenManagement();
    }
    // 카페 업그레이드 버튼
    public void CafeUpgradeButton()
    {
        UpNum = 0;
        UpgradeManagement();
    }
    // 편의점 업그레이드 버튼
    public void MarketUpgradeButton()
    {
        UpNum = 1;
        UpgradeManagement();
    }
    // PC방 업그레이드 버튼
    public void PCUpgradeButton()
    {
        UpNum = 2;
        UpgradeManagement();
    }
    // 노래방 업그레이드 버튼
    public void SingUpgradeButton()
    {
        UpNum = 3;
        UpgradeManagement();
    } 
    // 병원 업그레이드 버튼
    public void HospitalUpgradeButton()
    {
        UpNum = 4;
        UpgradeManagement();
    }
    // 당구장 업그레이드 버튼
    public void Dang9UpgradeButton()
    {
        UpNum = 5;
        


        UpgradeManagement();
    }

    // 구매 버튼 클릭시 임대 문의 꺼지고 상가 열림, 버튼 바뀜
    void OpenManagement()
    {
        openCount++;
        PlayerPrefs.SetInt("openCount", openCount);
        for (int i = 0; i < GameManager.instance.buildingManagement.openMarket.Length; i++)
        {
            if(bNum == i)
            {
                achiveManager.buildingLevel[i]++;
                shopUpgrade[i].text = ((GameManager.instance.shopPrice[i] / 2) + (GameManager.instance.shopPrice[i] * (achiveManager.buildingLevel[i] - 1))).ToString();
                // 레벨, 업그레이드 저장
                PlayerPrefs.SetInt(buildingName[i], achiveManager.buildingLevel[i]);
                //
                GameManager.instance.buildingManagement.openMarket[i].SetActive(true);
                GameManager.instance.buildingManagement.emptyMarket[i].SetActive(false);
                achiveManager.lockBuilding[i].gameObject.SetActive(false);
                achiveManager.unlockBuilding[i].gameObject.SetActive(true);
                // 돈계산
                GameManager.instance.totalMoney -= GameManager.instance.shopPrice[i];
            }
        }
    }

    // 업그레이드 버튼 클릭시 레벨업, 증가량, 저장
    void UpgradeManagement()
    {
        for (int i = 0; i < achiveManager.buildingLevel.Length; i++)
        {
            if(UpNum == i && GameManager.instance.totalMoney > (GameManager.instance.shopPrice[i] / 2) + (GameManager.instance.shopPrice[i] * (achiveManager.buildingLevel[i] - 1)))
            {
                // 맥스 레벨 도착시 return 아니면 레벨 업
                if (achiveManager.buildingLevel[i] == GameManager.instance.maxLevel)
                {
                    shopLevel[i].text = "MAX";
                    shopUpgrade[i].text = "MAX";
                    return;
                }
                else
                {
                    // 건물 레벨 업 
                    achiveManager.buildingLevel[i]++;
                    // 시간 돈 증가 
                    GameManager.instance.TimeMoney(i, achiveManager.buildingLevel[i] * (i + 1));
                    // 업그레이드 비용 지불
                    //GameManager.instance.totalMoney -= (double)((GameManager.instance.shopPrice[i] / 2) + (GameManager.instance.shopPrice[i] * (achiveManager.buildingLevel[i] - 1)));
                    GameManager.instance.PayMoney((GameManager.instance.shopPrice[i] / 2) + (GameManager.instance.shopPrice[i] * (achiveManager.buildingLevel[i] - 1)));
                    // 업그레이드 비용 계산
                    shopUpgrade[i].text = ((GameManager.instance.shopPrice[i] / 2) + (GameManager.instance.shopPrice[i] * (achiveManager.buildingLevel[i]-1))).ToString();
                    // 업그레이드 돈 증가량
                    shopUpRatio[i].text = (achiveManager.buildingLevel[i] * (i + 1)).ToString();
                    // 레벨, 업그레이드 저장
                    PlayerPrefs.SetInt(buildingName[i], achiveManager.buildingLevel[i]);
                    // 매장 업그레이드를 모두 완료했을때
                    if (GameManager.instance.achiveManager.buildingLevel[5] == GameManager.instance.maxLevel)
                        GameManager.instance.everyStoryManagement.story[3].SetActive(true);
                }
            }
        }
        
    }
}
