using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderEndTrigger : MonoBehaviour
{
    public GameManager gameManager; 

    void OnTriggerEnter() 
    {
        gameManager.CompleteDefenderLevel();
    }
}
