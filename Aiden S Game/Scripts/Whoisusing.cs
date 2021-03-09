using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Whoisusing : NetworkBehaviour
{
    public GameObject user = null;


    public void UpdateUser(GameObject newuser) {
        user = newuser;
    }
}
