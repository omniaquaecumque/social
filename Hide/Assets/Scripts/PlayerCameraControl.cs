using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Cinemachine;

public class PlayerCameraControl : NetworkBehaviour
{
    [Header("Camera")]
    [SerializeField] private Vector2 maxFollowOffset = new Vector2(-45f, 45f);
    [SerializeField] private Vector2 cameraVelocity = new Vector2(4f, 3f);
    [SerializeField] private Transform playerTransform = null;
    [SerializeField] private Transform headTransform = null;
    [SerializeField] private Transform wandTransform = null;
    [SerializeField] private CinemachineVirtualCamera virtualCamera = null;

    private Control controls;
    private float verRot;
    private Control Controls
    {
        get
        {
            if (controls != null) { return controls; }
            return controls = new Control();
        }
    }

    private void Start()
    {
        verRot = headTransform.eulerAngles.x;
    }

    public override void OnStartAuthority()
    {
        virtualCamera.gameObject.SetActive(true);

        enabled = true;

        Controls.Player.Look.performed += ctx => Look(ctx.ReadValue<Vector2>());
    }

    [ClientCallback]
    private void OnEnable() => Controls.Enable();
    [ClientCallback]
    private void OnDisable() => Controls.Disable();

    private void Look(Vector2 lookAxis)
    {
        float deltaTime = Time.deltaTime;

        verRot -= lookAxis.y * cameraVelocity.y * deltaTime;
        verRot = Mathf.Clamp(verRot, maxFollowOffset.x, maxFollowOffset.y);
        headTransform.localEulerAngles = new Vector3(verRot, 0, 0);
        if(wandTransform!=null)
            wandTransform.localEulerAngles = new Vector3(verRot+45, 0, 0);
        playerTransform.Rotate(0f, lookAxis.x * cameraVelocity.x * deltaTime, 0f);
    }
}
