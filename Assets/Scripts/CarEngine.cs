using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarEngine : MonoBehaviour {

    public Transform path;
    public float maxSteerAngle = 45f;
    public WheelCollider Front_Wheel_Left;
    public WheelCollider Front_Wheel_Right;
    public float maxMotorTorque = 140f;
    public float currentSpeed;
    public float maxSpeed = 150f;

    private List<Transform> nodes;
    private int currentNode = 0;

    private void Start () {
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++) {
            if (pathTransforms[i] != path.transform) {
                nodes.Add(pathTransforms[i]);
            }
        }
    }
	
	private void FixedUpdate () {
        ApplySteer();
        Drive();
        CheckWaypointDistance();
	}
    //to steer wheel colliders
    private void ApplySteer() {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        Front_Wheel_Left.steerAngle = newSteer;
        Front_Wheel_Right.steerAngle = newSteer;
    }
    //to make car move
    private void Drive() {
        currentSpeed = 2 * Mathf.PI * Front_Wheel_Left.radius * Front_Wheel_Left.rpm * 60 / 1000;

        if (currentSpeed < maxSpeed) {
            Front_Wheel_Left.motorTorque = maxMotorTorque;
            Front_Wheel_Right.motorTorque = maxMotorTorque;
        } else {
            Front_Wheel_Left.motorTorque = 0;
            Front_Wheel_Right.motorTorque = 0;
        }
    }
    //loop for movement on each node
    private void CheckWaypointDistance() {
        if(Vector3.Distance(transform.position, nodes[currentNode].position) < 3f) {
            if(currentNode == nodes.Count - 1) {
                currentNode = 0;
            } else {
                currentNode++;
            }
        }
    }
}
