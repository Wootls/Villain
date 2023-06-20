using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float zoomStep, minCamSize, maxCamSize;

    [SerializeField]
    private SpriteRenderer mapRenderere;

    private float mapMinX, mapMaxX, mapMinY, mapMaxY;

    private Vector3 dragOrigin;

    //
    public GameObject tipPrefab;
    float spawnsTime;
    public float defaultTime = 0.05f;


    private void Awake()
    {
        mapMinX = mapRenderere.transform.position.x - mapRenderere.bounds.size.x / 2f;
        mapMaxX = mapRenderere.transform.position.x + mapRenderere.bounds.size.x / 2f;

        mapMinY = mapRenderere.transform.position.y - mapRenderere.bounds.size.y / 2f;
        mapMaxY = mapRenderere.transform.position.y + mapRenderere.bounds.size.y / 2f;

    }

    private void Update()
    {
        PanCamera();
    }

    private void PanCamera()
    {
        //save position of mouse in world space when drag starts (first time clicked)
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
            TipCreate();
        }
        spawnsTime += Time.deltaTime;

        //calculate distance between drag origin ans new position if it is still held down
        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            print("origin " + dragOrigin + " newPosition " + cam.ScreenToWorldPoint(Input.mousePosition) + " =difference" + difference);

            //move the camera by that distance
            cam.transform.position = ClampCamera(cam.transform.position + difference);
        }
    }

    public void ZoomIn()
    {
        float newSize = cam.orthographicSize - zoomStep;
        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);

        cam.transform.position = ClampCamera(cam.transform.position);
    }

    public void ZoomOut()
    {
        float newSize = cam.orthographicSize + zoomStep;
        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);

        cam.transform.position = ClampCamera(cam.transform.position);
    }


    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }

    void TipCreate()
    {
        Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mPosition.z = 0;
        Instantiate(tipPrefab, mPosition, Quaternion.identity);
        GameManager.instance.touchMoney += 100;
    }
}
