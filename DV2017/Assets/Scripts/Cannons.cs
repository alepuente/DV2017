using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannons : MonoBehaviour
{
    public bool isLeft = false;
    public bool isRight = false;
    public bool isFront = false;
    public bool isEnemy = false;
    public float shootTimer;
    public Renderer rangeColor;
    public bool _sailor;

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
            if (PlayerController.instance._stateMachine.getActualState1() == SMPlayer.States.LeftCannon || PlayerController.instance._stateMachine.getActualState2() == SMPlayer.States.LeftCannon)
            {
                _sailor = true;
            }
            else
            {
                _sailor = false;
            }
        else if (isRight)
            if (PlayerController.instance._stateMachine.getActualState1() == SMPlayer.States.RightCannon || PlayerController.instance._stateMachine.getActualState2() == SMPlayer.States.RightCannon)
            {
                _sailor = true;
            }
            else
            {
                _sailor = false;
            }
        else if (isFront)
            if (PlayerController.instance._stateMachine.getActualState1() == SMPlayer.States.FrontCannon || PlayerController.instance._stateMachine.getActualState2() == SMPlayer.States.FrontCannon)
            {
                _sailor = true;
            }
            else
            {
                _sailor = false;
            }
        else if (isEnemy)
                        _sailor = true;

        if (_sailor && shootTimer > GameManager.instance.FireRateDic[gameObject.name])
        {
              rangeColor.material.color = Color.green;
        }
        else
            rangeColor.material.color = Color.red;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && other.tag != gameObject.tag  && _sailor)
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
        if (other.gameObject.layer == 8 && other.tag != gameObject.tag && _sailor && shootTimer > GameManager.instance.FireRateDic[gameObject.name])
        {
            other.GetComponent<Health>().health = GameManager.instance.calculateDamage(gameObject.name, other.GetComponent<Health>().health);
            shootTimer = 0;
        }
    }
}
