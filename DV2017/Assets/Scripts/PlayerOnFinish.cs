﻿using UnityEngine;

public class PlayerOnFinish : MonoBehaviour
{
    float timer;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                GameManager.instance.resetGame();
                Debug.Log("<color=red>PLAYER ON FINISH!</color>");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            timer = 0;
        }
    }
}