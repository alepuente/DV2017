using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OrbitCamera : MonoBehaviour {

	public Transform target;
	public float distance = 2.0f;
	public float xSpeed = 20.0f;
	public float ySpeed = 20.0f;
	public float yMinLimit = -90f;
	public float yMaxLimit = 90f;
	public float distanceMin = 10f;
	public float distanceMax = 10f;
	public float smoothTime = 2f;
	float rotationYAxis = 0.0f;
	float rotationXAxis = 0.0f;
	float velocityX = 0.0f;
	float velocityY = 0.0f;


	// Use this for initialization
	void Start()
	{
		Vector3 angles = transform.eulerAngles;
		rotationYAxis = angles.y;
		rotationXAxis = angles.x;
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
		{
			GetComponent<Rigidbody>().freezeRotation = true;
		}
	}

    bool validInput = true;

    void validateInput()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        //DESKTOP COMPUTERS
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                validInput = false;
            else
                validInput = true;
        }
#else
    //MOBILE DEVICES
    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
    {
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            validInput = false;
        else
            validInput = true;
    }
#endif
    }


    void LateUpdate()
	{
        validateInput();

#if UNITY_STANDALONE || UNITY_EDITOR
        //DESKTOP COMPUTERS
        if (Input.GetMouseButton(0) && validInput)
        {
            //Put your code here
            //Debug.Log("Valid Input");
            velocityX += xSpeed * Input.GetAxis("Mouse X") * distance * 0.02f;
            velocityY += ySpeed * Input.GetAxis("Mouse Y") * 0.02f;
            
        }
            DoMovement();
#else

    //MOBILE DEVICES
    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && validInput)
    {
        //Put your code here
        //Debug.Log("Valid Input");
        velocityX += xSpeed * Input.GetAxis("Mouse X") * distance * 0.02f;
        velocityY += ySpeed * Input.GetAxis("Mouse Y") * 0.02f;
    }
        DoMovement();
#endif
    }

    void DoMovement()
    {        
        rotationYAxis -= velocityX/10f;
        rotationXAxis += velocityY/10f;
        rotationXAxis = ClampAngle(rotationXAxis, yMinLimit, yMaxLimit);
        Quaternion fromRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
        Quaternion toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
        Quaternion rotation = toRotation;
        transform.rotation = rotation;
        velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
        velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);
    }

	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}
}
