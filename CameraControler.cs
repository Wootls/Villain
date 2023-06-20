using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public SpriteRenderer mapRenderere;
    private Vector3 dragOrigin;
    private float mapMinX, mapMaxX, mapMinY, mapMaxY;

    const float max_X = 5;
    const float min_x = -5;
    const float max_y = 5;
    const float min_y = -5;
    const float fScale = 0.01f;
    const float zoomSpeed = 0.01f;

    public Camera cam;

    Vector2 vScale = new Vector2(fScale, fScale);
    Vector2 startPos;
    Vector2 curPos;
    Vector2 change;
    bool hold = false;

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
        //swipe
        //if(Input.touchCount == 1)
        
        if (Input.GetMouseButtonDown(0))
        {
            this.hold = true;
            this.startPos = Input.mousePosition;
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
            TipCreate();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            this.hold = false;
        }
        if (this.hold)
        {
            this.curPos = Input.mousePosition;
            transform.Translate((cam.orthographicSize / 5) * vScale * (startPos - curPos));
            if (OutSide())
                transform.Translate((cam.orthographicSize / 5) * vScale * (curPos - startPos));
            startPos = curPos;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            //print("origin " + dragOrigin + " newPosition " + cam.ScreenToWorldPoint(Input.mousePosition) + " =difference" + difference);

            //move the camera by that distance
            cam.transform.position = ClampCamera(cam.transform.position + difference);
        }


        //zoom
        if (Input.touchCount > 1)
        {
            //store both touches
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            //check position
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            //check deltas (original current)
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            //check how much zoom in / out
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            //zoom camera
            float newSize = cam.orthographicSize + (deltaMagnitudeDiff * zoomSpeed);
            newSize = Mathf.Max(newSize, 1f);
            newSize = Mathf.Min(newSize, 6f);
            cam.orthographicSize = newSize;
        }

    }

    //check if outside the boundary
    bool OutSide()
    {
        if (transform.position.x > max_X || transform.position.x < min_x || transform.position.y > max_y || transform.position.y < min_y)
            return true;
        else return false;
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
        GameManager.instance.TouchMoney(1);
        GameManager.instance.soundManagement.coinSound.Play();
    }
}
