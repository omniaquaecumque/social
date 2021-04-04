﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;

public class LobbyRoom : NetworkBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject lobbyUI = null;
    [SerializeField] private TMP_Text[] playerNameTexts = new TMP_Text[5];
    [SerializeField] private TMP_Text[] playerReadyTexts = new TMP_Text[5];
    [SerializeField] private Button startGameButton = null;

    [SyncVar(hook = nameof(HandleDisplayNameChanged))]
    public string DisplayName = "Loading...";
    [SyncVar(hook = nameof(HandleReadyStatusChanged))]
    public bool IsReady = false;

    private bool isLeader;
    public bool IsLeader
    {
        get { return isLeader; }
        set
        {
            isLeader = value;
            startGameButton.gameObject.SetActive(value);
        }
    }

    private NetworkManagerHide room;
    private NetworkManagerHide Room
    {
        get
        {
            if (room != null) { return room; }
            return room = NetworkManager.singleton as NetworkManagerHide;
        }
    }

    public override void OnStartAuthority()
    {
        CmdSetDisplayName(PlayerPrefs.GetString("PlayerName"));
        lobbyUI.SetActive(true);
    }

    public override void OnStartClient()
    {
        Room.RoomPlayers.Add(this);
        gameObject.name = DisplayName;
        UpdateDisplay();
    }

    public override void OnStopClient()
    {
        Room.RoomPlayers.Remove(this);

        UpdateDisplay();
    }

    public void HandleReadyStatusChanged(bool oldValue, bool newValue) => UpdateDisplay();
    public void HandleDisplayNameChanged(string oldValue, string newValue) => UpdateDisplay();

    private void UpdateDisplay()
    {
        if (!hasAuthority)
        {
            foreach (var player in Room.RoomPlayers)
            {
                if (player.hasAuthority)
                {
                    player.UpdateDisplay();
                    break;
                }
            }

            return;
        }

        /*
        for (int i = 0; i < playerNameTexts.Length; i++)
        {
            playerNameTexts[i].text = "Waiting For Player...";
            playerReadyTexts[i].text = string.Empty;
        }*/

        for (int i = 0; i < Room.RoomPlayers.Count; i++)
        {
            lobbyUI.SetActive(false);
            playerNameTexts[i].text = Room.RoomPlayers[i].DisplayName;
            playerReadyTexts[i].text = Room.RoomPlayers[i].IsReady ?
                "<color=green>Ready</color>" :
                "<color=red>Not Ready</color>";
            lobbyUI.SetActive(true);
        }
    }

    public void HandleReadyToStart(bool readyToStart)
    {
        if (!isLeader) { return; }

        startGameButton.interactable = readyToStart;
    }

    [Command]
    private void CmdSetDisplayName(string displayName)
    {
        DisplayName = displayName;
    }

    [Command]
    public void CmdReadyUp()
    {
        IsReady = !IsReady;

        Room.NotifyPlayersOfReadyState();
    }

    [Command]
    public void CmdStartGame()
    {
        if (Room.RoomPlayers[0].connectionToClient != connectionToClient) { return; }

        Room.StartGame();
    }
}

