using UnityEngine.UI;
using UnityEngine;

public class HUDManager : MonoBehaviour {

    public static HUDManager instance;

    public Button NestSailor1;
    public Button RubberSailor1;
    public Button LeftCannonSailor1;
    public Button RightCannonSailor1;

    public Button NestSailor2;
    public Button RubberSailor2;
    public Button LeftCannonSailor2;
    public Button RightCannonSailor2;

    public Color ActivePositionSailor1;
    public Color ActivePositionSailor2;
    
    Color ResetColorSailor1;
    Color ResetColorSailor2;

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

    private void Start()
    {
        ResetColorSailor1 = PlayerController.instance.Sailor1;
        ResetColorSailor2 = PlayerController.instance.Sailor2;

        ColorBlock tmp = NestSailor1.colors;

        tmp.pressedColor = ActivePositionSailor1;
        tmp.highlightedColor = ActivePositionSailor1;
        NestSailor1.colors = tmp;
        RubberSailor1.colors = tmp;
        LeftCannonSailor1.colors = tmp;
        RightCannonSailor1.colors = tmp;

        tmp.pressedColor = ActivePositionSailor2;
        tmp.highlightedColor = ActivePositionSailor2;
        NestSailor2.colors = tmp;
        RubberSailor2.colors = tmp;
        LeftCannonSailor2.colors = tmp;
        RightCannonSailor2.colors = tmp;

    }

    public void UpdateUI()
    {
        cleanButtons();

        switch (PlayerController.instance._stateMachine.getActualState1())
        {
            case SMPlayer.States.LeftCannon:
                {
                    ColorBlock tmp = LeftCannonSailor1.colors;

                    tmp.normalColor = ActivePositionSailor1;
                    LeftCannonSailor1.colors = tmp;
                }
                break;
            case SMPlayer.States.RightCannon:
                {
                    ColorBlock tmp = LeftCannonSailor1.colors;

                    tmp.normalColor = ActivePositionSailor1;
                    RightCannonSailor1.colors = tmp;
                }
                break;
            case SMPlayer.States.Steering:
                {
                    ColorBlock tmp = LeftCannonSailor1.colors;

                    tmp.normalColor = ActivePositionSailor1;
                    RubberSailor1.colors = tmp;
                }
                break;
            case SMPlayer.States.OnNest:
                {
                    ColorBlock tmp = LeftCannonSailor1.colors;

                    tmp.normalColor = ActivePositionSailor1;
                    NestSailor1.colors = tmp;
                }
                break;
            case SMPlayer.States.Iddle:
                break;
            default:
                break;
        }

        switch (PlayerController.instance._stateMachine.getActualState2())
        {
            case SMPlayer.States.LeftCannon:
                {
                    ColorBlock tmp = LeftCannonSailor1.colors;

                    tmp.normalColor = ActivePositionSailor2;
                    LeftCannonSailor2.colors = tmp;
                }
                break;
            case SMPlayer.States.RightCannon:
                {
                    ColorBlock tmp = LeftCannonSailor1.colors;

                    tmp.normalColor = ActivePositionSailor2;
                    RightCannonSailor2.colors = tmp;                    
                }
                break;
            case SMPlayer.States.Steering:
                {
                    ColorBlock tmp = LeftCannonSailor1.colors;

                    tmp.normalColor = ActivePositionSailor2;
                    RubberSailor2.colors = tmp;
                }
                break;
            case SMPlayer.States.OnNest:
                {
                    ColorBlock tmp = LeftCannonSailor1.colors;

                    tmp.normalColor = ActivePositionSailor2;
                    NestSailor2.colors = tmp;
                }
                break;
            case SMPlayer.States.Iddle:
                break;
            default:
                break;
        }
    }

    void cleanButtons()
    {
        ColorBlock tmp = NestSailor1.colors;
        tmp.normalColor = ResetColorSailor1;
        
        NestSailor1.colors = tmp;
        RubberSailor1.colors = tmp;
        LeftCannonSailor1.colors = tmp;
        RightCannonSailor1.colors = tmp;

        tmp.normalColor = ResetColorSailor2;
        NestSailor2.colors = tmp;
        RubberSailor2.colors = tmp;
        LeftCannonSailor2.colors = tmp;
        RightCannonSailor2.colors = tmp;
}
}
