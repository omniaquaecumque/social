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
        green.GetComponentInChildren<Light>().intensity = 0;
        yellow.GetComponentInChildren<Light>().intensity = 0;
    }

    [ServerCallback]
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            RpcLight(red);
            SvrLight(red);
        }
        else if (Input.GetKeyUp(KeyCode.O))
        {
            RpcLightOff(red);
            SvrLightOff(red);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            RpcLight(blue);
            SvrLight(blue);
        }
        else if (Input.GetKeyUp(KeyCode.P))
        {
            RpcLightOff(blue);
            SvrLightOff(blue);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            RpcLight(yellow);
            SvrLight(yellow);
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            RpcLightOff(yellow);
            SvrLightOff(yellow);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            RpcLight(green);
            SvrLight(green);
        }
        else if(Input.GetKeyUp(KeyCode.L))
        {
            RpcLightOff(green);
            SvrLightOff(green);
        }
    }

    [Server]
    private void SvrLight(GameObject l)
    {
        l.GetComponentInChildren<Light>().intensity = 2;
    }

    [ClientRpc]
    private void RpcLight(GameObject l)
    {
        l.GetComponentInChildren<Light>().intensity = 2;
    }

    [Server]
    private void SvrLightOff(GameObject l)
    {
        l.GetComponentInChildren<Light>().intensity = 0;
    }
    [ClientRpc]
    private void RpcLightOff(GameObject l)
    {
        l.GetComponentInChildren<Light>().intensity = 0;
    }
}
