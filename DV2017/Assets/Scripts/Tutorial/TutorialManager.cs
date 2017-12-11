using System.Collections;
using System.Collections.Generic;


using UnityEngine;
using UnityEngine.SceneManagement;


public class TutorialManager : MonoBehaviour {

    public Message _message;
    public static TutorialManager instance;

    public bool nestMessage = false;
    GameObject enemy;

    void Start () {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        if(PlayerPrefs.GetInt("tutorial") == 1) SceneManager.LoadScene("Main");

        MenuManager.instance.StartGame();
        MenuManager.instance.gameState = GameState.Tutorial;
        SpawnManager.instance.TurnOnOffSpawnEnemiesSpawn();

        string[] aux = new string[5];
        aux[0] = "Bienvenido al tutorial de Land In Sight!";
        aux[1] = "Para mover el barco deberás usar a tus marineros...";
        aux[2] = "Ubica un marinero en el timón.";
        aux[3] = "Una vez allí, clickea en cualquier punto en el agua y el barco se moverá hacia esa dirección!";
        aux[4] = "¡Hagámoslo!";

        _message.Mensaje(aux);
    }
    
    private void Update()
    {
        if (nestMessage)
        {
            if (EnemyFactory.instance.enemiesAlive == 0)
            {
                nestMessage = false;
                Mensaje("Ahora tienes que buscar la salida, ubica uno de tus marineros en el mástil para encontrarla mas fácil!!");
            }
        }            
    }

    public void Mensaje(string message)
    {
        _message.Mensaje(message);
    }

    public void Mensaje(string[] message)
    {
        _message.Mensaje(message);
    }

    public void ResetTutorial()
    {
        //Mensaje("Reiniciando tutorial...");
        SceneManager.LoadScene("Tutorial");
    }

    public void TutorialFinished()
    {
        PlayerPrefs.SetInt("tutorial", 1);
        Mensaje("TUTORIAL FINALIZADO...");
        Time.timeScale = 1;
        SceneManager.LoadScene("Main");
    }
}