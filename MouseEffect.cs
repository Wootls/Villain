using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEffect : MonoBehaviour
{
    public GameObject tipPrefab;
    float spawnsTime;
    public float defaultTime = 0.05f;

    private void Update()
    {
        if (Input.GetMouseButton(0) && spawnsTime >= defaultTime)
        {
            TipCreate();
            spawnsTime = 0;
        }
        spawnsTime += Time.deltaTime;
    }

    void TipCreate()
    {
        Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mPosition.z = 0;
        Instantiate(tipPrefab, mPosition, Quaternion.identity);
        GameManager.instance.touchMoney += 100;
    }

}
