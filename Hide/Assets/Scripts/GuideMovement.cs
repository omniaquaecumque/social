using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GuideMovement : NetworkBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private CharacterController controller = null;

    private Vector2 previousInput;
    private float previousHeight;

    private Control controls;
    private Control Controls
    {
        get
        {
            if (controls != null) { return controls; }
            return controls = new Control();
        }
    }
    public override void OnStartAuthority()
    {
        enabled = true;

        Controls.Player.Move.performed += ctx => SetMovement(ctx.ReadValue<Vector2>());
        Controls.Player.Move.canceled += ctx => ResetMovement();
        Controls.Player.Jump.performed += ctx => SetVerticle(ctx.ReadValue<float>());
        Controls.Player.Jump.canceled += ctx => ResetHeight();
    }

    [ClientCallback]
    private void OnEnable() => Controls.Enable();
    [ClientCallback]
    private void OnDisable() => Controls.Disable();
    [ClientCallback]
    private void Update()
    {
        Move(); MoveVerticle();
    }

    [Client]
    private void SetMovement(Vector2 movement) => previousInput = movement;

    [Client]
    private void SetVerticle(float movement) => previousHeight = movement;

    [Client]
    private void ResetMovement() => previousInput = Vector2.zero;
    [Client]
    private void ResetHeight() => previousHeight = 0f;

    [Client]
    private void Move()
    {
        Vector3 right = controller.transform.right;
        Vector3 forward = controller.transform.forward;
        right.y = 0f;
        forward.y = 0f;

        Vector3 movement = right.normalized * previousInput.x + forward.normalized * previousInput.y;

        controller.Move(movement * movementSpeed * Time.deltaTime);
    }

    [Client]
    private void MoveVerticle()
    {
        controller.Move(new Vector3(0, previousHeight * movementSpeed * Time.deltaTime, 0));
    }
}
