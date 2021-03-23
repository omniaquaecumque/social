using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class KeyPadSubParts : NetworkBehaviour
{

    public Text _OutputCode;
    public int _codeLength = 4;

    public Slider _slider;

    public int[] _codePart = new int[4];

    bool beenClicked = false;


    private void OnEnable()
    {

        for (int i = 0; i < _codeLength; i++)
        {
            _codePart[i] = Random.Range(1, 10);
        }
        _OutputCode.text = string.Empty;
    }

    public void ButtonClick()
    {
        Debug.Log("Clicked");

        if (!beenClicked)
        {
            Debug.Log("Fell");
            beenClicked = true;
            InvokeRepeating("increaseSlider", 0.5f, 1.0f);
        }

    }

    public void increaseSlider() {
        Debug.Log("Invoked");
        if (_slider.value < 1)
        {
            Debug.Log("Invoked Fell");
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

        randomizeout(_slider.value);

    }
}
