using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    public Message _message;

    void Start () {
        MenuManager.instance.StartGame();
        SpawnManager.instance.TurnOnOffSpawnEnemiesSpawn();


    }
    
    private void Update()
    {

    }

    public void Mensaje(string message)
    {
        _message.Mensaje(message); 
    }
}
