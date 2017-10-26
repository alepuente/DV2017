using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image healthBar;
    public float health;
    public float maxHealth;
    public HealthBarController enemyHealthBar;

    private void Start()
    {
        if (tag == "Player")
        {

        }
        reset();
    }

    public void reset()
    {
        health = GameManager.instance.HealthDic[gameObject.name];
        maxHealth = GameManager.instance.HealthDic[gameObject.name];
    }

    private void Update()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = health / maxHealth;
        }
        if (MenuManager.instance.gameState == GameState.Game)
        {
            if (health <= 0)
            {
                if (tag != "Player")
                {
                    reset();
                    gameObject.SetActive(false);
                    enemyHealthBar.healthPanel.SetActive(false);
                    EnemyFactory.instance.enemiesAlive--;
                    GameManager.instance.addMoney(name);
                }
                else
                {
                    reset();
                    GameManager.instance.resetGame();
                }
            }
        }
        else if (MenuManager.instance.gameState == GameState.Menu)
        {
            reset();
        }
        else if(MenuManager.instance.gameState == GameState.Tutorial)
        {
            if (health <= 0)
            {
                if (tag != "Player")
                {
                    reset();
                    gameObject.SetActive(false);
                    enemyHealthBar.healthPanel.SetActive(false);
                    EnemyFactory.instance.enemiesAlive--;
                    GameManager.instance.addMoney(name);
                }
                else
                {
                    TutorialManager.instance.ResetTutorial();
                }
            }
        }

    }
}
