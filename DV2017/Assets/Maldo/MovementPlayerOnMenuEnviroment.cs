using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayerOnMenuEnviroment : MonoBehaviour {
    public float speed = 1;
    public float height = 1;
    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
    }
        
    void Update () {
        transform.position = _startPosition + new Vector3(0.0f, height * Mathf.Cos(Time.time * speed), 0.0f);
    }
}
