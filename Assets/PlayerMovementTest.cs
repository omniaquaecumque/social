﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    public Transform cam;     
    public float speed = 6f; 
    public float jumpForce = 10f;
    public Rigidbody rb; 
    public bool onGround = true; 
 
    public float turnSmoothTime = 3f;
    float turnSmoothVelocity;
    Vector3 inputVector;

    void Start() {
       rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(horizontal, 0f, vertical).normalized;
        if (dir.magnitude >= 0.1f) {
            float turnAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, turnAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            rb.MoveRotation(transform.rotation);
        }        

        Vector2 input = new Vector2(horizontal, vertical);
        input = Vector2.ClampMagnitude(input, 1);

        Vector3 camF = cam.forward;
        Vector3 camR = cam.right;
        camF.y = 0;
        camR.y = 0; 
        camF = camF.normalized;
        camR = camR.normalized; 
        transform.position += (camF * input.y + camR*input.x) * speed * Time.deltaTime;
    
        if (Input.GetButtonDown("Jump") && onGround) {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            onGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Ground") {
            onGround = true;
        }
    }
}
