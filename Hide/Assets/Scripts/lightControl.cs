using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class lightControl : NetworkBehaviour
{
    public GameObject blue, red, yellow, green;
    // Start is called before the first frame update
    public override void OnStartServer()
    {
        red.GetComponentInChildren<Light>().intensity = 0;
        blue.GetComponentInChildren<Light>().intensity = 0;
        green.GetComponentInChildren<Light>().intensity = 0;
        yellow.GetComponentInChildren<Light>().intensity = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            RpcLight(red);
        }
        else if (Input.GetKeyUp(KeyCode.O))
        {
            RpcLightOff(red);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            RpcLight(blue);
        }
        else if (Input.GetKeyUp(KeyCode.P))
        {
            RpcLightOff(blue);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            RpcLight(yellow);
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            RpcLightOff(yellow);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            RpcLight(green);
        }
        else if(Input.GetKeyUp(KeyCode.L))
        {
            RpcLightOff(green);
        }
    }

    [ClientRpc]
    private void RpcLight(GameObject l)
    {
        l.GetComponentInChildren<Light>().intensity = 2;
    }

    [ClientRpc]
    private void RpcLightOff(GameObject l)
    {
        l.GetComponentInChildren<Light>().intensity = 0;
    }
}
