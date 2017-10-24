using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager _instance;
    public PlayerController _playerInstance;   

    public Image playerHealthBar;
    // Use this for initialization
    void Start()
    {
        _instance = this;
    }

    public void SpawnPlayer()
    {
        if (_playerInstance == null)
        {
            GameObject aux = (GameObject)Instantiate(Resources.Load("Player" + 2));
            _playerInstance = aux.GetComponent<PlayerController>();
        }
        _playerInstance._frontCannonButton1 = HUDManager.instance.frontCannonButton1.gameObject;
        _playerInstance._frontCannonButton2 = HUDManager.instance.frontCannonButton2.gameObject;
        _playerInstance.GetComponent<Health>().healthBar = playerHealthBar;
        _playerInstance.name = "Player";
    }

    public void SetState1(int ev)
    {
        _playerInstance._stateMachine.SetEvent1((SMPlayer.Events)ev);
        // Debug.Log("SetState1 :" + (SMPlayer.Events)ev);
        HUDManager.instance.UpdateUI();
        _playerInstance.resetPositions();
    }

    public void SetState2(int ev)
    {
        // Debug.Log("SetState2 :" + (SMPlayer.Events)ev);
        _playerInstance._stateMachine.SetEvent2((SMPlayer.Events)ev);
        HUDManager.instance.UpdateUI();
        _playerInstance.resetPositions();
    }
}
