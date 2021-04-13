using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class SyncButton : NetworkBehaviour
{
    public int _myNum;
    public Text _NumSynced;

    public GameObject _syncer;

    public AudioSource press;

    public AudioSource release;

    public void onPress()
    {
        press.Play();
        _syncer.GetComponent<GameStartEnd>().updateButton(_myNum, false);
        updateText();
    }

    public void onRelease()
    {
        release.Play();
        _syncer.GetComponent<GameStartEnd>().updateButton(_myNum, true);
        updateText();
    }

    public void updateText() {
        _NumSynced.text = _syncer.GetComponent<GameStartEnd>().getNumSynced() + "/4";
    }


    private void Update()
    {
        updateText();
    }


}




