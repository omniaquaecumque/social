using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerCameraController : NetworkBehaviour
{
    public GameObject CameraLocation;

    void Start()
    {
        if (isLocalPlayer)
        {  //Transform main camera to local player camera location for first person view
            Transform cameraTransform = Camera.main.gameObject.transform;  
            cameraTransform.parent = CameraLocation.transform;  
            cameraTransform.position = CameraLocation.transform.position;  
            cameraTransform.rotation = CameraLocation.transform.rotation;
        }
    }
}
