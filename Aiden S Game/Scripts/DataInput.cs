using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class DataInput : NetworkBehaviour
{
    public int _myNum;
    
    public Color[] _colors = new Color[2];
    
    public GameObject _name;
    public GameObject _major;

    public Text _nameQuest;
    public Text _majorQuest;

    public Button _submitName;
    public Button _submitMajor;

    public string namSubmitted;
    public string majorSubmitted;

    public GameObject _GameManager;



    public void onClickName() {
        if (_name.GetComponent<Text>().text != "")
        {
            namSubmitted = _name.GetComponent<Text>().text;
            _GameManager.GetComponent<GameStorage>().AddDataInputName(namSubmitted, _myNum);
            _submitName.GetComponent<Image>().color = _colors[1];

            //complete task if both name and major have had valid inputs
            if (_submitMajor.GetComponent<Image>().color == _colors[1])
            {
                _GameManager.GetComponent<GameStorage>().TaskComplete(true, _myNum);
            }
        }

        else {
            //set task to incomplete if text field is empty
            _submitName.GetComponent<Image>().color = _colors[0];
            _GameManager.GetComponent<GameStorage>().TaskComplete(false, _myNum);
        }
    
    }



    public void onClickMajor() {
        if (_major.GetComponent<Text>().text != "")
        {
            majorSubmitted = _major.GetComponent<Text>().text;
            _GameManager.GetComponent<GameStorage>().AddDataInputMajor(majorSubmitted, _myNum);
            _submitMajor.GetComponent<Image>().color = _colors[1];

            //complete task if both name and major have had valid inputs
            if (_submitName.GetComponent<Image>().color == _colors[1]) {
                _GameManager.GetComponent<GameStorage>().TaskComplete(true, _myNum);
            }

        }

        else
        {
            //set task to incomplete if text field is empty
            _submitMajor.GetComponent<Image>().color = _colors[0];
            _GameManager.GetComponent<GameStorage>().TaskComplete(false, _myNum);
        }


    }
}
