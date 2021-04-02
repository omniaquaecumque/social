using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class GameStartEnd : NetworkBehaviour
{
    public GameObject _Gamemanager;

    public GameObject Win;

    public GameObject _Timer;

    public List<GameObject> InspectWalls = new List<GameObject>();

    public SyncList<GameObject> Walls = new SyncList<GameObject>();

    public SyncList<bool> buttonsHeld = new SyncList<bool>() { false, false, false, false };

    public bool started = false;


    public override void OnStartServer()
    {
        for (int i = 0; i < InspectWalls.Count; i++) {
            Walls.Add(InspectWalls[i]);
        }
    }

    [Command(ignoreAuthority = true)]
    public void updateButton(int index, bool val) {
        buttonsHeld[index] = val;
        if (allTrue()) {
            if (!started) {
                started = true;
                for (int i = 0; i < Walls.Count; i++) {
                    Walls[i].transform.Translate(Vector3.forward * 10); 
                }

                startTime();
                return;
            }
            if (allTasksDone() && !_Gamemanager.GetComponent<GameStorage>().Lost) {
                EndGame();
            }
        }
    }


    public bool allTrue() {
        for (int i = 0; i < buttonsHeld.Count; i++) {
            if (buttonsHeld[i] == false) {
                return false;
            }
        
        }
        return true;
    }

    public bool allTasksDone() {
        for (int i = 0; i < _Gamemanager.GetComponent<GameStorage>().TasksCompleted.Count; i++)
        {
            if (_Gamemanager.GetComponent<GameStorage>().TasksCompleted[i] == false)
            {
                return false;
            }

        }
        return true;

    }

    public int getNumSynced() {
        int count = 0;
        for (int i = 0; i < buttonsHeld.Count; i++) {
            if (buttonsHeld[i]) {
                count++;
            }
        }

        return count;
    
    }

    public void startTime() {
            _Timer.GetComponent<Timer>().StartTimer();
        }

    public void EndGame() {
        _Gamemanager.GetComponent<GameStorage>().Won = true;
    }




}
