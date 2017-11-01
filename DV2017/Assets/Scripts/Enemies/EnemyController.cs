using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private int attackPosition;
    public float speed;
    public float turnSpeed;
    private Vector3 attackPos;


    void Start()
    {
        attackPosition = Random.Range(0, PlayerController.instance.attackPoint.Length);
        speed = GameManager.instance.SpeedDic[name];
        turnSpeed = GameManager.instance.TurnSpeedDic[name];
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.attackPoint[attackPosition].transform.position) > 0)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
            Vector3 targetDir = PlayerController.instance.attackPoint[attackPosition].transform.position + new Vector3(Random.Range(0,5),0,0) - transform.position;
            float step = Time.deltaTime / Mathf.PI;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step * turnSpeed, 0.0f);
            transform.rotation = Quaternion.LookRotation(new Vector3(newDir.x, 0, newDir.z));       
        }
    }
}