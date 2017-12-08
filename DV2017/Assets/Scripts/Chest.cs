using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && other.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }    
}
