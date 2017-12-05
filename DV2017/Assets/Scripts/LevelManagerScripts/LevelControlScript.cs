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


    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(gameObject);                
    }

    public void youWin()
    {
        Debug.Log("<color=green>YOU WIN!</color>");
        if(currentLevel >= levelPassed)
            levelPassed++;

        Invoke("loadMainMenu", 1f);
    }

    public void youLose()
    {
        Debug.Log("<color=red>YOU LOSE!</color>");
        Invoke("loadMainMenu", 1f);
    }

    void loadMainMenu()
    {
        SceneManager.UnloadSceneAsync(levels[currentLevel - 1]);
        GameManager.instance.resetGame();
    }

    public void LoadLevelAsIndex(int index)
    {
        currentLevel = index;
        SceneManager.LoadScene(levels[currentLevel - 1], LoadSceneMode.Additive);
    }
}
