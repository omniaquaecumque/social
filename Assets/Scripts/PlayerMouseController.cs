using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseController : NetworkBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerHead;
    public Transform playerBody;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Move Player Head (and Player View) up and down when mouse moves vertically
    void HeadTurn()
    {
        if (isLocalPlayer)
        {  
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -45f, 45f);  //Fix vertical field of view angle to 90 degrees temporally to central fixation

            playerHead.localRotation = Quaternion.Euler(0f, 0f, xRotation);

        }

    }

    //Move Player Body (and Player View) left and right when mouse moves horizontally
    void BodyTurn()
    {
        if (isLocalPlayer)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

    void Update()
    {
        HeadTurn();
        BodyTurn();
    }
}

