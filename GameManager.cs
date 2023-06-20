using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Numerics;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Touch")]
    public GameObject touch;
    public bool isTouch;

    [Header("# Scripts ")]
    // 저장 관리
    public AchiveManager achiveManager;
    // 건물관리
    public BuildingManagement buildingManagement;
    // 캐릭터 관리
    public CharacterManager characterManager;
    // open 버튼 관리
    public OpenButtonContorller openButtonContorller;
    // OutsideDeco 버튼 관리
    public DecoButtonController decoButtonController;
    // VillageDeco 버튼 관리
    public VillageButtonController villageButtonController;
    // Roof 버튼 관리
    public RoofButtonController roofButtonController;
    // Character 버튼 관리
    public CharacterButtonController characterButtonController;
    // 메인 스토리 관리
    public EveryStoryManagement everyStoryManagement;
    // 메인 버튼 관리
    public ButtonControler buttonControler;
    // final 관리
    public FinalManager finalManager;
    // final 버튼 관리
    public FinalButtonManagement finalButtonManagement;
    // audio 관리
    public SoundManagement soundManagement;

    // 메인 스토리 창 관리
    public GameObject mainStory;
    // final 버튼 켜기
    public GameObject finalButton;

    // 돈, 하트, 다이아 표시
    public Text moneyText;
    public Text heartText;
    public Text diaText;

    [Header("# 돈, 하트, 다이아 ")]
    // 시간 돈
    public int timeMoney = 1;
    // 시간 하트
    public int timeHeart = 0;
    // 궁퓨리 하트 증가량 저장
    [HideInInspector]
    public int gungPuriHeart;
    // 건물 업그레이드에 따른 돈 증가
    [HideInInspector]
    public int[] shopUpMoney;
    // 캐릭터 업그레이드에 따른 하트 증가
    [HideInInspector]
    public int[] characterUpHeart;

    // 터치 돈
    public int touchMoney = 1;
    // 데코 업그레이드에 따른 돈 증가
    [HideInInspector]
    public int[] decoUpMoney;

    //[HideInInspector]
    // 전체 돈
    public BigInteger totalMoney;
    // 돈 분할 저장
    public int frontMoney;
    public int behidMoney;

    // 하트
    public BigInteger totalHeart;
    // 하트 분할 저장
    public int frontHeart;
    public int behindHeart;

    // 다이아
    [HideInInspector]
    public int dia;

    [Header("# 시간 관리")]
    // 자동 돈 생기는 시간
    private float moneyTime;
    public float maxMoneyTime;

    // 하트 생기는 시간
    private float heartTime;
    public float maxHeartTime;

    // 1분당 자동 돈 생기는 시간
    public int omMoney;
    private float omTime;
    public float maxomTime;

    // 1시간당 자동 다이아 생기는 시간
    public int plusDia;
    private float diaTime;
    public float maxDiaTime;

    // 할머니 등장 시간
    private float grandmaTime;
    public float maxGrandmaTime;

    // 할머니 등장
    [HideInInspector]
    public bool isGrandMa = false;

    // 돈
    [Header("# Shop Price")]
    public int[] shopPrice;

    [Header("# OutsideDeco Price")]
    public int[] decoPrice;

    [Header("# VillageDeco Price, Upgrade Price")]
    public int[] villagePrice;
    public int[] villageUpPrice;

    [Header("# Roof Price")]
    public int[] roofPrice;

    [Header("# 동네 1, 4 1분당 돈 증가량")]
    public int[] dongnaeOM;

    [Header("# 동네 2, 5 터치 돈 증가량")]
    public int[] dongnaeTM;

    [Header("# 동네 할머니 등장 및 등장 시간 감소")]
    public float[] dongnaeHT;

    [Header("# Character Price, Upgrade Price")]
    public int[] characterPrice;

    [HideInInspector]
    // 건물, 캐릭터, 데코 업그레이드 최대 레벨
    public int maxLevel = 15;
    [HideInInspector]
    // 버프 업그레이드 최대 레벨
    public int maxBuffLevel = 5;

    private void Awake()
    {
        instance = this;

        // 터치 초기화
        isTouch = true;
        // 건물 돈 증가 초기화
        shopUpMoney = new int[achiveManager.unlockBuilding.Length];
        // 캐릭터 하트 증가 초기화
        characterUpHeart = new int[achiveManager.unlockCharacter.Length];
        // 터치 돈 증가 초기화
        decoUpMoney = new int[achiveManager.unlockDeco.Length];
    }

    private void Start()
    {
        maxLevel = 15;
        moneyTime = maxMoneyTime;
        omTime = maxomTime;
        diaTime = maxDiaTime;
        timeMoney = 1;
        touchMoney = 1;
        // 처음 스토리 진행
        if (!PlayerPrefs.HasKey("timeMoney"))
        {
            everyStoryManagement.story[0].SetActive(true);
            everyStoryManagement.mainBG.SetActive(true);
            totalHeart += 10;
            totalMoney += 100;
            dia += 100;
        }

        // 전체돈 불러오기
        if (PlayerPrefs.HasKey("timeMoney"))
        {
            frontMoney = PlayerPrefs.GetInt("front");
            behidMoney = PlayerPrefs.GetInt("behind");
            timeMoney = PlayerPrefs.GetInt("timeMoney");
            touchMoney = PlayerPrefs.GetInt("touchMoney");
            dia = PlayerPrefs.GetInt("Dia");
            openButtonContorller.openCount = PlayerPrefs.GetInt("openCount");
            characterButtonController.characterCount = PlayerPrefs.GetFloat("characterCount");
            if (frontMoney > 0)
            {
                totalMoney += frontMoney;
                totalMoney = totalMoney * 100000000;
            }
            totalMoney += behidMoney;
        }
        // 하트 불러오기
        if (PlayerPrefs.HasKey("timeHeart"))
        {
            frontHeart = PlayerPrefs.GetInt("frontHeart");
            behindHeart = PlayerPrefs.GetInt("behindHeart");
            timeHeart = PlayerPrefs.GetInt("timeHeart");
            if(frontHeart > 0)
            {
                totalHeart += frontHeart;
                totalHeart = totalHeart * 100000000;
            }
            totalHeart += behindHeart;
        }
        // 궁퓨리 하트 불러오기
        if (PlayerPrefs.HasKey("PuriHeart"))
            gungPuriHeart = PlayerPrefs.GetInt("PuriHeart");
        // 1분당 돈 불러오기
        if (PlayerPrefs.HasKey("OMMoney"))
            omMoney = PlayerPrefs.GetInt("OMMoney");
        // 할머니 등장시간 불러오기(할머니 등장 버튼 활성화했을시)
        if (PlayerPrefs.HasKey("OpenGrandMa"))
        {
            isGrandMa = true;
            maxGrandmaTime = PlayerPrefs.GetFloat("GrandMaTime");
            grandmaTime = maxGrandmaTime;
        }
        // 파이널 버튼 관리
        if (PlayerPrefs.HasKey("OnFinal"))
            finalButton.SetActive(true);
    }

    private void Update()
    {
        // 터치 관리
        if (isTouch == false)
            touch.SetActive(false);
        else
            touch.SetActive(true);

        // 재화 text로 표시
        if (totalMoney < 0)
            moneyText.text = 0.ToString();
        else
            moneyText.text = GetBigIntegerText(totalMoney);
        if (totalHeart < 0)
            heartText.text = 0.ToString();
        else
            heartText.text = GetBigIntegerText(totalHeart);
        diaText.text = dia.ToString();

        // 시간 돈 계산 및 전체 돈 분할
        if (moneyTime <= 0)
        {
            totalMoney += timeMoney * openButtonContorller.openCount;
            moneyTime = maxMoneyTime;
            // 분할 저장하기
            frontMoney = (int)(totalMoney / 100000000);
            behidMoney = (int)(totalMoney % 100000000);
        }
        moneyTime -= Time.deltaTime;

        // 시간당 하트 계산
        if(heartTime <= 0)
        {
            totalHeart += timeHeart;
            // 캐릭터 오픈 할때마다 1초 감소
            heartTime = (maxHeartTime - characterButtonController.characterCount);
            // 분할 저장하기
            frontHeart = (int)(totalHeart / 100000000);
            behindHeart = (int)(totalHeart % 1000000000);
        }
        heartTime -= Time.deltaTime;

        // 1분당 돈 증가
        if(omTime <= 0)
        {
            totalMoney += omMoney;
            omTime = maxomTime;
        }
        omTime -= Time.deltaTime;

        // 1시간당 다이아 증가
        if (diaTime <= 0)
        {
            dia += plusDia;
            diaTime = maxDiaTime;
        }
        diaTime -= Time.deltaTime;

        // 할머니 등장
        if (PlayerPrefs.GetInt("OpenGrandMa") == 1 && isGrandMa == true)
        {
            if (grandmaTime <= 0)
            {
                characterManager.grandMa.SetActive(true);
                isGrandMa = false;
                grandmaTime = maxGrandmaTime;
            }
            grandmaTime -= Time.deltaTime;
        }

        // 최종장 등장
        if(totalMoney >= 100000000 && !PlayerPrefs.HasKey("OnFinal"))
        {
            PlayerPrefs.SetInt("OnFinal", 1);
            everyStoryManagement.story[4].SetActive(true);
            finalButton.SetActive(true);
        }

        // 재화 text로 표시 2
        moneyText.text = GetBigIntegerText(totalMoney);
        heartText.text = GetBigIntegerText(totalHeart);
        diaText.text = dia.ToString();

        // 전체 돈 저장
        PlayerPrefs.SetInt("front", frontMoney);
        PlayerPrefs.SetInt("behind", behidMoney);
        // 시간 돈 저장
        PlayerPrefs.SetInt("timeMoney", timeMoney);
        // 터치 돈 저장
        PlayerPrefs.SetInt("touchMoney", touchMoney);
        // 전체 하트 저장
        PlayerPrefs.SetInt("frontHeart", frontHeart);
        PlayerPrefs.SetInt("behindHeart", behindHeart);
        // 시간 하트 저장
        PlayerPrefs.SetInt("timeHeart", timeHeart);
        // 다이아 저장
        PlayerPrefs.SetInt("Dia", dia);
    }

    private void LateUpdate()
    {

    }

    // 터치했을 때 돈 증가
    public void TouchMoney(int tMoney)
    {
        totalMoney += (tMoney + touchMoney);
    }
    // 타임 머니(매출) 증가
    public void TimeMoney(int num, int money)
    {
        shopUpMoney[num] = money;
        timeMoney += shopUpMoney[num];
        if (achiveManager.buildingLevel[num] - 1 > 0)
            timeMoney -= (achiveManager.buildingLevel[num] - 1) * (num + 1);
    }
    // 시실물 타임머(매출) 증가
    public void SisalTimeMoney(int money)
    {
        timeMoney += money;
    }
    // 동네 1분당 돈 증가
    public void OneTimeMoney(int num, int money)
    {
        omMoney += money;
        if (num > 0)
            omMoney -= dongnaeOM[num - 1];
        PlayerPrefs.SetInt("OMMoney", omMoney);
    }
    // 동네 구매 및 업그레이드 했을 때 터치 돈 증가
    public void OneTouchMoney(int num, int money)
    {
        touchMoney += money;
        if (num > 0)
            touchMoney -= dongnaeTM[num - 1];
    }
    // 하트 자동 증가(캐릭터)
    public void AddHeart(int num, int heart)
    {
        characterUpHeart[num] = heart;
        timeHeart += characterUpHeart[num];
        if (achiveManager.characterLevel[num] - 1 > 0)
            timeHeart -= (achiveManager.characterLevel[num] - 1) * (num + 1);
    }
    // 궁퓨리 자동 하트 증가
    public void GungPuriAddHeart(int level)
    {
        int heart;
        heart = level / 10 + 1;
        timeHeart += heart;
        gungPuriHeart += heart;
        PlayerPrefs.SetInt("PuriHeart", gungPuriHeart);
    }
    // 궁퓨리 Tip(터치 머니) 증가
    public void GungPuriTip(int level)
    {
        int tip;
        tip = level * 10;
        touchMoney += tip;
        if (level > 1)
            touchMoney -= ((level - 1)*10);
    }

    // 비용 지불
    public void PayMoney(int Money)
    {
        totalMoney -= Money;
    }
    // 하트 지불
    public void PayHeart(int heart)
    {
        totalHeart -= heart;
    }

    //단위 표시
    private string[] UnitArr = new string[] { "", "만", "억", "조", "경", "해" };
    public string GetBigIntegerText(BigInteger integer)
    {
        int placeN = 4;
        BigInteger value = integer;
        List<int> numList = new List<int>();
        // 제곱 식
        int p = (int)Mathf.Pow(10, placeN);

        do
        {
            numList.Add((int)(value % p));
            value /= p;
        }
        while (value >= 1);
        string retStr = "";
        for (int i = 0; i < numList.Count; i++)
        {
            retStr = numList[i] + UnitArr[i] + retStr;
        }
        return retStr;
    }
}
