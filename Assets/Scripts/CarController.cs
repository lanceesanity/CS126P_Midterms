using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

// inputs to store data for horizontalnumber, verticalnumber, 
// steerangle, breakforce, and boolean for breaking and boost.
    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbreakForce;
    private bool isBreaking;
    private bool isBoosting;

//SerializeFields for unity component. 
//easy adjusting of values through unity components.
    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private float motorBoost;

// Wheel Transform and Colliders to help with steering and animation of wheels turning,
// moving forward, backward, etc.
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;


//FixedUpdate to store different functions.
    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

//GetInput for horizontalinput and verticalinput axis and 
// isBoost/isBoosting key configuration.
    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
        isBoosting = Input.GetKey(KeyCode.F1);
    }

//HandleMotor to maintain the variables to 0 when breaking or boosting until
//Key configurations are pressed.
    private void HandleMotor()
    {
        if (isBreaking)
            breakForce = 5000;
            else
            breakForce = 0;

        if (isBoosting)
            motorForce = 3000;
            else
            motorForce = 1500;

        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;

        if (isBreaking)
        {
            ApplyBreaking();
        }

        if (isBoosting)
        {
            ApplyBoosting();
        }
    }
//Function to help for Boosting
    private void ApplyBoosting()
    {
        frontLeftWheelCollider.motorTorque = motorForce + 1500;
        frontRightWheelCollider.motorTorque = motorForce + 1500;
    }

//Function to help for Breaking
    private void ApplyBreaking()
    {
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

//Function to help for Steering
    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

//Function to update each wheel colliders and Wheel Transform for Unity Components
    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
    }

//Function to update each single wheel with the Wheel Colliders and Transforms.
    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
