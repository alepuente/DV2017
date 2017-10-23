using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

    public static CameraController instance;
    
    public GameObject closePos;
    public GameObject farPos;
    public GameObject menuPos;

    public Camera followCamera;
    public Camera menuCamera;
    
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
    
    void Start () {
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
            followCamera.gameObject.SetActive(false);
            menuCamera.gameObject.SetActive(true);

            menuCamera.gameObject.transform.position = menuPos.transform.position;
            actualPos = pos;
        }
        else if (pos == CameraPositions.Close)
        {
            followCamera.gameObject.SetActive(true);
            menuCamera.gameObject.SetActive(false);

            followCamera.gameObject.transform.position = new Vector3(followCamera.gameObject.transform.position.x, closePos.transform.position.y, followCamera.gameObject.transform.position.z);
            actualPos = pos;
        }
        else if (pos == CameraPositions.Far)
        {
            followCamera.gameObject.SetActive(true);
            menuCamera.gameObject.SetActive(false);

            followCamera.gameObject.transform.position = new Vector3(followCamera.gameObject.transform.position.x, farPos.transform.position.y, followCamera.gameObject.transform.position.z);
            actualPos = pos;
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

        if (actualPos == CameraPositions.Close || actualPos == CameraPositions.Far)
        {
            followCamera.gameObject.transform.position = new Vector3(PlayerController.instance.gameObject.transform.position.x, followCamera.transform.position.y, PlayerController.instance.gameObject.transform.position.z);
        }
    }
}
