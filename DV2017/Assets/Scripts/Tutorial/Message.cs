using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Message : MonoBehaviour
{

    public Text textMessage;
    public GameObject panel;

    List<string> pendingMessages = new List<string>();

    private void Update()
    {
        if (!panel.gameObject.activeInHierarchy)
        {
            if (pendingMessages.Count > 0)
            {
                Mensaje(pendingMessages[0]);
                pendingMessages.Remove(pendingMessages[0]);
            }
        }
    }

    public void Mensaje(string message)
    {
        textMessage.text = message;
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Mensaje(string[] message)
    {
        for (int i = 0; i < message.Length; i++)
            pendingMessages.Add(message[i]);
    }

    public void HidePanel()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }


}
