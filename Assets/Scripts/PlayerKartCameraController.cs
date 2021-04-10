using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Mirror;

public class PlayerKartCameraController : NetworkBehaviour
{
    
    [Header("Camera")]
    [SerializeField] private CinemachineFreeLook virtualCamera = null;
    
    public override void OnStartAuthority()
    {
        virtualCamera.gameObject.SetActive(true);
        enabled = true;
    
    }
/*
    public GameObject CameraLocation;
    
    public override void OnStartAuthority()
    {
        Transform cameraTransform = Camera.main.gameObject.transform;  
        cameraTransform.parent = CameraLocation.transform;  
        cameraTransform.position = CameraLocation.transform.position;  
        cameraTransform.rotation = CameraLocation.transform.rotation;
    }*/
}
