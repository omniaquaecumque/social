using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AssignHidden : NetworkBehaviour
{
    //[SerializeField] private GameObject[] hiddens = new GameObject[4];
    //[SerializeField] private Transform[] spawnPos = new Transform[4];
    private ArrayList Hides = new ArrayList();
    // Start is called before the first frame update
    public override void OnStartServer()
    {
        Assign();
    }

    [ServerCallback]
    private void Assign()
    {
        foreach (Transform child in transform)
        {
            Hides.Add(child);
            NetworkServer.Spawn(child.gameObject);
            // Debug.Log(child.gameObject.name);
        }
        int select = Random.Range(0, Hides.Count);
        GameObject choosen = ((Transform)Hides[select]).gameObject;
        RpcTag(choosen);
    }

    [ClientRpc]
    private void RpcTag(GameObject a)
    {
        a.tag = "Hidden";
    }
}
