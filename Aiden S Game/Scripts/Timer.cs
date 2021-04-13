using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

//only exists on the server
public class Timer : NetworkBehaviour
{

    public bool started = false;

    public int _myTime;

    public GameObject _Gamemanager;

    public void StartTimer() {
        started = true;
        timerStarted();
        InvokeRepeating("DeacreaseTime", 1.0f, 1.0f);
    }

    public void DeacreaseTime() {
        if (_myTime != 0) {
            _myTime = _myTime - 1;
            if (_myTime == 0) {
                sendLost();
            } 
            sendTime();
        }
    }

    //send current time to players
    [ClientRpc]
    public void sendTime() {
        _Gamemanager.GetComponent<GameStorage>().time = _myTime;
    }

    //send that the timer started to players
    [ClientRpc]
    public void timerStarted()
    {
        _Gamemanager.GetComponent<GameStorage>().timerStart = true;
    }

    //if client starts send them the time
    public override void OnStartLocalPlayer()
    {
        sendTime();
    }

    //if timer reaches zero send to the players that they lost
    public void sendLost() {
        _Gamemanager.GetComponent<GameStorage>().Lost = true;
    }

 
}
