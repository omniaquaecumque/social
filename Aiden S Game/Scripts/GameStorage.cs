using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class GameStorage : NetworkBehaviour
{    
    [SyncVar]
    public bool timerStart;

    [SyncVar]
    public int time;

    public Text _TimeText;

    public Text _HudText;

    public List<Color> _PlayerColors;

    public List<GameObject> _Round1Tasks = new List<GameObject>();

    public List<GameObject> _Round2Tasks = new List<GameObject>();

    public JobSet[] _JobSets  = new JobSet[4];

    public SyncList<int> _indexes = new SyncList<int>{0, 1, 2, 3};

    public SyncList<int> activePlayers = new SyncList<int>();

    public SyncList<Color> DataInputColors = new SyncList<Color>() { Color.white, Color.white, Color.white };

    public SyncList<string> DataInputNames = new SyncList<string>() { "", "", "" };

    public SyncList<string> DataInputMajors = new SyncList<string>() { "", "", "" };

    public SyncList<string> KeyPadSubparts = new SyncList<string>() { "", ""};

    public SyncList<bool> TasksCompleted = new SyncList<bool>() { false, false, false, false, false, false, false, false };

    [SyncVar]
    public bool Lost;

    [SyncVar]
    public bool Won;

    public bool firstWinLose = false;

    public GameObject YouLose;

    public GameObject YouWin;


    [Command(ignoreAuthority = true)]
    public void AddDataInputColor(Color color, int myInt) {
        DataInputColors[myInt] = color;
    }

    [Command(ignoreAuthority = true)]
    public void AddDataInputName(string name, int myNum) {
        DataInputNames[myNum - 4] = name;
    }

    [Command(ignoreAuthority = true)]
    public void AddDataInputMajor(string name, int myNum)
    {
        DataInputMajors[myNum - 4] = name;
    }

    [Command(ignoreAuthority = true)]
    public void AddKeyPadSubpart(string name, int myNum)
    {
        KeyPadSubparts[myNum - 1] = name;
    }

    [Command(ignoreAuthority = true)]
    public void TaskComplete(bool complete, int myNum) {
        TasksCompleted[myNum] = complete;
    }


    [Command(ignoreAuthority = true)]
    public void IndexesAdd(int i) {
        _indexes.Add(i);
        activePlayers.Remove(i);
    }

    [Command (ignoreAuthority = true)]
    public void RemoveInt(int remove) {
        _indexes.Remove(remove);
        AddInt(remove);

    }

    [Command(ignoreAuthority = true)]
    public void AddInt(int add) {
        activePlayers.Add(add);
    }

    [Command(ignoreAuthority = true)]
    public void AddMissing(float delayTime) {
        StartCoroutine(waitAddMissing(delayTime));
    
    }

    //on client disconnect add back in the index that the player used
    IEnumerator waitAddMissing(float delayTime) {

        yield return new WaitForSeconds(delayTime);

        GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");

        List<int> curPlayers = new List<int>();


        for (int i = 0; i < Players.Length; i++) {
            if(Players[i] != null) {
                curPlayers.Add(Players[i].GetComponent<OnPlayerBuild>()._myInt);
            }
            
        }

        for (int i = 0; i < activePlayers.Count; i++) {
            if (!curPlayers.Contains(activePlayers[i])) {
                IndexesAdd(activePlayers[i]);
            }
        }
    
    }


    public void MakePlayer(GameObject player) {
        //choose index
        int setIndex = Random.Range(0, _indexes.Count);
        int listindex = _indexes[setIndex];


        //build player based on the jobset
        player.GetComponent<OnPlayerBuild>()._myInt = listindex;

        player.GetComponent<OnPlayerBuild>()._myColor = _PlayerColors[listindex];
        player.GetComponent<Renderer>().material.color = player.GetComponent<OnPlayerBuild>()._myColor;

        player.GetComponent<OnPlayerBuild>()._myTasksT1 = new List<GameObject>(_JobSets[listindex]._jobs);

        for (int i = 0; i < player.GetComponent<OnPlayerBuild>()._myTasksT1.Count; i++)
        {
            player.GetComponent<OnPlayerBuild>()._completedT1.Add(false);
        }

        RemoveInt(listindex);
    }


    private void Update()
    {

        //check for a win/loss
        if (firstWinLose == false)
        {
            if (Lost == true)
            {
                YouLose.SetActive(true);
                firstWinLose = true;
            }
            if (Won == true) {
                YouWin.SetActive(true);
                firstWinLose = true;
            }

        }

        //update the timer
        if (timerStart)
        {
            int div = time / 60;
            int sec = time - div * 60;

            if (sec < 10)
            {
                _TimeText.text = div + ":0" + sec;
                return;
            }
            _TimeText.text = div + ":" + sec;
        }

        
    }






}
