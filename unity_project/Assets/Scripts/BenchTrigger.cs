using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BenchTrigger : MonoBehaviour
{
    public GameObject definedButton;
    public UnityEvent OnClick = new UnityEvent();

    private AudioSource mic;

    void Start()
    {
        if (Microphone.devices.Length > 0) 
        {
            //Get AudioSource component  
            mic = this.GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;

        if (Input.GetMouseButtonDown(0))
        {
            // Cast ray out of the camera and see if it hits this gameobject
            if (Physics.Raycast(ray, out Hit) && Hit.collider.gameObject == gameObject)
            {
                if (!Microphone.IsRecording(null))
                {
                    this.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                    mic.clip = Microphone.Start(null, true, 20, 44100); // Start recording
                }
                else
                {
                    this.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    Microphone.End(null); //Stop recording  
                    mic.Play(); //Play recording
                }
                OnClick.Invoke();
            }
        }
    }


}
