using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Pmovement : NetworkBehaviour
{


    public CharacterController controller;


    // Start is called before the first frame update

    private void Start()
    {
       //Physics.IgnoreLayerCollision(0, 8, true);
    }

    void CharacterMovement() 
    {
        if (isLocalPlayer) {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal * 15f, moveVertical * 15f, 0) * Time.deltaTime;
            movement = Vector3.ClampMagnitude(movement, 15f);
            controller.Move(movement);
        }    
    }

    private void Update()
    {
        CharacterMovement();
    }
}
