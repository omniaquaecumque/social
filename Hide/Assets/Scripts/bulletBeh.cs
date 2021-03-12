using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class bulletBeh : NetworkBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        // collide to hidden object
        if (collision.gameObject.CompareTag("Hidden"))
        {
            Debug.Log("Hidden found");
        }
        Destroy(gameObject);
    }
}
