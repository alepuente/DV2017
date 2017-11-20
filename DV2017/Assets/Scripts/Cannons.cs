﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannons : MonoBehaviour
{
    public bool isLeft = false;
    public bool isRight = false;
    public bool isFront = false;
    public bool isEnemy = false;
    public int sailors;
    public float shootTimer;
    public Renderer rangeColor;

    void Start()
    {
        rangeColor = GetComponent<Renderer>();
        if (tag == PlayerController.instance.tag)
            gameObject.name = PlayerController.instance.name;
        else
            gameObject.name = gameObject.GetComponentInParent<EnemyController>().name;
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;
        if (isLeft)
            sailors = PlayerController.instance.cannonLeft;
        else if (isRight)
            sailors = PlayerController.instance.cannonRight;
        else if (isFront)
            sailors = PlayerController.instance.cannonFront;
        else if (isEnemy)
            sailors = 3;

        if (sailors > 0 && shootTimer > GameManager.instance.FireRateDic[gameObject.name])
        {
            if(sailors == 1)        rangeColor.material.color = HUDManager.instance.ActivePositionSailor1;
            else if (sailors == 2)  rangeColor.material.color = HUDManager.instance.ActivePositionSailor2;
            else if (sailors == 3)  rangeColor.material.color = Color.green;
        }
        else
            rangeColor.material.color = Color.red;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && other.tag != gameObject.tag  && sailors > 0 )
        {
            if (shootTimer > GameManager.instance.FireRateDic[gameObject.name])
            {
                foreach (ParticleSystem item in transform.GetComponentsInChildren<ParticleSystem>())
                {
                    item.Play();
                }
                foreach (AudioSource item in transform.GetComponentsInChildren<AudioSource>())
                {
                    item.Play();
                }
            other.GetComponent<Health>().health = GameManager.instance.calculateDamage(gameObject.name, other.GetComponent<Health>().health);
            shootTimer = 0;
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 8 && other.tag != gameObject.tag && sailors > 0 && shootTimer > GameManager.instance.FireRateDic[gameObject.name])
        {
            other.GetComponent<Health>().health = GameManager.instance.calculateDamage(gameObject.name, other.GetComponent<Health>().health);
            shootTimer = 0;
        }
    }
}
