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
        if (_playerInstance != null && _playerInstance._shipType != PlayerPrefs.GetInt("CurrentShip"))
        {
            Destroy(_playerInstance.gameObject);
            _playerInstance = null;
        }

        if (_playerInstance == null)
        {
            GameObject aux = (GameObject)Instantiate(Resources.Load("Player" + PlayerPrefs.GetInt("CurrentShip")));
            _playerInstance = aux.GetComponent<PlayerController>();
            HUDManager.instance.frontCannonButton1.gameObject.SetActive(_playerInstance._frontCannon);
            HUDManager.instance.frontCannonButton2.gameObject.SetActive(_playerInstance._frontCannon);
            _playerInstance.GetComponent<Health>().healthBar = playerHealthBar;
            _playerInstance.name = "Player";
        }    
    }

    public void SetState1(int ev)
    {
        _playerInstance._stateMachine.SetEvent1((SMPlayer.Events)ev);
        HUDManager.instance.UpdateUI();
        _playerInstance.resetPositions();
    }

    public void SetState2(int ev)
    {
        _playerInstance._stateMachine.SetEvent2((SMPlayer.Events)ev);
        HUDManager.instance.UpdateUI();
        _playerInstance.resetPositions();
    }


}
