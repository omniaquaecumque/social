using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public GameObject shieldObject;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F)) {
            DefenseBar.instance.UseDefense(1);
            shieldObject.SetActive(true);
        }
        else {
            shieldObject.SetActive(false);
        }
    }

    
}
