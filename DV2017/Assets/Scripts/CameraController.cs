using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{

    public static CameraController instance;

    public GameObject closePos;
    public GameObject farPos;
    public GameObject menuPos;

    public Camera mainCamera;

    private CameraPositions actualPos;

    public bool nest;

    public Button nestButton;

    public enum CameraPositions
    {
        Close,
        Far,
        Menu
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        changeCameraPos(CameraPositions.Menu);
    }

    public void changeToClose()
    {
        changeCameraPos(CameraPositions.Close);
    }
    public void changeToFar()
    {
        changeCameraPos(CameraPositions.Far);
    }
    public void changeToMenu()
    {
        changeCameraPos(CameraPositions.Menu);
    }

    public void changeCameraPos(CameraPositions pos)
    {
        if (pos == CameraPositions.Menu)
        {
            mainCamera.gameObject.transform.position = menuPos.transform.position;
            mainCamera.gameObject.transform.rotation = menuPos.transform.rotation;
        }
        else if (pos == CameraPositions.Close)
        {
            mainCamera.gameObject.transform.position = new Vector3(mainCamera.gameObject.transform.position.x, closePos.transform.position.y, mainCamera.gameObject.transform.position.z);
            mainCamera.gameObject.transform.rotation = closePos.transform.rotation;
        }
        else if (pos == CameraPositions.Far)
        {
            mainCamera.gameObject.transform.position = new Vector3(mainCamera.gameObject.transform.position.x, farPos.transform.position.y, mainCamera.gameObject.transform.position.z);
            mainCamera.gameObject.transform.rotation = farPos.transform.rotation;
        }
    }

    private void Update()
    {
        if (nest)
        {
            nestButton.interactable = true;
        }
        else
        {
            nestButton.interactable = false;
        }
        if (PlayerController.instance != null)
        {
            if (actualPos == CameraPositions.Close || actualPos == CameraPositions.Far)
            {
                mainCamera.gameObject.transform.position = new Vector3(PlayerController.instance.gameObject.transform.position.x, mainCamera.transform.position.y, PlayerController.instance.gameObject.transform.position.z);
            }
        }
    }
}
