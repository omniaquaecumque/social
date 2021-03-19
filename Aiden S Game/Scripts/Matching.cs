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

    private int NumCorrect = 0;

    public List<PlayerSprite> _Players = new List<PlayerSprite>(); //Needs to get switched to sprites

    public List<Color> _PlayerColors = new List<Color>();

    public List<Wire> _P1BottomWires = new List<Wire>();

    public List<Wire> _P1TopWires = new List<Wire>();

    public List<Wire> _P2BottomWires = new List<Wire>();

    public List<Wire> _P2TopWires = new List<Wire>();

    public List<int> _correctP1 = new List<int>();

    public List<int> _correctP2 = new List<int>();

    public Wire CurrentWire;

    public Wire CurrentHovered;

    public bool CompareCorrect() {
        Debug.Log("Comparing Correct");
        Debug.Log(NumCorrect);
        
        if (_P1BottomWires.Contains(CurrentWire))
        {
            int index1 = _P1BottomWires.IndexOf(CurrentWire);
            int index2 = _P1TopWires.IndexOf(CurrentHovered);
            if (_correctP1[index1] == index2)
            {
                Debug.Log("Correct Placement");
                NumCorrect++;
                if (NumCorrect == 6) {
                    this.gameObject.GetComponent<TaskUtil>().CompleteTask();
                }
                Debug.Log(NumCorrect);
                return true;
            }
        }
        else
        {
            int index1 = _P2BottomWires.IndexOf(CurrentWire);
            int index2 = _P2TopWires.IndexOf(CurrentHovered);
            if (_correctP2[index1] == index2)
            {
                Debug.Log("Correct Placement");
                NumCorrect++;
                if (NumCorrect == 6)
                {
                    this.gameObject.GetComponent<TaskUtil>().CompleteTask();
                }
                Debug.Log(NumCorrect);
                return true;
            }

        }
        Debug.Log("Incorrect Placement");

        Debug.Log(NumCorrect);

        return false;
    }


    public void DecrementCorrect() {
        Debug.Log("Reducing Correct By 1");
        
        NumCorrect--;
       
        Debug.Log(NumCorrect);
    }


    private void Start()
    {
        _WireColors = new List<Color>(_PlayerColors);
        _P1BottomWireIndex = new List<int>();
        _P1TopWireIndex = new List<int>();
        _P2TopWireIndex = new List<int>();

        for (int i = 0; i < _P1BottomWires.Count; i++) {
            _P1BottomWireIndex.Add(i);
            _P1TopWireIndex.Add(i);
            _P2TopWireIndex.Add(i);
        }

        while (_WireColors.Count > 1 && _P1BottomWireIndex.Count > 0 && _P1TopWireIndex.Count > 0 && _P2TopWireIndex.Count > 0) {
            Color pickedColor = _WireColors[Random.Range(0, _WireColors.Count)];
            int pickedBottomWireP1 = Random.Range(0, _P1BottomWireIndex.Count);
            int pickedTopWireP1 = Random.Range(0, _P1TopWireIndex.Count);
            int pickedTopWireP2 = Random.Range(0, _P2TopWireIndex.Count);

            _P1BottomWires[_P1BottomWireIndex[pickedBottomWireP1]].SetColor(pickedColor);
            _Players[_P1BottomWireIndex[pickedBottomWireP1]].SetColor(pickedColor);


            //_P1TopWires[_P1TopWireIndex[pickedTopWireP1]].SetColor(pickedColor);
            //_P2BottomWires[_P1TopWireIndex[pickedTopWireP1]].SetColor(pickedColor);

            //_P2TopWires[_P2TopWireIndex[pickedTopWireP2]].SetColor(pickedColor);


            _WireColors.Remove(pickedColor);
            _P1BottomWireIndex.Remove(_P1BottomWireIndex[pickedBottomWireP1]);
            _P1TopWireIndex.Remove(_P1TopWireIndex[pickedTopWireP1]);
            _P2TopWireIndex.Remove(_P2TopWireIndex[pickedTopWireP2]);
        }
    }

    public void ClearAll() {
        for (int i = 0; i < 3; i++) {
            _P1BottomWires[i].Clear();
            _P2BottomWires[i].Clear();
        }
    
    }
    void Update()
    {

        if (NumCorrect == 6)
        {
            this.gameObject.GetComponent<TaskUtil>().CompleteTask();
        }

        if (this.gameObject.GetComponent<CanvasGroup>().alpha == 0)
        {
            this.ClearAll();
        }

    }




}
