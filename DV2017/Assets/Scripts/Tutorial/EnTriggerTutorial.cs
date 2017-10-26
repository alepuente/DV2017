using UnityEngine;

public class EnTriggerTutorial : MonoBehaviour {

    public GameObject spawnPointEnemy;
    bool done = false;

    private void OnTriggerEnter(Collider other)
    {
        if(!done)
        if(other.tag == "Player")
        {
            string[] aux = new string[4];
            aux[0] = "¡Un enemigo!";
            aux[1] = "Al igual que el timón, si ubicas un marinero en algún cañon podrás atacarlo!";
            aux[2] = "Si te acercas lo suficiente con el cañón que hayas seleccionado este atacará automaticamente!!";
            aux[3] = "Vamos a intentarlo, ¡DERROTA AL ENEMIGO!";

            SpawnManager.instance.spawnEnemyAtPoint(spawnPointEnemy.transform.position);
            TutorialManager.instance.Mensaje(aux);
            TutorialManager.instance.nestMessage = true;
            done = true;
        }
    }
}
