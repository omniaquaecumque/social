using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Task : NetworkBehaviour
{

    [SerializeField] private GameObject _taskWindow;


    public void Use(bool isActive, GameObject U) {

        this.GetComponent<Whoisusing>().UpdateUser(U);
        Debug.Log(this.GetComponent<Whoisusing>().user.name);
        _taskWindow.SetActive(isActive);
        

    }
}
