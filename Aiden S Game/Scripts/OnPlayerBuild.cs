using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class OnPlayerBuild : NetworkBehaviour
{
    
    
    
    private List<Color> avalibleColors;
    //private List<GameObject> avalibleJobs1;
    //private List<GameObject> avalibleJobs2;

    //public int T1PlayerTasks;
    //public int T2PlayerTasks;

    private GameObject _GameManager;

    [SyncVar(hook = nameof(OnJobUpdate))]
    public List<GameObject> _myTasksT1;
    //private List<GameObject> _myTasksT2;
    [SyncVar(hook = nameof(OnJobBoolUpdate))]
    public List<bool> _completedT1;
    //private List<bool> _completedT2;

    [SyncVar(hook = nameof(OnColorUpdate))]
    public Color _myColor;

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
        CmdSetupPlayer(_myColor, _myTasksT1, _completedT1);
    }

    public override void OnStopClient() {
        GameObject temp = GameObject.Find("GameManager");
        temp.GetComponent<GameStorage>().AddMissing();
     }

    [Command]
    public void CmdSetupPlayer(Color Pcolor, List<GameObject> tasks, List<bool> complete) {
        _myColor = Pcolor;
        _myTasksT1 = new List<GameObject>(tasks);
        _completedT1 = new List<bool>(complete);
    }

}
