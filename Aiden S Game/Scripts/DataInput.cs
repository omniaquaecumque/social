using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class DataInput : NetworkBehaviour
{
    public Color[] _colors = new Color[2];
    
    public GameObject _name;
    public GameObject _major;

    public Text _nameQuest;
    public Text _majorQuest;

    public Button _submitName;
    public Button _submitMajor;

    public string namSubmitted;
    public string majorSubmitted;



    public void onClickName() {
        if (_name.GetComponent<Text>().text != "")
        {
            namSubmitted = _name.GetComponent<Text>().text;
            _submitName.GetComponent<Image>().color = _colors[1];
        }

        else {
            _submitName.GetComponent<Image>().color = _colors[0];
        }
    
    }



    public void onClickMajor() {
        if (_major.GetComponent<Text>().text != "")
        {
            majorSubmitted = _major.GetComponent<Text>().text;
            _submitMajor.GetComponent<Image>().color = _colors[1];
        }

        else
        {
            _submitMajor.GetComponent<Image>().color = _colors[0];
        }


    }
    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
