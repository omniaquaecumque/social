using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class lightControl : NetworkBehaviour
{
    public GameObject blue, red, yellow, green;
    // Start is called before the first frame update
    void Start()
    {
        red.GetComponentInChildren<Light>().intensity = 0;
        blue.GetComponentInChildren<Light>().intensity = 0;
        yellow.GetComponentInChildren<Light>().intensity = 0;
        green.GetComponentInChildren<Light>().intensity = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            red.GetComponentInChildren<Light>().intensity = 2;
        }
        else if (Input.GetKeyUp(KeyCode.O))
        {
            red.GetComponentInChildren<Light>().intensity = 0;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            blue.GetComponentInChildren<Light>().intensity = 2;
        }
        else if (Input.GetKeyUp(KeyCode.P))
        {
            blue.GetComponentInChildren<Light>().intensity = 0;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            yellow.GetComponentInChildren<Light>().intensity = 2;
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            yellow.GetComponentInChildren<Light>().intensity = 0;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            green.GetComponentInChildren<Light>().intensity = 2;
        }
        else if(Input.GetKeyUp(KeyCode.L))
        {
            green.GetComponentInChildren<Light>().intensity = 0;
        }
    }
}
