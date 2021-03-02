using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Interact : NetworkBehaviour
{
    bool interact = false;

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Job") 
        {
            interact = true;
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Job")
        {
            interact = false;
        }
    }

    void PlayJob() {
        Debug.Log("Interacted");
    }

    private void Update()
    {
        if (interact && Input.GetKeyDown(KeyCode.E)) {
            PlayJob();
        }
    }


}
