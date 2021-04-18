using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class GamePlayer : NetworkBehaviour
{
    [SyncVar]
    private string displayName = "Loading...";

    [Header("UI")]
    [SerializeField] GameObject winPanel = null;

    private NetworkManagerHide room;
    private NetworkManagerHide Room
    {
        get
        {
            if (room != null) { return room; }
            return room = NetworkManager.singleton as NetworkManagerHide;
        }
    }

    public override void OnStartClient()
    {
        DontDestroyOnLoad(gameObject);

        Room.GamePlayers.Add(this);

        gameObject.transform.position = new Vector3(35, -5, 15);
    }

    public override void OnStopClient()
    {
        Room.GamePlayers.Remove(this);
    }

    [Server]
    public void SetDisplayName(string displayName)
    {
        this.displayName = displayName;
        this.name = displayName;
    }

    [Server]
    public void ShowWinMsg()
    {
        winPanel.SetActive(true);
    }

    public void ChangeTag(GameObject hide)
    {
        Debug.Log(hide.name);
        hide.tag = "hidden";
    }
}
