using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceCamObject : MonoBehaviour
{
    //WebCam Textures are textures onto which the live video input is rendered.
    static WebCamTexture deviceCam;

    void Start()
    {
        
    }

    void Update()
    {
        // When press "Z" turn on device camera and project to game object
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (deviceCam == null)
                deviceCam = new WebCamTexture();

            GetComponent<Renderer>().material.mainTexture = deviceCam;

            if (!deviceCam.isPlaying)
                deviceCam.Play();
        }
        // When press "X" turn off device camera 
        if (Input.GetKeyDown(KeyCode.X))
        {

            GetComponent<Renderer>().material.mainTexture = null;

            if (deviceCam.isPlaying)
                deviceCam.Stop();
        }
    }
}
