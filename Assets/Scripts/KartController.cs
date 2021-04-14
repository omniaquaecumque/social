using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class KartController : NetworkBehaviour
{
    private float horizontal; 
    private float vertical; 
    private float currentBreakForce;
    private float steerAngle; 
    private bool isBreaking;
    [SerializeField] private Rigidbody carBody;

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

    public override void OnStartAuthority()
    {
        //virtualCamera.gameObject.SetActive(true);
        enabled = true;
    }

    public void FixedUpdate() {
        GetInput();
        Drive();
        HandleSteering();
        UpdateWheels();
    }

    public void GetInput() {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    public void Drive() {
        frontLeftWheelCollider.motorTorque = vertical * motorForce;
        frontRightWheelCollider.motorTorque = vertical * motorForce;
        currentBreakForce = isBreaking ? breakForce : 0f;
        Break();
    }

    public void Break() {
        frontLeftWheelCollider.brakeTorque = currentBreakForce;
        frontRightWheelCollider.brakeTorque = currentBreakForce;
        rearLeftWheelCollider.brakeTorque = currentBreakForce;
        rearRightWheelCollider.brakeTorque = currentBreakForce;
    }

    public void HandleSteering() {
        steerAngle = maxSteerAngle * horizontal;
        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;
    }

    public void UpdateWheels() {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
    }

    public void UpdateSingleWheel(WheelCollider c, Transform t) {
        Vector3 position; 
        Quaternion rotation; 
        c.GetWorldPose(out position, out rotation);
        
        rotation = rotation * Quaternion.Euler(new Vector3(0, 90, 0));
        t.rotation = rotation; 
        t.position = position; 
    }
    
    /*
    void Update() {
        // Fall Detection 
        if (this.transform.position.y <= -20) {
            carBody = GetComponent<Rigidbody>();
            this.transform.position = respawnPoint.transform.position;
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            carBody.velocity = Vector3.zero;                  
        }
    }*/    
}
