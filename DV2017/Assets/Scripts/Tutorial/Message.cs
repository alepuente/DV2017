using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour{

    public Text textMessage;
    public GameObject panel;

    public void Mensaje(string message)
    {
        textMessage.text = message;
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void HidePanel()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }
}
