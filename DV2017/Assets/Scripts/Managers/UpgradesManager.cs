using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour {

    public GameObject _spawnPosition;
    public List<GameObject> _playerModels;
    private List<GameObject> _models;
    private int _actualShip;
    private ShopItem _actualShipObj;
    
    public Button _buyButton;
    public Text _nameLabel;

    void Start() {
        _models = new List<GameObject>();
        foreach (GameObject item in _playerModels)
        {
            _models.Add(Instantiate(item, _spawnPosition.transform));
        }

        for (int i = 1; i < _models.Count; i++)
        {
            _models[i].SetActive(false);
        }
        _actualShip = 0;
        PlayerPrefs.SetInt("CurrentShip", _actualShip);
        PlayerPrefs.Save();
        _buyButton.gameObject.SetActive(false);
        _actualShipObj = _models[_actualShip].GetComponent<ShopItem>();
        _nameLabel.text = _actualShipObj._name;
    }	

    public void Update()
    {
        if (_buyButton.IsActive())
        {
          _buyButton.interactable = !(GameManager.instance.money < _actualShipObj._price);
        }
    }

    public void NextShip()
    {
        _models[_actualShip].SetActive(false);
        _actualShip++;
        if (_actualShip > _models.Count-1)
        {
            _actualShip = 0;
        }
        _models[_actualShip].SetActive(true);
        _actualShipObj = _models[_actualShip].GetComponent<ShopItem>();
        _nameLabel.text = _actualShipObj._name;
        UpdateUpgrades();
    }

    public void PrevShip()
    {
        _models[_actualShip].SetActive(false);
        _actualShip--;
        if (_actualShip < 0)
        {
            _actualShip = _models.Count-1;
        }
        _models[_actualShip].SetActive(true);
        _actualShipObj = _models[_actualShip].GetComponent<ShopItem>();
        _nameLabel.text = _actualShipObj._name;
        UpdateUpgrades();
    }

    public void BuyShip()
    {
        GameManager.instance.money -= _actualShipObj._price;
        PlayerPrefs.SetInt("Upgrade" + _actualShip, 1);
        PlayerPrefs.Save();
        UpdateUpgrades();
    }

    private void UpdateUpgrades()
    {
        if (PlayerPrefs.GetInt("Upgrade" + _actualShip) == 1)
        {
            _buyButton.gameObject.SetActive(false);
            PlayerPrefs.SetInt("CurrentShip", _actualShip);
            PlayerPrefs.Save();
        }
        else
        {
            _buyButton.GetComponentInChildren<Text>().text = "Unlock " + _models[_actualShip].GetComponent<ShopItem>()._price.ToString();
            _nameLabel.text = _actualShipObj._name;
            _buyButton.gameObject.SetActive(true);
        }
    }
}
