using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : NetworkBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.8f;
    public float jumpHeight = 1f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    //Move player based on keyboard instructions
    void PlayerMove()
    {
        if (isLocalPlayer)
        {
            //Check if player reaches ground
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if(isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            //Move in x & z directions
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            //Negative values since initial model was 90 degree off and player body was rotated -90 degrees
            Vector3 move = - transform.right * x - transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
            
            //Move in y direction
            if(Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(- jumpHeight * gravity);
            }
            velocity.y = velocity.y + gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    void Update()
    {
        PlayerMove();
    }
}
