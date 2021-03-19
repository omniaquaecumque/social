using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TaskUtil : MonoBehaviour
{
    bool leftTask = false;
    GameObject character;
    public GameObject _mytask;

    public CanvasGroup _visable;

    private void OnEnable()
    {
        character = _mytask.GetComponent<Whoisusing>().user;
    }

    public void MakeVisable()
    {
        _visable.alpha = 1;
        _visable.interactable = true;
        _visable.blocksRaycasts = true;
    }

    public void MakeUseable()
    {
        leftTask = false;
    }

    public void CompleteTask()
    {
        character.GetComponent<Pmovement>().inTask = false;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!leftTask)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                character.GetComponent<Pmovement>().inTask = false;
                _visable.alpha = 0;
                _visable.interactable = false;
                _visable.blocksRaycasts = false;
                leftTask = true;
            }

        }
    }
}
