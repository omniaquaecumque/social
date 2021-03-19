using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Task : NetworkBehaviour
{

    [SerializeField] private GameObject _taskWindow;


    public void Use(bool isActive, GameObject U) {

        this.GetComponent<Whoisusing>().UpdateUser(U);
        if (_taskWindow != null && _taskWindow.activeSelf) {
            _taskWindow.GetComponentInChildren<TaskUtil>().MakeVisable();
            _taskWindow.GetComponentInChildren<TaskUtil>().MakeUseable();
        }
        else {
            _taskWindow.SetActive(isActive);
        }
        
    }
}
