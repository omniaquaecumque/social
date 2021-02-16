using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : NetworkBehaviour
{
    private Animator m_Animator = null;
    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!hasAuthority) { return; }
    }

    //Player enter triggering range
    void onTriggerEnter(Collider other)
    {
        m_Animator.SetBool("character_nearby", true);
    }

    //Player exit triggering range
    void onTriggerExit(Collider other)
    {
        m_Animator.SetBool("character_nearby", false);
    }
}
