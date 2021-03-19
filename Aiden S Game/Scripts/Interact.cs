using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Interact : NetworkBehaviour
{
    bool interact = false;

    GameObject _task;

    

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Job") 
        {
            interact = true;
            _task = collision.gameObject;
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Job")
        {
            interact = false;
            _task = null;
        }
    }

    void PlayJob() {
        if (isLocalPlayer) 
        { 
            _task.GetComponent<Task>().Use(true, this.gameObject);
            this.GetComponent<Pmovement>().inTask = true;
        }
    }

    private void Update()
    {

        if (interact && Input.GetKeyDown(KeyCode.E) && isLocalPlayer && (_task.GetComponent<Whoisusing>().user == this.gameObject || _task.GetComponent<Whoisusing>().user == null)) {
            PlayJob();
        }
    }


}
