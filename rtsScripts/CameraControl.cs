using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{
    //moveCamera
    public float cameraSpeed;
    [Range(0,1)]
    float zoomLerp = 0.7f;
    float shiftSpeed;
    float cursorTreshold = 0.03f;
    new Camera camera;
    Vector2 mousePosition;
    Vector2 mousePositionOnScreen;
    Vector2 keyboardInput;
    Vector2 mouseScroll;
    bool isCursorInGame;
    //zoomCamera
    public float zoomSpeed, groundHeight;
    public Vector2 cameraHeight;
    //selection
    RectTransform selectionBox;
    Rect selectionRect, boxRect;

    //selection list
    List<Unit> selectedUnits = new List<Unit>();
    public GameObject[] EnemyList;

    void Awake()
    {
        selectionBox = GetComponentInChildren<Image>().transform as RectTransform;
        camera = GetComponent<Camera>();
        shiftSpeed = 1;
        selectionBox.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();
        CameraZoom();
        MouseHit();
        enemyList();
    }
    void CameraMovement()
    {
        keyboardInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        mousePosition = Input.mousePosition;
        mousePositionOnScreen = camera.ScreenToViewportPoint(mousePosition);
        isCursorInGame = mousePositionOnScreen.x >= 0 &&
             mousePositionOnScreen.x <= 1 &&
             mousePositionOnScreen.y >= 0 &&
             mousePositionOnScreen.y <= 1;

        Vector2 cameraMovement = keyboardInput;

        if (isCursorInGame)
        {
            if (mousePositionOnScreen.x < cursorTreshold) cameraMovement.x -= 1 - mousePositionOnScreen.x / cursorTreshold;
            if (mousePositionOnScreen.x > 1-cursorTreshold) cameraMovement.x += 1 - (1- mousePositionOnScreen.x) / (1- cursorTreshold);
            if (mousePositionOnScreen.y < cursorTreshold) cameraMovement.y -= 1 - mousePositionOnScreen.y / cursorTreshold;
            if (mousePositionOnScreen.y > 1- cursorTreshold) cameraMovement.y += 1 - (1 - mousePositionOnScreen.y) / (1 - cursorTreshold);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) shiftSpeed = 2;
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift)) shiftSpeed = 1;
        
        transform.position += new Vector3(-cameraMovement.y, 0, cameraMovement.x) * cameraSpeed * shiftSpeed * Time.deltaTime;
    }
    void CameraZoom()
    {

        mouseScroll = Input.mouseScrollDelta;
        float zoomDelta = mouseScroll.y * zoomSpeed * Time.deltaTime;
        zoomLerp = Mathf.Clamp01(zoomLerp + zoomDelta);

        var position = transform.position;
        position.y = Mathf.Lerp(cameraHeight.y, cameraHeight.x, zoomLerp) + groundHeight;
        transform.position = position;
    }

    void MouseHit()
    {
       if (Input.GetMouseButtonDown(0))
        {
           // buldingClick();
            selectionBox.gameObject.SetActive(true);
            selectionRect.position = mousePosition;
        }    
       else if (Input.GetMouseButtonUp(0))
        {
            selectionBox.gameObject.SetActive(false);
            
        }
       if (Input.GetMouseButton(0))
        {
            selectionRect.size = mousePosition - selectionRect.position;
            boxRect = AbsRect(selectionRect);
            selectionBox.anchoredPosition = boxRect.position;
            selectionBox.sizeDelta = boxRect.size;
            UpdateSelecting();
        }
       if (Input.GetMouseButton(1))
        {
            giveCommand();
        }
    }
    Rect AbsRect(Rect rect)
    {
        if(rect.width < 0)
        {
            rect.x += rect.width;
            rect.width *= -1;
        }
        if (rect.height < 0)
        {
            rect.y += rect.height;
            rect.height *= -1;
        }
        return rect;
    }
    void UpdateSelecting()
    {
        selectedUnits.Clear();
        foreach(Unit unit in Unit.SelectableUnits)
        {
            if (!unit) continue;//???????????????????????????????CO TO JEST DOKLADNIE????????
            var position = unit.transform.position;
            var positionScreen = camera.WorldToScreenPoint(position);
            bool inRect = isPointInRect(boxRect, positionScreen);
            (unit as ISelectable).SetSelected(inRect);
            if (inRect) selectedUnits.Add(unit);
        }
    }
   /* void buldingClick()
    {
        ray = camera.ViewportPointToRay(mousePositionOnScreen);
        if (Physics.Raycast(ray, out rayHit, 1000, BuildingLayerMask))
        {
            if (rayHit.collider.tag == "PlayerBase")
            {
                rayHit.collider.GetComponent<ActiveBuilding>().activeMe();
            }
        }
    }*/
    bool isPointInRect(Rect rect, Vector2 point)
    {

        return point.x >= rect.position.x && point.x <= (rect.position.x + rect.size.x) &&
            point.y >= rect.position.y && point.y <= (rect.position.y + rect.size.y);

    }
    Ray ray;
    RaycastHit rayHit;
    [SerializeField]
    LayerMask commandLayerMask;// BuildingLayerMask;
    void giveCommand()
    {
        ray = camera.ViewportPointToRay(mousePositionOnScreen);
        if(Physics.Raycast(ray, out rayHit, 1000, commandLayerMask))
        {
            object commandData = null;
            if(rayHit.collider is TerrainCollider)
            {
                commandData = rayHit.point;
            }
            else if(rayHit.collider.tag == "Enemy")
            {
                commandData = rayHit.collider.gameObject;
            }
            /*else
            {
                commandData = rayHit.collider.gameObject.GetComponent<Unit>();
            }*/
            giveCommand(commandData);
        }

    }
    void giveCommand(object dataCommand)
    {
        foreach (Unit unit in selectedUnits)
            unit.SendMessage("Command", dataCommand, SendMessageOptions.DontRequireReceiver);
    }
    public void enemyList()
    {
        EnemyList = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject oneUnit in EnemyList);

    }
}//end
