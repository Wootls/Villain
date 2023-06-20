using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    public OpenButtonContorller openButtonContorller;

    [Header("# 건물 저장 관리")]
    public GameObject[] lockBuilding;
    public GameObject[] unlockBuilding;
    enum AchiveBuilding { unlockCafe, unlockMarket, unlockPC, unlockSR, unlockHospital, unlockD9}
    AchiveBuilding[] achiveBuildings;

    [Header("# 건물 레벨 관리")]
    public int[] buildingLevel;
    enum LevelBuilding { CafeLevel, marketLevel, pcLevel, singLevel, hospitalLevel, dang9Level}
    LevelBuilding[] levelBuildings;

    [Header("# 시설물 저장 관리 ")]
    public GameObject[] lockDeco;
    public GameObject[] unlockDeco;
    enum AchiveDeco { unlockTree, unlockLailac, unlockDeco1, unlockEggFlower, unlockMagicbitjalu, unlockCar}
    AchiveDeco[] achiveDecos;

    [Header("# 동네 저장 관리")]
    public GameObject[] lockBackGround;
    public GameObject[] unlockBackGround;
    public GameObject[] upgradeBackGround;
    enum AchiveOld { old1, old2, old3, old4, old5, old6 }
    AchiveOld[] achiveOlds;

    [Header("# 옥상 저장 관리")]
    public GameObject[] lockRoof;
    public GameObject[] unlockRoof;
    enum AchiveRoof { flowerSet, treeSet, tableSet, roofETC, roofLight}
    AchiveRoof[] achiveRoofs;

    [Header("# 옥상 버프 관리")]
    public int[] roofBuff;
    enum RoofBuffSave { reduceHT, doubleTip, reduceDNT, reduceST, dropDia}
    RoofBuffSave[] roofBuffSaves;

    [Header("# 캐릭터 저장 관리")]
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;
    enum AchiveCharacter { bbangsang, airThief, airPods, jaluban, danso, dol, jocar, ddanggoo }
    AchiveCharacter[] achiveCharacters;

    [Header("# 캐릭터 레벨 관리")]
    public int[] characterLevel;
    enum LevelCharacter { bbangsangLevel, airThiefLevel, airPodsLevel, jalubanLevel, dansoLevel, dolLevel, jocarLevel, ddanggooLevel }
    LevelCharacter[] levelCharacters;

    [Header("# 최종장 저장 관리")]
    public GameObject[] lockFinal;
    public GameObject[] unlockFinal;
    enum AchiveFinal { sun, moon, nooleeho, ufo, rainbow}
    AchiveFinal[] achivefinals;

    private void Awake()
    {
        // 건물 상태 초기화
        achiveBuildings = (AchiveBuilding[])Enum.GetValues(typeof(AchiveBuilding));
        // 건물 레벨 초기화
        levelBuildings = (LevelBuilding[])Enum.GetValues(typeof(LevelBuilding));
        // 시설물 상태 초기화
        achiveDecos = (AchiveDeco[])Enum.GetValues(typeof(AchiveDeco));
        // 동네 상태 초기화
        achiveOlds = (AchiveOld[])Enum.GetValues(typeof(AchiveOld));
        // 옥상 상태 초기화
        achiveRoofs = (AchiveRoof[])Enum.GetValues(typeof(AchiveRoof));
        // 옥상  버프 초기화
        roofBuffSaves = (RoofBuffSave[])Enum.GetValues(typeof(RoofBuffSave));
        // 캐릭터 상태 초기화
        achiveCharacters = (AchiveCharacter[])Enum.GetValues(typeof(AchiveCharacter));
        // 캐릭터 레벨 초기화
        levelCharacters = (LevelCharacter[])Enum.GetValues(typeof(LevelCharacter));
        // 파이널 상태 초기화
        achivefinals = (AchiveFinal[])Enum.GetValues(typeof(AchiveFinal));
        if (!PlayerPrefs.HasKey("MyBuilding"))
            InitBuilding();
        if (!PlayerPrefs.HasKey("MyDeco"))
            InitDeco();
        if (!PlayerPrefs.HasKey("MyDongNae"))
            InitDongNae();
        if (!PlayerPrefs.HasKey("MyRoof"))
            InitRoof();
        if (!PlayerPrefs.HasKey("MyCharacter"))
            InitCharacter();
        if (!PlayerPrefs.HasKey("MyFinal"))
            InitFinal();
    }

    // 건물 상태 초기화
    void InitBuilding()
    {
        PlayerPrefs.SetInt("MyBuilding", 1);
        foreach(AchiveBuilding achiveBuilding in achiveBuildings)
        {
            PlayerPrefs.SetInt(achiveBuilding.ToString(), 0);
        }
        foreach(LevelBuilding levelBuilding in levelBuildings)
        {
            PlayerPrefs.SetInt(levelBuilding.ToString(), 0);
        }
    }

    // 시설물 상태 초기화
    void InitDeco()
    {
        PlayerPrefs.SetInt("MyDeco", 1);
        foreach (AchiveDeco achivedeco in achiveDecos)
        {
            PlayerPrefs.SetInt(achivedeco.ToString(), 0);
        }
    }

    // 동네 상태 초기화
    void InitDongNae()
    {
        PlayerPrefs.SetInt("MyDongNae", 1);
        foreach (AchiveOld achiveOld in achiveOlds)
        {
            PlayerPrefs.SetInt(achiveOld.ToString(), 0);
        }
    }

    // 옥상 상태 초기화
    void InitRoof()
    {
        PlayerPrefs.SetInt("MyRoof", 1);
        foreach (AchiveRoof achiveRoof in achiveRoofs)
        {
            PlayerPrefs.SetInt(achiveRoof.ToString(), 0);
        }
        foreach(RoofBuffSave roofBuffSave in roofBuffSaves)
        {
            PlayerPrefs.SetInt(roofBuffSave.ToString(), 0);
        }
    }

    // 캐릭터 초기화
    void InitCharacter()
    {
        PlayerPrefs.SetInt("MyCharacter", 1);
        foreach (AchiveCharacter achiveCharacter in achiveCharacters)
        {
            PlayerPrefs.SetInt(achiveCharacter.ToString(), 0);
        }
        foreach (LevelCharacter levelCharacter in levelCharacters)
        {
            PlayerPrefs.SetInt(levelCharacter.ToString(), 0);
        }
    }

    // 파이널 초기화
    void InitFinal()
    {
        PlayerPrefs.SetInt("MyFinal", 1);
        foreach (AchiveFinal achiveFinal in achivefinals)
            PlayerPrefs.SetInt(achiveFinal.ToString(), 0);
    }

    private void Start()
    {
        // 건물 상태 불러오기
        UnlockBuilding();

        // 건물 레벨, 업그레이드 저장상태 불러오기
        BringLevelBuilding();

        // 시설물 상태 불러오기
        UnlockDeco();

        // 동네 상태 불러오기
        UnlockDongNae();

        // 옥상 상태 불러오기
        UnlockRoof();

        // 옥상 버프 상태 불러오기
        RoofBuff();

        // 캐릭터 상태 불러오기
        UnlockCharacter();

        // 캐릭터 레벨 불러오기
        BringCharacterLevel();

        // 파이널 불러오기
        UnlockFinal();
    }

    // 건물 상태 불러오기
    void UnlockBuilding()
    {
        for(int i = 0; i < lockBuilding.Length; i++)
        {
            string achiveBuilding = achiveBuildings[i].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveBuilding) == 1;
            lockBuilding[i].SetActive(!isUnlock);
            unlockBuilding[i].SetActive(isUnlock);
            GameManager.instance.buildingManagement.emptyMarket[i].SetActive(!isUnlock);
            GameManager.instance.buildingManagement.openMarket[i].SetActive(isUnlock);
        }
    }
    // 건물 업그레이드 상태 불러오기
    void BringLevelBuilding()
    {
        for (int i = 0; i < buildingLevel.Length; i++)
        {
            string buildShopLevel = levelBuildings[i].ToString();
            buildingLevel[i] = PlayerPrefs.GetInt(buildShopLevel);
        }
    }
    // 시설물 상태 불러오기
    void UnlockDeco()
    {
        for(int i = 0; i < lockDeco.Length; i++)
        {
            string achiveDeco = achiveDecos[i].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveDeco) == 1;
            lockDeco[i].SetActive(!isUnlock);
            unlockDeco[i].SetActive(isUnlock);
            GameManager.instance.buildingManagement.decoration[i].SetActive(isUnlock);
        }
    }
    // 동네 상태 불러오기
    void UnlockDongNae()
    {
        for(int i = 0; i < lockBackGround.Length; i++)
        {
            string achiveOld = achiveOlds[i].ToString();
            bool isUnlock;
            if (isUnlock = PlayerPrefs.GetInt(achiveOld) == 2)
            {
                lockBackGround[i].SetActive(!isUnlock);
                unlockBackGround[i].SetActive(!isUnlock);
                upgradeBackGround[i].SetActive(isUnlock);
                GameManager.instance.buildingManagement.oldHouse[i].SetActive(!isUnlock);
                GameManager.instance.buildingManagement.newBuilding[i].SetActive(isUnlock);
            }
            else if(isUnlock = PlayerPrefs.GetInt(achiveOld) == 1)
            {
                lockBackGround[i].SetActive(!isUnlock);
                unlockBackGround[i].SetActive(isUnlock);
                upgradeBackGround[i].SetActive(!isUnlock);
                GameManager.instance.buildingManagement.oldHouse[i].SetActive(isUnlock);
                GameManager.instance.buildingManagement.newBuilding[i].SetActive(!isUnlock);
            }
        }
    }
    // 옥상 상태 불러오기
    void UnlockRoof()
    {
        for(int i = 0; i < lockRoof.Length; i++)
        {
            string achiveRoof = achiveRoofs[i].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveRoof) == 1;
            lockRoof[i].SetActive(!isUnlock);
            unlockRoof[i].SetActive(isUnlock);
            GameManager.instance.buildingManagement.roofDeco[i].SetActive(isUnlock);
        }
    }
    // 옥상 버프 불러오기
    void RoofBuff()
    {
        for (int i = 0; i < roofBuff.Length; i++)
        {
            string roofbuff = roofBuffSaves[i].ToString();
            roofBuff[i] = PlayerPrefs.GetInt(roofbuff);
        }
    }
    // 캐릭터 상태 불러오기
    void UnlockCharacter()
    {
        for (int i = 0; i < lockCharacter.Length; i++)
        {
            string achiveCharacter = achiveCharacters[i].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveCharacter) == 1;
            lockCharacter[i].SetActive(!isUnlock);
            unlockCharacter[i].SetActive(isUnlock);
            GameManager.instance.characterManager.characters[i].SetActive(isUnlock);
        }
    }
    // 캐릭터 레벨 불러오기
    void BringCharacterLevel()
    {
        for (int i = 0; i < characterLevel.Length; i++)
        {
            string levelCharacter = levelCharacters[i].ToString();
            characterLevel[i] = PlayerPrefs.GetInt(levelCharacter);
        }
    }
    // 파이널 상태 불러오기
    void UnlockFinal()
    {
        for (int i = 0; i < lockFinal.Length; i++)
        {
            string achiveFinal = achivefinals[i].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveFinal) == 1;
            lockFinal[i].SetActive(!isUnlock);
            unlockFinal[i].SetActive(isUnlock);
            GameManager.instance.finalManager.final[i].SetActive(isUnlock);
        }
    }

    private void LateUpdate()
    {
        foreach (AchiveBuilding achiveBuilding in achiveBuildings)
        {
            CheckAchive(achiveBuilding);
        }
        foreach (AchiveDeco achiveDeco in achiveDecos)
        {
            CheckDeco(achiveDeco);
        }
        foreach (AchiveOld achiveold in achiveOlds)
        {
            CheckDongNae(achiveold);
        }
        foreach (AchiveRoof achiveRoof in achiveRoofs)
        {
            CheckRoof(achiveRoof);
        }
        foreach (AchiveCharacter achiveCharacter in achiveCharacters)
        {
            CheckCharacter(achiveCharacter);
        }
        foreach (AchiveFinal achiveFinal in achivefinals)
        {
            CheckFinal(achiveFinal);
        }
    }

    // 건물
    void CheckAchive(AchiveBuilding achiveBuilding)
    {
        bool isAchive = false;

        switch (achiveBuilding)
        {
            case AchiveBuilding.unlockCafe:
                isAchive = openButtonContorller.cafeBought == true;
                break;
            case AchiveBuilding.unlockMarket:
                isAchive = openButtonContorller.marketBought == true;
                break;
            case AchiveBuilding.unlockPC:
                isAchive = openButtonContorller.pcBought == true;
                break;
            case AchiveBuilding.unlockSR:
                isAchive = openButtonContorller.singBought == true;
                break;
            case AchiveBuilding.unlockHospital:
                isAchive = openButtonContorller.hospitalBought == true;
                break;
            case AchiveBuilding.unlockD9:
                isAchive = openButtonContorller.dang9Bought == true;
                break;
        }

        // 구매하지 않았을시 저장
        if(isAchive && PlayerPrefs.GetInt(achiveBuilding.ToString())== 0)
        {
            PlayerPrefs.SetInt(achiveBuilding.ToString(), 1);
        }
    }

    // 시설물
    void CheckDeco(AchiveDeco achiveDeco)
    {
        bool isDeco = false;

        switch (achiveDeco)
        {
            case AchiveDeco.unlockTree:
                isDeco = GameManager.instance.decoButtonController.treeBought == true;
                break;
            case AchiveDeco.unlockLailac:
                isDeco = GameManager.instance.decoButtonController.lailacBought == true;
                break;
            case AchiveDeco.unlockDeco1:
                isDeco = GameManager.instance.decoButtonController.deco1Bought == true;
                break;
            case AchiveDeco.unlockEggFlower:
                isDeco = GameManager.instance.decoButtonController.eggFlowerBought == true;
                break;
            case AchiveDeco.unlockMagicbitjalu:
                isDeco = GameManager.instance.decoButtonController.magicbitjaluBought == true;
                break;
            case AchiveDeco.unlockCar:
                isDeco = GameManager.instance.decoButtonController.carBought == true;
                break;
        }

        // 구매하지 않았을시 저장
        if (isDeco && PlayerPrefs.GetInt(achiveDeco.ToString())==0)
        {
            PlayerPrefs.SetInt(achiveDeco.ToString(), 1);
        }
    }

    // 동네
    void CheckDongNae(AchiveOld achiveOld)
    {
        bool isOld = false;
        bool isNew = false;

        switch (achiveOld)
        {
            case AchiveOld.old1:
                isOld = GameManager.instance.villageButtonController.old1 == true;
                isNew = GameManager.instance.villageButtonController.new1 == true;
                break;
            case AchiveOld.old2:
                isOld = GameManager.instance.villageButtonController.old2 == true;
                isNew = GameManager.instance.villageButtonController.new2 == true;
                break;
            case AchiveOld.old3:
                isOld = GameManager.instance.villageButtonController.old3 == true;
                isNew = GameManager.instance.villageButtonController.new3 == true;
                break;
            case AchiveOld.old4:
                isOld = GameManager.instance.villageButtonController.old4 == true;
                isNew = GameManager.instance.villageButtonController.new4 == true;
                break;
            case AchiveOld.old5:
                isOld = GameManager.instance.villageButtonController.old5 == true;
                isNew = GameManager.instance.villageButtonController.new5 == true;
                break;
            case AchiveOld.old6:
                isOld = GameManager.instance.villageButtonController.old6 == true;
                isNew = GameManager.instance.villageButtonController.new6 == true;
                break;
        }

        //
        if (isNew && PlayerPrefs.GetInt(achiveOld.ToString()) == 1)
        {
            PlayerPrefs.SetInt(achiveOld.ToString(), 2);
        }
        else if (isOld && PlayerPrefs.GetInt(achiveOld.ToString()) == 0)
        {
            PlayerPrefs.SetInt(achiveOld.ToString(), 1);
        }
    }

    // 옥상
    void CheckRoof(AchiveRoof achiveRoof)
    {
        bool isRoof = false;

        switch (achiveRoof)
        {
            case AchiveRoof.flowerSet:
                isRoof = GameManager.instance.roofButtonController.flowerBought == true;
                break;
            case AchiveRoof.treeSet:
                isRoof = GameManager.instance.roofButtonController.treeBought == true;
                break;
            case AchiveRoof.tableSet:
                isRoof = GameManager.instance.roofButtonController.tableBought == true;
                break;
            case AchiveRoof.roofETC:
                isRoof = GameManager.instance.roofButtonController.etcBought == true;
                break;
            case AchiveRoof.roofLight:
                isRoof = GameManager.instance.roofButtonController.lightBought == true;
                break;
        }

        //
        if(isRoof && PlayerPrefs.GetInt(achiveRoof.ToString()) == 0)
        {
            PlayerPrefs.SetInt(achiveRoof.ToString(), 1);
        }
    }

    // 캐릭터
    void CheckCharacter(AchiveCharacter achiveCharacter)
    {
        bool isCharacter = false;

        switch (achiveCharacter)
        {
            case AchiveCharacter.bbangsang:
                isCharacter = GameManager.instance.characterButtonController.bbangsangBought == true;
                break;
            case AchiveCharacter.airThief:
                isCharacter = GameManager.instance.characterButtonController.airthiefBought == true;
                break;
            case AchiveCharacter.airPods:
                isCharacter = GameManager.instance.characterButtonController.airpodsBought == true;
                break;
            case AchiveCharacter.jaluban:
                isCharacter = GameManager.instance.characterButtonController.jalubanBought == true;
                break;
            case AchiveCharacter.danso:
                isCharacter = GameManager.instance.characterButtonController.dansoBought == true;
                break;
            case AchiveCharacter.dol:
                isCharacter = GameManager.instance.characterButtonController.dolBought == true;
                break;
            case AchiveCharacter.jocar:
                isCharacter = GameManager.instance.characterButtonController.jocarBought == true;
                break;
            case AchiveCharacter.ddanggoo:
                isCharacter = GameManager.instance.characterButtonController.ddanggooBought == true;
                break;
        }

        //
        if (isCharacter && PlayerPrefs.GetInt(achiveCharacter.ToString()) == 0)
            PlayerPrefs.SetInt(achiveCharacter.ToString(), 1);
    }

    // 파이널
    void CheckFinal(AchiveFinal achiveFinal)
    {
        bool isFinal = false;

        switch (achiveFinal)
        {
            case AchiveFinal.sun:
                isFinal = GameManager.instance.finalButtonManagement.sunBought == true;
                break;
            case AchiveFinal.moon:
                isFinal = GameManager.instance.finalButtonManagement.moonBought == true;
                break;
            case AchiveFinal.nooleeho:
                isFinal = GameManager.instance.finalButtonManagement.nooBought == true;
                break;
            case AchiveFinal.ufo:
                isFinal = GameManager.instance.finalButtonManagement.ufoBought == true;
                break;
            case AchiveFinal.rainbow:
                isFinal = GameManager.instance.finalButtonManagement.rainbowBought == true;
                break;
        }
        //
        if (isFinal && PlayerPrefs.GetInt(achiveFinal.ToString()) == 0)
            PlayerPrefs.SetInt(achiveFinal.ToString(), 1);
    }
}
