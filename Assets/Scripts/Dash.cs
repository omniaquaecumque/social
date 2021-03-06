using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dashSpeed = 200; 
    Rigidbody rig; 
    bool isDashing; 

    // public GameObject dashEffect; 

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            isDashing = true; 
        }
    }

    void FixedUpdate() {
        if (isDashing) {
            Dashing();
        }
    }

    private void Dashing() {
        rig.AddForce(transform.forward * dashSpeed, ForceMode.Impulse);
        isDashing = false; 
    }
}
