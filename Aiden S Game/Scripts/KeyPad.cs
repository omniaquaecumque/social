using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class KeyPad : NetworkBehaviour
{
    bool _isReseting = false;
    public string _cardCode;

    GameObject character;

    public GameObject _mytask;
    

    public Text _inputCode;
    public int _codeLength = 6;


    private void OnEnable()
    {
        Debug.Log(_mytask.name);
        string code = string.Empty;
        
        for (int i = 0; i < _codeLength; i++) {
            code += 1 + i;
        }

        _cardCode = code;
        _inputCode.text = string.Empty;

        character = _mytask.GetComponent<Whoisusing>().user;
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
            CompleteTask();
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

    void CompleteTask() {
        character.GetComponent<Pmovement>().inTask = false;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            character.GetComponent<Pmovement>().inTask = false;
            gameObject.SetActive(false);
        }
    }
}
