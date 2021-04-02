using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TaskUtil : MonoBehaviour
{
    public bool _leftTask = false;
    public GameObject _character;
    public GameObject _mytask;

    public CanvasGroup _visable;

    private void OnEnable()
    {
        _character = _mytask.GetComponent<Whoisusing>().user;
    }

    public void MakeVisable()
    {
        _visable.alpha = 1;
        _visable.interactable = true;
        _visable.blocksRaycasts = true;
    }

    public void MakeUseable()
    {
        _leftTask = false;
    }

    public void CompleteTask()
    {
        _character.GetComponent<Pmovement>().inTask = false;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_leftTask)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _character.GetComponent<Pmovement>().inTask = false;
                _visable.alpha = 0;
                _visable.interactable = false;
                _visable.blocksRaycasts = false;
                _leftTask = true;
            }

        }
    }
}
