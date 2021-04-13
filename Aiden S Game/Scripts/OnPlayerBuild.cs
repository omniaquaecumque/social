using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class OnPlayerBuild : NetworkBehaviour
{
    private List<Color> avalibleColors;

    public GameObject _GameManager;

    [SyncVar(hook = nameof(OnJobUpdate))]
    public List<GameObject> _myTasksT1;

    [SyncVar(hook = nameof(OnJobBoolUpdate))]
    public List<bool> _completedT1;

    [SyncVar(hook = nameof(OnColorUpdate))]
    public Color _myColor;

    [SyncVar]
    public int _myInt;

    void OnColorUpdate(Color oldColor, Color newColor) {
        this.gameObject.GetComponent<Renderer>().material.color = newColor;
    }

    void OnJobUpdate(List<GameObject> old, List<GameObject> newList) { 
        _myTasksT1 = new List<GameObject>(newList);
    }

    void OnJobBoolUpdate(List<bool> old, List<bool> newList) {
        _completedT1 = new List<bool>(newList);
    }


    public override void OnStartLocalPlayer()
    {
        _GameManager = GameObject.Find("GameManager");
        _GameManager.GetComponent<GameStorage>().MakePlayer(this.gameObject);


        //set hud text for owned tasks
        string ret = string.Empty;
        if (isLocalPlayer) {
            for (int i = 0; i < _myTasksT1.Count; i++) {
                ret += _myTasksT1[i].name + "\n";
            }
            _GameManager.GetComponent<GameStorage>()._HudText.text = ret;
        }

        //send player data to server
        CmdSetupPlayer(_myColor, _myTasksT1, _completedT1, _myInt);
    }
    
    //setup new player on the server
    [Command]
    public void CmdSetupPlayer(Color Pcolor, List<GameObject> tasks, List<bool> complete, int newint) {
        _myColor = Pcolor;
        _myTasksT1 = new List<GameObject>(tasks);
        _completedT1 = new List<bool>(complete);
        _myInt = newint;
    }


    //if client disconnects send player data back to the pool of avalible players
    public override void OnStopClient()
    {
        if (this != null)
        {
            GameObject temp = GameObject.Find("GameManager");
            if(temp != null) {
                temp.GetComponent<GameStorage>().AddMissing(2f);
            }
            
        }
    }

}
