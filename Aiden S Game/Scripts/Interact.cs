using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Interact : NetworkBehaviour
{
    bool interact = false;

    GameObject _task;

    GameObject _GenTasks;

    GameObject GameManager;


    public override void OnStartLocalPlayer() {
        _GenTasks = GameObject.Find("GeneralTasks");
        GameManager = GameObject.Find("GameManager");
    }

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

        if (_task != null && isLocalPlayer) {
            if (_task.name == "Matching" && (!GameManager.GetComponent<GameStorage>().TasksCompleted[4] || !GameManager.GetComponent<GameStorage>().TasksCompleted[5] || !GameManager.GetComponent<GameStorage>().TasksCompleted[6]))
            {
                return;
            }

            if (_task.name == "KeyPad" && (!GameManager.GetComponent<GameStorage>().TasksCompleted[1] || !GameManager.GetComponent<GameStorage>().TasksCompleted[2]))
            {
                return;

            }
            if (interact && Input.GetKeyDown(KeyCode.E) && isLocalPlayer && (this.GetComponent<OnPlayerBuild>()._myTasksT1.Contains(_task) || _GenTasks.GetComponent<GeneralTasks>()._generalTasks.Contains(_task)))
            {
                PlayJob();
            }


        }
       
    }


}
