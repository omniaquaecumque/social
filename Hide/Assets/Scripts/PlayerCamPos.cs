using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerCamPos : NetworkBehaviour
{
    public GameObject CameraPos;

    void Start()
    {
        if (isLocalPlayer)
        {  //Transform main camera to local player camera location for first person view
            Transform cameraTransform = Camera.main.gameObject.transform;  
            cameraTransform.parent = CameraPos.transform;  
            cameraTransform.position = CameraPos.transform.position;  
            cameraTransform.rotation = CameraPos.transform.rotation;
        }
    }
}
