using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
 
public class FollowPlayer : MonoBehaviour
{
    public GameObject tPlayer;
    public Transform tFollowTarget;
    private CinemachineVirtualCamera vcam;
 
    void Start()
    {
        var vcam = GetComponent<CinemachineVirtualCamera>();
    }
 
    void Update()
    {
        if (tPlayer == null)
        {
            tPlayer = GameObject.FindWithTag("Player");
        }
        tFollowTarget = tPlayer.transform;
        var newPlayer = Instantiate(tFollowTarget);
        vcam.m_Follow = newPlayer;
    }
}