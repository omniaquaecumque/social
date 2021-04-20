using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class MazeServer : NetworkBehaviour
{
    [SyncVar]
    public int xVal = 0;
    [SyncVar]
    public int yVal = 0;

    public GameObject UD;
    public GameObject LR;

    [Command(ignoreAuthority = true)]
    public void increaseX() {
        xVal += 1;
    }

    [Command(ignoreAuthority = true)]
    public void decreaseX()
    {
        xVal -= 1;
    }

    [Command(ignoreAuthority = true)]
    public void increaseY()
    {
        yVal += 1;
    }

    [Command(ignoreAuthority = true)]
    public void decreaseY()
    {
        yVal -= 1;
    }

    [ClientRpc]
    public void sendVals() {
        UD.GetComponent<Maze>().xVal = xVal;
        UD.GetComponent<Maze>().yVal = yVal;
        LR.GetComponent<Maze>().xVal = xVal;
        LR.GetComponent<Maze>().yVal = yVal;
    }

    private void Update()
    {
        if (isServer)
        {
            sendVals();
        }
        
    }
}
