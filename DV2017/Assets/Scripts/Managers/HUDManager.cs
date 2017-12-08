using UnityEngine.UI;
using UnityEngine;

public class HUDManager : MonoBehaviour {

    public static HUDManager instance;

    public Button NestSailor1;
    public Button RubberSailor1;
    public Button LeftCannonSailor1;
    public Button RightCannonSailor1;
    public Button frontCannonButton1;

    public Button NestSailor2;
    public Button RubberSailor2;
    public Button LeftCannonSailor2;
    public Button RightCannonSailor2;
    public Button frontCannonButton2;

    public Color ActivePositionSailor1;
    public Color ActivePositionSailor2;
    public Color DisablePositionSailor1;
    public Color DisablePositionSailor2;

    public Color StandByColorButton;

    public ColorBlock aux;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        cleanButtons();
    }

    public void UpdateUI()
    {
        cleanButtons();

        switch (PlayerController.instance._stateMachine.getActualState1())
        {
            case SMPlayer.States.FrontCannon:
                {
                    ColorBlock tmp = aux;

                    tmp.normalColor = ActivePositionSailor1;
                    tmp.highlightedColor = ActivePositionSailor1;
                    frontCannonButton1.colors = tmp;

                    tmp.disabledColor = DisablePositionSailor1;               
                    frontCannonButton2.colors = tmp;
                    frontCannonButton2.interactable = false;

                }
                break;
            case SMPlayer.States.LeftCannon:
                {
                    ColorBlock tmp = aux;

                    tmp.normalColor = ActivePositionSailor1;
                    tmp.highlightedColor = ActivePositionSailor1;
                    LeftCannonSailor1.colors = tmp;

                    tmp.disabledColor = DisablePositionSailor1;
                    LeftCannonSailor2.colors = tmp;
                    LeftCannonSailor2.interactable = false;
                }
                break;
            case SMPlayer.States.RightCannon:
                {
                    ColorBlock tmp = aux;

                    tmp.normalColor = ActivePositionSailor1;
                    tmp.highlightedColor = ActivePositionSailor1;
                    RightCannonSailor1.colors = tmp;

                    tmp.disabledColor = DisablePositionSailor1;
                    RightCannonSailor2.colors = tmp;
                    RightCannonSailor2.interactable = false;
                }
                break;
            case SMPlayer.States.Steering:
                {
                    ColorBlock tmp = aux;

                    tmp.normalColor = ActivePositionSailor1;
                    tmp.highlightedColor = ActivePositionSailor1;
                    RubberSailor1.colors = tmp;

                    tmp.disabledColor = DisablePositionSailor1;
                    RubberSailor2.colors = tmp;
                    RubberSailor2.interactable = false;
                }
                break;
            case SMPlayer.States.OnNest:
                {
                    ColorBlock tmp = aux;

                    tmp.normalColor = ActivePositionSailor1;
                    tmp.highlightedColor = ActivePositionSailor1;
                    NestSailor1.colors = tmp;

                    tmp.disabledColor = DisablePositionSailor1;
                    NestSailor2.colors = tmp;
                    NestSailor2.interactable = false;
                }
                break;
            case SMPlayer.States.Iddle:
                break;
            default:
                break;
        }

        switch (PlayerController.instance._stateMachine.getActualState2())
        {
            case SMPlayer.States.FrontCannon:
                {
                    ColorBlock tmp = aux;

                    tmp.normalColor = ActivePositionSailor2;
                    tmp.highlightedColor = ActivePositionSailor2;
                    frontCannonButton2.colors = tmp;

                    tmp.disabledColor = DisablePositionSailor2;
                    frontCannonButton1.colors = tmp;
                    frontCannonButton1.interactable = false;
                }
                break;
            case SMPlayer.States.LeftCannon:
                {
                    ColorBlock tmp = aux;

                    tmp.normalColor = ActivePositionSailor2;
                    tmp.highlightedColor = ActivePositionSailor2;
                    LeftCannonSailor2.colors = tmp;

                    tmp.disabledColor = DisablePositionSailor2;
                    LeftCannonSailor1.colors = tmp;
                    LeftCannonSailor1.interactable = false;
                }
                break;
            case SMPlayer.States.RightCannon:
                {
                    ColorBlock tmp = aux;

                    tmp.normalColor = ActivePositionSailor2;
                    tmp.highlightedColor = ActivePositionSailor2;
                    RightCannonSailor2.colors = tmp;

                    tmp.disabledColor = DisablePositionSailor2;
                    RightCannonSailor1.colors = tmp;
                    RightCannonSailor1.interactable = false;
                }
                break;
            case SMPlayer.States.Steering:
                {
                    ColorBlock tmp = aux;

                    tmp.normalColor = ActivePositionSailor2;
                    tmp.highlightedColor = ActivePositionSailor2;
                    RubberSailor2.colors = tmp;

                    tmp.disabledColor = DisablePositionSailor2;
                    RubberSailor1.colors = tmp;
                    RubberSailor1.interactable = false;
                }
                break;
            case SMPlayer.States.OnNest:
                {
                    ColorBlock tmp = aux;

                    tmp.normalColor = ActivePositionSailor2;
                    tmp.highlightedColor = ActivePositionSailor2;
                    NestSailor2.colors = tmp;

                    tmp.disabledColor = DisablePositionSailor2;
                    NestSailor1.colors = tmp;
                    NestSailor1.interactable = false;
                }
                break;
            case SMPlayer.States.Iddle:
                break;
            default:
                break;
        }
    }

    public void cleanButtons()
    {
        ColorBlock tmp = aux;
        
        NestSailor1.colors = tmp;
        RubberSailor1.colors = tmp;
        LeftCannonSailor1.colors = tmp;
        RightCannonSailor1.colors = tmp;
        frontCannonButton1.colors = tmp;
        NestSailor2.colors = tmp;
        RubberSailor2.colors = tmp;
        LeftCannonSailor2.colors = tmp;
        RightCannonSailor2.colors = tmp;
        frontCannonButton2.colors = tmp;

        NestSailor1.interactable = true;
        RubberSailor1.interactable = true;
        LeftCannonSailor1.interactable = true;
        RightCannonSailor1.interactable = true;
        frontCannonButton1.interactable = true;
        NestSailor2.interactable = true;
        RubberSailor2.interactable = true;
        LeftCannonSailor2.interactable = true;
        RightCannonSailor2.interactable = true;
        frontCannonButton2.interactable = true;
    }
}
