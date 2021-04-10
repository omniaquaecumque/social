﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartController : MonoBehaviour
{
    float horizontal; 
    float vertical; 
    float currentBreakForce;
    float steerAngle; 
    bool isBreaking;
    Rigidbody carBody;

    [SerializeField] private Transform respawnPoint;

    [SerializeField] private float motorForce; 
    [SerializeField] private float breakForce; 
    [SerializeField] private float maxSteerAngle; 

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;    

    void Update() {
        // Fall Detection 
        if (this.transform.position.y <= -20) {
            carBody = GetComponent<Rigidbody>();
            this.transform.position = respawnPoint.transform.position;
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            carBody.velocity = Vector3.zero;                  
        }
    }
    void FixedUpdate() {
        GetInput();
        Drive();
        HandleSteering();
        UpdateWheels();
    }

    void GetInput() {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    void Drive() {
        frontLeftWheelCollider.motorTorque = vertical * motorForce;
        frontRightWheelCollider.motorTorque = vertical * motorForce;
        currentBreakForce = isBreaking ? breakForce : 0f;
        Break();
    }

    void Break() {
        frontLeftWheelCollider.brakeTorque = currentBreakForce;
        frontRightWheelCollider.brakeTorque = currentBreakForce;
        rearLeftWheelCollider.brakeTorque = currentBreakForce;
        rearRightWheelCollider.brakeTorque = currentBreakForce;
    }

    void HandleSteering() {
        steerAngle = maxSteerAngle * horizontal;
        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;
    }

    void UpdateWheels() {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
    }

    void UpdateSingleWheel(WheelCollider c, Transform t) {
        Vector3 position; 
        Quaternion rotation; 
        c.GetWorldPose(out position, out rotation);
        
        rotation = rotation * Quaternion.Euler(new Vector3(0, 90, 0));
        t.rotation = rotation; 
        t.position = position; 
    }
}