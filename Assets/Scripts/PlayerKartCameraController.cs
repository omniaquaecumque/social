﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Mirror;

public class PlayerKartCameraController : NetworkBehaviour
{
    
    [Header("Camera")]
    [SerializeField] private Vector2 maxFollowOffset = new Vector2(-1f, 6f);
    [SerializeField] private Vector2 cameraVelocity = new Vector2(4f, 0.25f);
    [SerializeField] private CinemachineFreeLook virtualCamera = null;
    /*
    private Controls controls;
    private Controls Controls
    {
        get
        {
            if (controls != null) { return controls; }
            return controls = new Controls();
        }
    }    */

    //private CinemachineTransposer transposer;

    public override void OnStartAuthority()
    {
        virtualCamera.gameObject.SetActive(true);
        enabled = true;

    }
/*
    [ClientCallback]
    private void OnEnable() => Controls.Enable();
    [ClientCallback]
    private void OnDisable() => Controls.Disable();

    private void Look(Vector2 lookAxis)
    {
        float deltaTime = Time.deltaTime;
    }    */

}