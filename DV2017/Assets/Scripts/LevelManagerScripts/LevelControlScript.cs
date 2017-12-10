using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelControlScript : MonoBehaviour
{
    public static LevelControlScript instance = null;

	public int levelPassed = 1;
    public int currentLevel = 1;

    public string[] levels;
    public int[] startsPerLevel;

    public int currentLevelStarsCount = 0;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(gameObject);

		if ( PlayerPrefs.GetInt ("LevelsPassed") == 0) {
			levelPassed = 1;
		} else {
			levelPassed = PlayerPrefs.GetInt ("LevelsPassed");	
		}
        startsPerLevel = new int[levels.Length];
        for (int i = 0; i < startsPerLevel.Length; i++)
        {
            startsPerLevel[i] = 0;
        }  
    }

    public void youWin()
    {
        Debug.Log("<color=green>YOU WIN!</color>");

		if(currentLevel >= levelPassed)
            levelPassed++;
		PlayerPrefs.SetInt ("LevelsPassed", levelPassed);

        currentLevelStarsCount = 3 - GameObject.FindGameObjectsWithTag("Chest").Length;
        if (currentLevelStarsCount > 3) currentLevelStarsCount = 3;
        if (currentLevelStarsCount < 0) currentLevelStarsCount = 0;

        if (startsPerLevel[currentLevel - 1] < currentLevelStarsCount) startsPerLevel[currentLevel - 1] = currentLevelStarsCount;
		if (PlayerPrefs.GetInt("Level" + (currentLevel - 1)) < startsPerLevel [currentLevel - 1]) {			
			PlayerPrefs.SetInt ("Level" + (currentLevel - 1), startsPerLevel [currentLevel - 1]);
			PlayerPrefs.Save();
		}

        loadMainMenu();
    }

    void loadMainMenu()
    {
        GameManager.instance.resetGame();
    }

    public void LoadLevelAsIndex(int index)
    {
        currentLevel = index;
        SceneManager.LoadScene(levels[currentLevel - 1], LoadSceneMode.Additive);
    }

    public void ClearLevels()
    {
        currentLevelStarsCount = 0;

        if(SceneManager.GetSceneByName(levels[currentLevel - 1]).buildIndex != -1)
            SceneManager.UnloadSceneAsync(levels[currentLevel - 1]);
    }
}
