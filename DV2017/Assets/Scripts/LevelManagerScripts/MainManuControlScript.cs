using UnityEngine;
using UnityEngine.UI;

public class MainManuControlScript : MonoBehaviour {

	public Button[] levelButtonsArray;
    
	void Update () {
        resetButtons();
    }
	
	public void levelToLoad (int level)
	{
		LevelControlScript.instance.LoadLevelAsIndex(level);
	}

	public void resetButtons()
	{
        for (int i = 0; i < levelButtonsArray.Length; i++)
        {
                levelButtonsArray[i].interactable = true;
            if (LevelControlScript.instance.levelPassed < i + 1)
                levelButtonsArray[i].interactable = false;
        }
	}
}
