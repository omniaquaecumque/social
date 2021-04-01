using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class KeyPad : NetworkBehaviour
{
    public int _myInt;

    bool _isReseting = false;
    public string _cardCode;

    public GameObject _codeGold;
    public GameObject _codeGreen;

    public Image _Input1;
    public Image _Input2;

    public Color[] _colors = new Color[2];

    public GameObject _GameManager;

    public Text _inputCode;
    public int _codeLength = 8;

    private void OnEnable()
    {
        string code = string.Empty;

        if (Random.Range(0, 2) == 0)
        {
            _Input1.color = _colors[0];
            _Input2.color = _colors[1];

            code += _GameManager.GetComponent<GameStorage>().KeyPadSubparts[0];
            code += _GameManager.GetComponent<GameStorage>().KeyPadSubparts[1];
        }
        else {
            _Input1.color = _colors[1];
            _Input2.color = _colors[0];
            
            code += _GameManager.GetComponent<GameStorage>().KeyPadSubparts[1];
            code += _GameManager.GetComponent<GameStorage>().KeyPadSubparts[0];

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
            _GameManager.GetComponent<GameStorage>().TaskComplete(true, _myInt);
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

    void Update()
    {
        
    }
}
