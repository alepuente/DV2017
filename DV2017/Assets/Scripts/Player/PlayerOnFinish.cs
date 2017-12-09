using UnityEngine;

public class PlayerOnFinish : MonoBehaviour
{
    float timer;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && other.gameObject.layer == 8)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {

                MoneyPerLevelManager.instance.ShowFinishBoard();

                Debug.Log("<color=red>PLAYER ON FINISH!</color>");
                timer = 0;
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
