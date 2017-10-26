using UnityEngine;

public class FinishTutorialTrigger : MonoBehaviour {
    
    bool done = false;
    float timer;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            timer += Time.deltaTime;
            if (timer > .8)
            {
                if (!done)
                    {
                        TutorialManager.instance.Mensaje("¡Tutorial FINALIZADO!");
                        done = true;
                    }
            }
        }
    }
}
