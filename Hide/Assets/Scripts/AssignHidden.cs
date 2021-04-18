using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AssignHidden : NetworkBehaviour
{
    [SerializeField] GameObject[] hides = new GameObject[4];
    //private NetworkManagerHide manager;

    public override void OnStartServer()
    {
        int select = Random.Range(0, hides.Length);
        GameObject choosen = hides[select];
        RpcTag(choosen);
        SvrTag(choosen);
    }

    [Server]
    void SvrTag(GameObject a)
    {
        a.tag = "Hidden";
    }

    [ClientRpc]
    void RpcTag(GameObject a)
    {
        a.tag = "Hidden";
    }

    /*
    public void Assign()
    {
        
        foreach (Transform child in transform)
        {
            Hides.Add(child);
            // Debug.Log(child.gameObject.name);
        }
        int select = Random.Range(0, Hides.Count);
        GameObject choosen = ((Transform)Hides[select]).gameObject;
        //CmdTag(choosen);
        //choosen.tag = "Hidden";
        manager.ChangeTag(choosen);
    }*/

}
