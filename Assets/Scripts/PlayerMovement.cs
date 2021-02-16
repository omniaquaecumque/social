using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : NetworkBehaviour
{
    public CharacterController controller;

    public float speed = 12f;

    //Move player based on keyboard instructions
    void PlayerMove()
    {
        if (isLocalPlayer)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            //negative values since initial model was 90 degree off and player body was rotated -90 degrees
            Vector3 move = - transform.right * x - transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);
        }
    }

    void Update()
    {
        PlayerMove();
    }
}
