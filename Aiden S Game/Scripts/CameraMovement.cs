using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CameraMovement : NetworkBehaviour
{
    public GameObject CameraLocation;


    void CameraZoom()
    {

        var zoomSpeed = 15f;

        //set z bound on zoom (-50 away, or -5 close)
        if ((CameraLocation.transform.position.z == -50 && Mathf.Sign(Input.GetAxis("Mouse ScrollWheel")) == -1f) || (CameraLocation.transform.position.z == -5 && Mathf.Sign(Input.GetAxis("Mouse ScrollWheel")) == 1f))
        {
            return;
        }

        //bound on z, scroll to exactly -50
        if (CameraLocation.transform.position.z + (Input.GetAxis("Mouse ScrollWheel") * zoomSpeed) < -50)
        {
            CameraLocation.transform.Translate(0, 0, -50 - CameraLocation.transform.position.z);
            return;
        }
        //bound on z, scroll to exactly -5
        else if (CameraLocation.transform.position.z + (Input.GetAxis("Mouse ScrollWheel") * zoomSpeed) > -5)
        {
            CameraLocation.transform.Translate(0, 0, -5 - CameraLocation.transform.position.z);
            return;
        }

        //zoom based on scroll amount
        CameraLocation.transform.Translate(0, 0, Input.GetAxis("Mouse ScrollWheel") * zoomSpeed);

    }

    void Start()
    {
        if (isLocalPlayer)
        {  //Transform main camera to local player camera location for first person view
            Transform cameraTransform = Camera.main.gameObject.transform;
            cameraTransform.parent = CameraLocation.transform;
            cameraTransform.position = CameraLocation.transform.position;
        }
    }

    private void Update()
    {
        //if not in task allow zoom
        if (!this.GetComponent<Pmovement>().inTask) { CameraZoom(); }
       
    }
}
