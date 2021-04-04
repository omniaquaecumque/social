using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class bulletBeh : NetworkBehaviour
{
    private NetworkManagerHide networkManager;
    public void OnCollisionEnter(Collision collision)
    {
        // collide to hidden object
        if (collision.gameObject.CompareTag("Hidden"))
        {
            Debug.Log("Hit hidden");
            networkManager.winGame();
        }
        Destroy(gameObject);
    }
}
