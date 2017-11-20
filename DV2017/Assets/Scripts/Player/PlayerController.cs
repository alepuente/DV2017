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
    private bool _anchor;
    private bool _fullSpeed;
    public bool _frontCannon;
    private Rigidbody _rgb;

    public SMPlayer _stateMachine;

    public GameObject _movingIndicator;

    private void Awake()
    {
        instance = this;
        //sailorEvent.AddListener(spawnSailor);
        _stateMachine = new SMPlayer();
        _rgb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        turnSpeed = GameManager.instance.TurnSpeedDic[tag];
        patrolSpeed = GameManager.instance.SpeedDic[tag];
        reset();
        GameObject aux = (GameObject)Instantiate(Resources.Load("MovingIndicator"));
        _movingIndicator = aux;
        _movingIndicator.SetActive(false);
    }

    private void Update()
    {
        MouseController();

        if (_stateMachine.getActualState1() != SMPlayer.States.Iddle && _stateMachine.getActualState2() != SMPlayer.States.Iddle)
        {
            CameraController.instance.changeToClose();
        }

        if (_stateMachine.getActualState1() != SMPlayer.States.Steering && _stateMachine.getActualState2() != SMPlayer.States.Steering)
        {
            _fullSpeed = false;
        }

        switch (_stateMachine.getActualState1())
        {
            case SMPlayer.States.FrontCannon:
                break;
            case SMPlayer.States.LeftCannon:
                break;
            case SMPlayer.States.RightCannon:
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
                break;
            case SMPlayer.States.LeftCannon:
                break;
            case SMPlayer.States.RightCannon:
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
        if (!_anchor)
        {
            if (_fullSpeed)
                transform.position += transform.forward * patrolSpeed * Time.deltaTime;
            else
                transform.position += transform.forward * patrolSpeed/1.5f * Time.deltaTime;
        }


    }

    public void resetPositions()
    {
        CameraController.instance.nest = false;
    }

    public void reset()
    {
        _anchor = true;
        resetPositions();
        waypoints.Clear();
        _stateMachine.resetStates();
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().isKinematic = false;
        transform.rotation = Quaternion.identity;
    }

    public void MouseController()
    {
        if (InputManager.instance.getSelection() != Vector3.zero)
        {
            waypoints.Clear();
            waypoints.Add(InputManager.instance.getSelection());
            _movingIndicator.SetActive(true);
            _movingIndicator.transform.position = waypoints[0];
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
        _fullSpeed = true;
        _anchor = false;
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
                _movingIndicator.SetActive(false);
            }

            // Set the destination for the agent
            // The navmesh agent is going to do the rest of the work
            if (waypoints.Count > 0)
            {
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
