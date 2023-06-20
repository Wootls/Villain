using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMSpwanManager : MonoBehaviour
{
    // 시민
    public GameObject[] seeMean1;
    public GameObject[] seeMean2;
    public GameObject[] seeMean3;
    public GameObject[] seeMean4;
    public GameObject[] seeMean5;
    public GameObject[] seeMean6;
    public GameObject[] seeMean7;
    public GameObject[] seeMean8;
    public GameObject[] seeMean9;
    public GameObject[] seeMean10;
    public GameObject[] seeMean11;


    // 스폰 스팟
    public Transform[] smSpot;

    float timer1;
    float timer2;
    float timer3;
    float maxTime1;
    float maxTime2;
    float maxTime3;

    private void Start()
    {
        maxTime1 = Random.Range(9.5f, 10.5f);
        maxTime2 = Random.Range(10f, 11f);
        maxTime3 = Random.Range(9.5f, 11f);
    }

    private void Update()
    {
        timer1 -= Time.deltaTime;
        timer2 -= Time.deltaTime;
        timer3 -= Time.deltaTime;
        if (timer1 < 0)
        {
            if(GameManager.instance.openButtonContorller.openCount > 6)
            {
                StartCoroutine(SMSpawn1(0));
                StartCoroutine(SMSpawn2(1));
                StartCoroutine(SMSpawn3(2));
                StartCoroutine(SMSpawn4(3));
                timer1 = maxTime1;
                return;
            }
            else if(GameManager.instance.openButtonContorller.openCount > 4)
            {
                StartCoroutine(SMSpawn1(0));
                StartCoroutine(SMSpawn3(2));
                StartCoroutine(SMSpawn4(3));
                timer1 = maxTime1;
                return;
            }
            else if(GameManager.instance.openButtonContorller.openCount > 2)
            {
                StartCoroutine(SMSpawn2(1));
                timer1 = maxTime1;
                return;
            }
            else
            {
                StartCoroutine(SMSpawn1(0));
                timer1 = maxTime1;
            }
        }
        if (timer2 < 0)
        {
            if (GameManager.instance.openButtonContorller.openCount > 6)
            {

                StartCoroutine(SMSpawn5(4));
                StartCoroutine(SMSpawn6(5));
                StartCoroutine(SMSpawn7(6));
                StartCoroutine(SMSpawn8(7));
                timer2 = maxTime2;
                return;
            }
            else if (GameManager.instance.openButtonContorller.openCount > 4)
            {
                StartCoroutine(SMSpawn7(6));
                StartCoroutine(SMSpawn8(7));
                timer2 = maxTime2;
                return;
            }
            else if (GameManager.instance.openButtonContorller.openCount > 2)
            {
                StartCoroutine(SMSpawn5(4));
                StartCoroutine(SMSpawn6(5));
                timer2 = maxTime2;
                return;
            }
            else
            {
                StartCoroutine(SMSpawn8(7));
                timer2 = maxTime2;
            }
        }
        if (timer3 < 0)
        {
            if (GameManager.instance.openButtonContorller.openCount > 6)
            {
                StartCoroutine(SMSpawn9(8));
                StartCoroutine(SMSpawn10(9));
                StartCoroutine(SMSpawn11(10));
                timer3 = maxTime3;
                return;
            }
            else if (GameManager.instance.openButtonContorller.openCount > 4)
            {
                StartCoroutine(SMSpawn10(9));
                StartCoroutine(SMSpawn11(10));
                timer3 = maxTime3;
                return;
            }
            else if (GameManager.instance.openButtonContorller.openCount > 2)
            {
                StartCoroutine(SMSpawn9(8));
                StartCoroutine(SMSpawn11(10));
                timer3 = maxTime3;
                return;
            }
            else
            {
                StartCoroutine(SMSpawn10(9));
                timer3 = maxTime3;
            }
        }
    }

    IEnumerator SMSpawn1(int num)
    {
        int rNum = Random.Range(0, seeMean1.Length);
        seeMean1[rNum].SetActive(true);
        seeMean1[rNum].transform.position = smSpot[num].position;
        yield return new WaitForSeconds(Random.Range(5f, 9f));
        seeMean1[rNum].SetActive(false);
    }
    IEnumerator SMSpawn2(int num)
    {
        int rNum = Random.Range(0, seeMean2.Length);
        seeMean2[rNum].SetActive(true);
        seeMean2[rNum].transform.position = smSpot[num].position;
        yield return new WaitForSeconds(Random.Range(5f, 9f));
        seeMean2[rNum].SetActive(false);
    }
    IEnumerator SMSpawn3(int num)
    {
        int rNum = Random.Range(0, seeMean3.Length);
        seeMean3[rNum].SetActive(true);
        seeMean3[rNum].transform.position = smSpot[num].position;
        yield return new WaitForSeconds(Random.Range(5f, 9f));
        seeMean3[rNum].SetActive(false);
    }
    IEnumerator SMSpawn4(int num)
    {
        int rNum = Random.Range(0, seeMean4.Length);
        seeMean4[rNum].SetActive(true);
        seeMean4[rNum].transform.position = smSpot[num].position;
        yield return new WaitForSeconds(Random.Range(5f, 9f));
        seeMean4[rNum].SetActive(false);
    }
    IEnumerator SMSpawn5(int num)
    {
        int rNum = Random.Range(0, seeMean5.Length);
        seeMean5[rNum].SetActive(true);
        seeMean5[rNum].transform.position = smSpot[num].position;
        yield return new WaitForSeconds(Random.Range(5f, 9f));
        seeMean5[rNum].SetActive(false);
    }
    IEnumerator SMSpawn6(int num)
    {
        int rNum = Random.Range(0, seeMean6.Length);
        seeMean6[rNum].SetActive(true);
        seeMean6[rNum].transform.position = smSpot[num].position;
        yield return new WaitForSeconds(Random.Range(5f, 9f));
        seeMean6[rNum].SetActive(false);
    }
    IEnumerator SMSpawn7(int num)
    {
        int rNum = Random.Range(0, seeMean7.Length);
        seeMean7[rNum].SetActive(true);
        seeMean7[rNum].transform.position = smSpot[num].position;
        yield return new WaitForSeconds(Random.Range(5f, 9f));
        seeMean7[rNum].SetActive(false);
    }
    IEnumerator SMSpawn8(int num)
    {
        int rNum = Random.Range(0, seeMean8.Length);
        seeMean8[rNum].SetActive(true);
        seeMean8[rNum].transform.position = smSpot[num].position;
        yield return new WaitForSeconds(Random.Range(5f, 9f));
        seeMean8[rNum].SetActive(false);
    }
    IEnumerator SMSpawn9(int num)
    {
        int rNum = Random.Range(0, seeMean9.Length);
        seeMean9[rNum].SetActive(true);
        seeMean9[rNum].transform.position = smSpot[num].position;
        yield return new WaitForSeconds(Random.Range(5f, 9f));
        seeMean9[rNum].SetActive(false);
    }
    IEnumerator SMSpawn10(int num)
    {
        int rNum = Random.Range(0, seeMean10.Length);
        seeMean10[rNum].SetActive(true);
        seeMean10[rNum].transform.position = smSpot[num].position;
        yield return new WaitForSeconds(Random.Range(5f, 9f));
        seeMean10[rNum].SetActive(false);
    }
    IEnumerator SMSpawn11(int num)
    {
        int rNum = Random.Range(0, seeMean11.Length);
        seeMean11[rNum].SetActive(true);
        seeMean11[rNum].transform.position = smSpot[num].position;
        yield return new WaitForSeconds(Random.Range(5f, 9f));
        seeMean11[rNum].SetActive(false);
    }
}
