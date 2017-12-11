using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{

    public static CameraController instance;

    public GameObject closePos;
    public GameObject farPos;
    public GameObject menuPos;

    public Camera menuCamera;
	public Camera gameCamera;

    public CameraPositions actualPos;

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
			gameCamera.gameObject.SetActive (false);
			menuCamera.gameObject.SetActive (true);
            actualPos = CameraPositions.Menu;
			menuCamera.gameObject.transform.position = menuPos.transform.position;
			menuCamera.gameObject.transform.rotation = menuPos.transform.rotation;
        }
        else if (pos == CameraPositions.Close)
        {
			gameCamera.gameObject.SetActive (true);
			menuCamera.gameObject.SetActive (false);
            actualPos = CameraPositions.Close;
			gameCamera.gameObject.transform.position = new Vector3(gameCamera.gameObject.transform.position.x, closePos.transform.position.y, gameCamera.gameObject.transform.position.z);
			gameCamera.gameObject.transform.rotation = closePos.transform.rotation;
        }
        else if (pos == CameraPositions.Far)
        {
			gameCamera.gameObject.SetActive (true);
			menuCamera.gameObject.SetActive (false);
            actualPos = CameraPositions.Far;
			gameCamera.gameObject.transform.position = new Vector3(gameCamera.gameObject.transform.position.x, farPos.transform.position.y, gameCamera.gameObject.transform.position.z);
			gameCamera.gameObject.transform.rotation = farPos.transform.rotation;
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
				gameCamera.gameObject.transform.position = new Vector3(PlayerController.instance.gameObject.transform.position.x, gameCamera.transform.position.y, PlayerController.instance.gameObject.transform.position.z);
            }       
            
        }
    }
}
