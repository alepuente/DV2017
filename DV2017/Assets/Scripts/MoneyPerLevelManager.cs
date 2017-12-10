using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyPerLevelManager : MonoBehaviour {
    public static MoneyPerLevelManager instance;

    public int CurrentMoney = 0;
    public GameObject finishBoard;

    public Text chestGottedText;
    public Text currentGoldText;
    public Text totalEarnedText;

    public Text currentMoneyText;

    private void Update()
    {
        currentMoneyText.text = "Current gold : " + CurrentMoney.ToString();
    }

    private void OnEnable()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void FinishLevel()
    {
        Time.timeScale = 1;
        GameManager.instance.money += CurrentMoney * ChestGottedCount();
        GameManager.instance.resetGame();
        LevelControlScript.instance.youWin();
        CurrentMoney = 0;
    }

    int ChestGottedCount()
    {
        int tmp = 3 - GameObject.FindGameObjectsWithTag("Chest").Length;

        if (tmp < 0) return 0;
        //if (tmp > 3) return 3;

        return tmp;
    }

    public void ShowFinishBoard()
    {
        finishBoard.SetActive(true);

        chestGottedText.text = "Chest Found : " + ChestGottedCount();
        currentGoldText.text = "Current Gold : " + CurrentMoney;
        totalEarnedText.text = "Total Earned : " + CurrentMoney * ChestGottedCount();

        Time.timeScale = 0;
    }

}
