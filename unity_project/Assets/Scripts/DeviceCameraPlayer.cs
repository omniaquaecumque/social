using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceCameraPlayer : MonoBehaviour
{
    //WebCam Textures are textures onto which the live video input is rendered.
    static WebCamTexture deviceCam;
    private Texture startTexture;

    void Start()
    {
        Renderer r = GetComponent<Renderer>();
        Material[] newMaterials = r.materials;
        // Remember the starting texture of the avatar
        startTexture = newMaterials[2].GetTexture("_MainTex");
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            TurnOnCam();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            TurnOffCam();
        }

    }

    // Turn on device camera and render image on player avatar
    void TurnOnCam()
    {
        if (deviceCam == null)
            deviceCam = new WebCamTexture();

        Renderer r = GetComponent<Renderer>();
        Material[] newMaterials = r.materials;
        // Change player face material texture to device camera texture
        newMaterials[2].SetTexture("_MainTex", deviceCam);
        // Update materials
        r.materials = newMaterials;

        // Turn on device camera  
        if (!deviceCam.isPlaying)
            deviceCam.Play();
    }


    // Turn off device camera
    void TurnOffCam()
    {
        Renderer r = GetComponent<Renderer>();
        Material[] newMaterials = r.materials;
        newMaterials[2].SetTexture("_MainTex", startTexture);
        // Update materials
        r.materials = newMaterials;

        // Turn off device camera
        if (deviceCam.isPlaying)
            deviceCam.Stop();
    }
}
