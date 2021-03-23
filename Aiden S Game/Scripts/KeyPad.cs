using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class KeyPad : NetworkBehaviour
{

    
    //public CanvasGroup _visable;

    //bool leftTask = false;

    bool _isReseting = false;
    public string _cardCode;

    public GameObject _codeGold;
    public GameObject _codeGreen;

    public Image _Input1;
    public Image _Input2;

    public Color[] _colors = new Color[2];

    //GameObject character;

    //public GameObject _mytask;
    

    public Text _inputCode;
    public int _codeLength = 8;

    //public void MakeVisable() {
    //    _visable.alpha = 1;
    //    _visable.interactable = true;
    //}

    //public void MakeUseable() {
    //    leftTask = false;
    //}

    private void OnEnable()
    {
        //this.MakeVisable();

        string code = string.Empty;

        if (Random.Range(0, 2) == 0)
        {
            _Input1.color = _colors[0];
            _Input2.color = _colors[1];

            for (int i = 0; i < _codeGold.GetComponent<KeyPadSubParts>()._codePart.Length; i++)
            {
                code += _codeGold.GetComponent<KeyPadSubParts>()._codePart[i];
            }
            for (int i = 0; i < _codeGreen.GetComponent<KeyPadSubParts>()._codePart.Length; i++)
            {
                code += _codeGreen.GetComponent<KeyPadSubParts>()._codePart[i];
            }
        }
        else {
            _Input1.color = _colors[1];
            _Input2.color = _colors[0];

            for (int i = 0; i < _codeGreen.GetComponent<KeyPadSubParts>()._codePart.Length; i++)
            {
                code += _codeGreen.GetComponent<KeyPadSubParts>()._codePart[i];
            }
            for (int i = 0; i < _codeGold.GetComponent<KeyPadSubParts>()._codePart.Length; i++)
            {
                code += _codeGold.GetComponent<KeyPadSubParts>()._codePart[i];
            }

        }

        _cardCode = code;
        _inputCode.text = string.Empty;
    }

    public void ButtonClick(int number)
    {

        if (_isReseting) {
            return;
        }
        _inputCode.text += number;

        if (_inputCode.text == _cardCode)
        {
            _inputCode.text = "Correct";
            StartCoroutine(ResetCode());
            this.GetComponent<TaskUtil>().CompleteTask();
            _isReseting = true;
        }
        else if (_inputCode.text.Length >= _codeLength) {
            _inputCode.text = "Incorrect";
            StartCoroutine(ResetCode());
        }
    }

    private IEnumerator ResetCode() {
        _isReseting = true;
        yield return new WaitForSeconds(0.5f);
        _inputCode.text = string.Empty;
        _isReseting = false;
    }

  

    // Update is called once per frame
    void Update()
    {

        //if (!leftTask) {
        //    if (Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        character.GetComponent<Pmovement>().inTask = false;
        //        _visable.alpha = 0;
        //        _visable.interactable = false;
        //        leftTask = true;
        //    }

        //}
        
    }
}
