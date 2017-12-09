using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChestCountOnButton : MonoBehaviour
{

    public int level;
    public Button button;
    public Text countText;

    private void Update()
    {
        if (button.IsInteractable())
        {
            countText.gameObject.SetActive(true);
			countText.text = PlayerPrefs.GetInt("Level"+(level - 1)).ToString() + "/3"; //LevelControlScript.instance.startsPerLevel[level - 1].ToString() + "/3";
        }
        else
        {
            countText.gameObject.SetActive(false);
        }
    }
}
