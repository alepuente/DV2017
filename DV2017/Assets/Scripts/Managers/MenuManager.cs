﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Menu,
    Game,
    Pause,
    Tutorial
}

public class MenuManager : MonoBehaviour
{

    public static MenuManager instance;

    public GameObject menuHUD;
    public GameObject gameHUD;
    public GameObject pauseHUD;

    public Text moneyText;

    public Text damageAmountText;
    public Button damageButton;
    public int damagePrice;
    public float damageAmount;    

    public Text speedAmountText;
    public Button speedButton;
    public int speedPrice;
    public float speedAmount;

    public Text turnAmountText;
    public Button turnButton;
    public int turnPrice;
    public float turnAmount;

    public Text healthAmountText;
    public Button healthButton;
    public int healthPrice;
    public float healthAmount;

    public Text enemiesLeft;

    public GameState gameState;

    [SerializeField]
    int SpeedUpgradeCount;
    [SerializeField]
    int SteeringUpgradeCount;
    [SerializeField]
    int CannonDamageUpgradeCount;
    [SerializeField]
    int HealthUpgradeCount;

    // Use this for initialization
    void Start()
    {
        instance = this;
        StartMenu();

        moneyText.text = "Gold: "+GameManager.instance.money.ToString();
        damageButton.GetComponentInChildren<Text>().text = damagePrice.ToString();
        damageAmountText.text = "+" + damageAmount + " Damage";

        speedButton.GetComponentInChildren<Text>().text = speedPrice.ToString();
        speedAmountText.text = "+" + speedAmount + " Speed";

        turnButton.GetComponentInChildren<Text>().text = turnPrice.ToString();
        turnAmountText.text = "+" + turnAmount + " Turn Speed";

        healthButton.GetComponentInChildren<Text>().text = healthPrice.ToString();
        healthAmountText.text = "+" + healthAmount + " Max Health";

        SpeedUpgradeCount = 0;
        SteeringUpgradeCount = 0;
        CannonDamageUpgradeCount = 0;
        HealthUpgradeCount = 0;
        
    }

    private void Update()
    {
        if (GameManager.instance.money >= damagePrice && CannonDamageUpgradeCount < 3) { damageButton.interactable = true; }
        else { damageButton.interactable = false; }

        if (GameManager.instance.money >= speedPrice && SpeedUpgradeCount < 3) { speedButton.interactable = true; }
        else { speedButton.interactable = false; }

        if (GameManager.instance.money >= turnPrice && SteeringUpgradeCount < 3) { turnButton.interactable = true; }
        else { turnButton.interactable = false; }

        if (GameManager.instance.money >= healthPrice && HealthUpgradeCount < 3) { healthButton.interactable = true; }
        else { healthButton.interactable = false; }

        enemiesLeft.text = "Enemies Left: " + EnemyFactory.instance.enemiesAlive;
        moneyText.text = "Gold: " + GameManager.instance.money.ToString();
    }


    #region MainMenu


    public void StartMenu()
    {
        gameState = GameState.Menu;
        gameHUD.SetActive(false);
        menuHUD.SetActive(true);
        pauseHUD.SetActive(false);
    }
    public void StartGame()
    {
        gameState = GameState.Game;
        PlayerManager._instance.SpawnPlayer();
        CameraController.instance.changeToClose();
        menuHUD.SetActive(false);
        gameHUD.SetActive(true);
        pauseHUD.SetActive(false);
        SpawnManager.instance.canSpawn = true;
        GameManager.instance.setStartGamePlayerPos();
    }
    public void Pause()
    {
        pauseHUD.SetActive(true);
        gameHUD.SetActive(false);
        menuHUD.SetActive(false);
        gameState = GameState.Pause;
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        pauseHUD.SetActive(false);
        gameHUD.SetActive(true);
        menuHUD.SetActive(false);
        gameState = GameState.Game;
        Time.timeScale = 1;
    }

    public void ToMenu()
    {
        Time.timeScale = 1;
        GameManager.instance.resetGame();
    }
    public void Exit()
    {
        Application.Quit();
    }
    #endregion


    #region Upgrades
    public void upgradeCannonDamage()
    {
        GameManager.instance.money -= damagePrice;
        GameManager.instance.updateDamage("Player", damageAmount);
        damageAmount += 1;
        damagePrice += 200;
        damageButton.GetComponentInChildren<Text>().text = damagePrice.ToString();
        damageAmountText.text = "+" + damageAmount + " Damage";
        CannonDamageUpgradeCount++;
    }
    public void upgradeSpeed()
    {
        GameManager.instance.money -= speedPrice;
        GameManager.instance.updateSpeed("Player", speedAmount);
        speedAmount += .3f;
        speedPrice += 150;
        speedButton.GetComponentInChildren<Text>().text = speedPrice.ToString();
        speedAmountText.text = "+" + speedAmount + " Speed";
        SpeedUpgradeCount++;
    }
    public void upgradeSteering()
    {
        GameManager.instance.money -= turnPrice;
        GameManager.instance.updateSteering("Player", turnAmount);
        turnAmount += 0.05f;
        turnPrice += 100;
        turnButton.GetComponentInChildren<Text>().text = turnPrice.ToString();
        turnAmountText.text = "+" + turnAmount + " Turn Speed";
        SteeringUpgradeCount++;
    }
    public void upgradeHealth()
    {
        GameManager.instance.money -= healthPrice;
        GameManager.instance.updateSteering("Player", healthAmount);
        healthPrice += 100;
        healthButton.GetComponentInChildren<Text>().text = healthPrice.ToString();
        healthAmountText.text = "+" + healthAmount + " MaxHealth";
        HealthUpgradeCount++;
    }

    #endregion

}
