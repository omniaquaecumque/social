using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class Matching : NetworkBehaviour
{
    
    
    private List<Color> _WireColors;

    private List<int> _P1BottomWireIndex;

    private List<int> _P1TopWireIndex;

    private List<int> _P2TopWireIndex;

    private List<int> _playerIndex;

    private int NumCorrect = 0;

    public int _myInt;
    
    public GameObject _GameManager;


    public GameObject[] _players = new GameObject[3];

    public Text[] _names = new Text[3];

    public Text[] _majors = new Text[3];

    public List<PlayerSprite> _Playersaw = new List<PlayerSprite>(3); //Needs to get switched to sprites

    public List<Color> _PlayerColors = new List<Color>();

    public List<Wire> _P1BottomWires = new List<Wire>();

    public List<Wire> _P1TopWires = new List<Wire>();

    public List<Wire> _P2BottomWires = new List<Wire>();

    public List<Wire> _P2TopWires = new List<Wire>();

    public List<int> _correctP1 = new List<int>(3);

    public List<int> _correctP2 = new List<int>(3);

    public Wire CurrentWire;

    public Wire CurrentHovered;


    //Update the number of correctly placed wires (called when wire is attached) returns true if all 6 wires are placed correctly
    public bool CompareCorrect() {
        //if the placed wire was in the bottom set of wires
        if (_P1BottomWires.Contains(CurrentWire))
        {
            int index1 = _P1BottomWires.IndexOf(CurrentWire);
            int index2 = _P1TopWires.IndexOf(CurrentHovered);
            if (_correctP1[index1] == index2)
            {
                NumCorrect++;
                if (NumCorrect == 6) {
                    _GameManager.GetComponent<GameStorage>().TaskComplete(true, _myInt);
                    this.gameObject.GetComponent<TaskUtil>().CompleteTask();
                }
                return true;
            }
        }
        else
        {
            //if the placed wire was in the top set of wires
            int index1 = _P2BottomWires.IndexOf(CurrentWire);
            int index2 = _P2TopWires.IndexOf(CurrentHovered);
            if (_correctP2[index1] == index2)
            {
                NumCorrect++;
                if (NumCorrect == 6)
                {
                    _GameManager.GetComponent<GameStorage>().TaskComplete(true, _myInt);
                    this.gameObject.GetComponent<TaskUtil>().CompleteTask();
                }
                return true;
            }

        }
        return false;
    }


    public void DecrementCorrect() {     
        NumCorrect--;
    }


    private void Start()
    {

        //intilize lists
        _WireColors = new List<Color>(_PlayerColors);
        _P1BottomWireIndex = new List<int>();
        _P1TopWireIndex = new List<int>();
        _P2TopWireIndex = new List<int>();
        _playerIndex = new List<int>();

        for (int i = 0; i < _P1BottomWires.Count; i++) {
            _P1BottomWireIndex.Add(i);
            _P1TopWireIndex.Add(i);
            _P2TopWireIndex.Add(i);
            _playerIndex.Add(i);
        }


        //matching assignment
        while (_WireColors.Count > 1 && _P1BottomWireIndex.Count > 0 && _P1TopWireIndex.Count > 0 && _P2TopWireIndex.Count > 0 && _playerIndex.Count > 0) {

            int player = Random.Range(0, _playerIndex.Count);
            Color pickedColor = _GameManager.GetComponent<GameStorage>().DataInputColors[_playerIndex[player]];

            int pickedBottomWireP1 = Random.Range(0, _P1BottomWireIndex.Count);
            int pickedTopWireP1 = Random.Range(0, _P1TopWireIndex.Count);
            int pickedTopWireP2 = Random.Range(0, _P2TopWireIndex.Count);
            
            //assign bottom set wire and place name
            _correctP1[_P1BottomWireIndex[pickedBottomWireP1]] = _P1TopWireIndex[pickedTopWireP1];
            _names[_P1TopWireIndex[pickedTopWireP1]].text = _GameManager.GetComponent<GameStorage>().DataInputNames[_playerIndex[player]];

            //assign top set wire and place major
            _correctP2[_P1TopWireIndex[pickedTopWireP1]] = _P2TopWireIndex[pickedTopWireP2];
            _majors[_P2TopWireIndex[pickedTopWireP2]].text = _GameManager.GetComponent<GameStorage>().DataInputMajors[_playerIndex[player]];

            //set wire color 
            _P1BottomWires[_P1BottomWireIndex[pickedBottomWireP1]].SetColor(pickedColor);
            //set player icon color
            _Playersaw[_P1BottomWireIndex[pickedBottomWireP1]].SetColor(pickedColor);

            //remove set index for random selection
            _playerIndex.Remove(_playerIndex[player]);
            _WireColors.Remove(pickedColor);
            _P1BottomWireIndex.Remove(_P1BottomWireIndex[pickedBottomWireP1]);
            _P1TopWireIndex.Remove(_P1TopWireIndex[pickedTopWireP1]);
            _P2TopWireIndex.Remove(_P2TopWireIndex[pickedTopWireP2]);
        }
    }

    public void ClearAll() {
        CurrentWire = null;
        NumCorrect = 0;
        for (int i = 0; i < 3; i++) {
            _P1BottomWires[i].Clear();
            _P2BottomWires[i].Clear();
            _P1TopWires[i].attatched = false;
            _P2TopWires[i].attatched = false;
        }
    
    }
    void Update()
    {
        
        //if task gets hidden clear all drawn lines
        if (this.gameObject.GetComponent<CanvasGroup>().alpha == 0)
        {
            this.ClearAll();
        }

    }




}
