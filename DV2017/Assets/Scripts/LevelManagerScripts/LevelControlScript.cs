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

        currentLevelStarsCount = 3 - GameObject.FindGameObjectsWithTag("Chest").Length;
        if (currentLevelStarsCount > 3) currentLevelStarsCount = 3;
        if (currentLevelStarsCount < 0) currentLevelStarsCount = 0;

        if (startsPerLevel[currentLevel - 1] < currentLevelStarsCount) startsPerLevel[currentLevel - 1] = currentLevelStarsCount;


        Invoke("loadMainMenu", 1f);
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
