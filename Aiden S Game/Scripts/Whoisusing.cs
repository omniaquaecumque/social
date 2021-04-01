using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Whoisusing : NetworkBehaviour
{
    public bool _DataInputTask;

    public int _MyNum;
    
    public GameObject user = null;

    public GameObject _Gamemanager;

    public void UpdateUser(GameObject newuser) {
        user = newuser;
        if (_DataInputTask) { 
            _Gamemanager.GetComponent<GameStorage>().AddDataInputColor(newuser.GetComponent<Renderer>().material.color, _MyNum); 
        }
        
    }
}
