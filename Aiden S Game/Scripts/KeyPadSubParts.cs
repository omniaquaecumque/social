﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class KeyPadSubParts : NetworkBehaviour
{

    public int _myNum;

    public Text _OutputCode;
    public int _codeLength = 4;

    public Slider _slider;

    public int[] _codePart = new int[4];

    public string _MyCode = string.Empty;

    bool beenClicked = false;

    public GameObject _GameManager;

    public bool DataSent = false;

    public bool updating = true;


    private void OnEnable()
    {

        for (int i = 0; i < _codeLength; i++)
        {
            int num = Random.Range(1, 10);
            _codePart[i] = num;
            _MyCode += num;

        }
        _OutputCode.text = string.Empty;
    }

    public void ButtonClick()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            InvokeRepeating("increaseSlider", 0.5f, 1.0f);
        }

    }

    public void increaseSlider() {
        if (_slider.value < 1)
        {
            _slider.value += 0.025f;
        }
    }

    public void randomizeout(float sliderval) {
        string code = string.Empty;

        for (int b = 0; b < (int)(sliderval / 0.25f); b++) {
            code += _codePart[b] + "\t";
        }

        for (int i = (int)(sliderval / 0.25f); i < _codeLength; i++)
        {
            code += Random.Range(1, 10) + "\t";
        }
        code = code.Trim('\t');

        _OutputCode.text = code;
    }

    // Update is called once per frame
    void Update()
    {
        if (!DataSent)
        {
            randomizeout(_slider.value);
            if (_slider.value >= 1) {
                _GameManager.GetComponent<GameStorage>().AddKeyPadSubpart(_MyCode, _myNum);
                _GameManager.GetComponent<GameStorage>().TaskComplete(true, _myNum);
                DataSent = true;
            }
        }
        
    }
}
