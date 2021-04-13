using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class TempButton : NetworkBehaviour
{

    public AudioSource _aud;

    public void onButtonClick() {
        _aud.Play();
    }


}
