using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerRot : NetworkBehaviour
{
    // camera sensitivities
    public float horSensitivity = 3f;
    public float verSensitivity = 3f;

    // max & min vertical rotations
    public float minRot = -45;
    public float maxRot = 45;

    private float verRot;

    // Start is called before the first frame update
    void Start()
    {
        verRot = transform.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        float verMouse = Input.GetAxis("Mouse Y");
        float horMouse = Input.GetAxis("Mouse X");

        verRot -= verMouse * verSensitivity;
        verRot = Mathf.Clamp(verRot, minRot, maxRot);
        float horRot = transform.eulerAngles.y + horMouse * horSensitivity;

        transform.localEulerAngles = new Vector3(verRot, horRot, 0);
    }
}