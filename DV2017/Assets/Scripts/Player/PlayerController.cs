using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[System.Serializable]
public class SailorsEvent : UnityEvent<int>
{
}

public class PlayerController : MonoBehaviour
{
    public int _shipType;

    public static PlayerController instance;
    public float patrolSpeed;
    public float turnSpeed;
    public List<Vector3> waypoints;
    public float minWaypointDistance = 0.1f;
    public GameObject[] attackPoint;
    public SailorsEvent sailorEvent;

    public int cannonLeft = 0;
    public int cannonRight = 0;
    public int cannonFront = 0;

    public SMPlayer _stateMachine;

    public GameObject _frontCannonButton1;
    public GameObject _frontCannonButton2; 

    private void Awake()
    {
        instance = this;
        //sailorEvent.AddListener(spawnSailor);
        _stateMachine = new SMPlayer();
    }

    private void Start()
    {
        switch (_shipType)
        {
            case 1:
                _frontCannonButton1.SetActive(false);
                _frontCannonButton2.SetActive(false);
                break;
            case 2:
                _frontCannonButton1.SetActive(true);
                _frontCannonButton2.SetActive(true);
                break;
            default:
                break;
        }
        turnSpeed = GameManager.instance.TurnSpeedDic[tag];
        patrolSpeed = GameManager.instance.SpeedDic[tag];
    }

    private void Update()
    {
        MouseController();

        if (_stateMachine.getActualState1() != SMPlayer.States.Iddle && _stateMachine.getActualState2() != SMPlayer.States.Iddle)
        {
            CameraController.instance.changeToClose();
        }

        switch (_stateMachine.getActualState1())
        {
            case SMPlayer.States.FrontCannon:
                cannonFront = 1;
                break;
            case SMPlayer.States.LeftCannon:
                cannonLeft = 1;
                break;
            case SMPlayer.States.RightCannon:
                cannonRight = 1;
                break;
            case SMPlayer.States.Steering:
                MouseController(); Patrolling();
                break;
            case SMPlayer.States.OnNest:
                CameraController.instance.changeToFar();
                break;
            case SMPlayer.States.Iddle:
                break;
            default:
                break;
        }

        switch (_stateMachine.getActualState2())
        {
            case SMPlayer.States.FrontCannon:
                cannonFront = 2;
                break;
            case SMPlayer.States.LeftCannon:
                cannonLeft = 2;
                break;
            case SMPlayer.States.RightCannon:
                cannonRight = 2;
                break;
            case SMPlayer.States.Steering:
                MouseController(); Patrolling();
                break;
            case SMPlayer.States.OnNest:
                CameraController.instance.changeToFar();
                break;
            case SMPlayer.States.Iddle:
                break;
            default:
                break;
        }


    }

    public void resetPositions()
    {
        cannonLeft = 0;
        cannonRight = 0;
        cannonFront = 0;

        CameraController.instance.nest = false;
    }

    public void reset()
    {
        resetPositions();
        waypoints.Clear();
        _stateMachine.resetStates();
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().isKinematic = false;
        transform.position = new Vector3(0f, 0.81f, 0f);
        transform.rotation = Quaternion.identity;
    }

    public void MouseController()
    {
        if (InputManager.instance.getSelection() != Vector3.zero)
        {
            waypoints.Clear();
            waypoints.Add(InputManager.instance.getSelection());
        }
    }

    /* private void spawnSailor(int sailorType)
     {
         resetPositions();
         switch (sailorType)
         {
             case 1: _stateMachine.SetEvent1(SMPlayer.Events.ToRudder); rudderPos.material.color = Color.green; break;
             case 2:_stateMachine.SetEvent1(SMPlayer.Events.ToNest); nestPos.material.color = Color.green; CameraController.instance.nest = true; break;
             case 3:_stateMachine.SetEvent1(SMPlayer.Events.ToLeftCannon); leftPos.material.color = Color.green; break;
             case 4:_stateMachine.SetEvent1(SMPlayer.Events.ToRightCannon); rightPos.material.color = Color.green; break;
             case 5:_stateMachine.SetEvent1(SMPlayer.Events.ToRudder); frontPos.material.color = Color.green; break;
         }
         Debug.Log("SpawnSailor: "+ sailorType);
     }*/

    public void Patrolling()
    {
        if (waypoints.Count > 0)
        {
            // Create two Vector3 variables, one to buffer the ai agents local position, the other to
            // buffer the next waypoints position
            Vector3 tempLocalPosition;
            Vector3 tempWaypointPosition;

            // Agents position (x, set y to 0, z)
            tempLocalPosition = transform.position;
            tempLocalPosition.y = 0f;

            // Current waypoints position (x, set y to 0, z)
            tempWaypointPosition = waypoints[0];
            tempWaypointPosition.y = 0f;


            // Is the distance between the agent and the current waypoint within the minWaypointDistance?
            if (Vector3.Distance(tempLocalPosition, tempWaypointPosition) <= minWaypointDistance)
            {
                waypoints.Remove(waypoints[0]);
            }

            // Set the destination for the agent
            // The navmesh agent is going to do the rest of the work
            if (waypoints.Count > 0)
            {
                transform.position += transform.forward * patrolSpeed * Time.deltaTime;
                Vector3 targetDir = waypoints[0] - transform.position;
                float step = Time.deltaTime / Mathf.PI;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step * turnSpeed, 0.0f);
                transform.rotation = Quaternion.LookRotation(new Vector3(newDir.x, 0, newDir.z));             
            }
        }
    }

    public void setPositionPlayer(Transform newPos)
    {
        gameObject.transform.position = newPos.position;

        Vector3 rot = newPos.rotation.eulerAngles;
        gameObject.transform.Rotate(rot);
    }
}
