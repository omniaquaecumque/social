using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;
using VivoxUnity;
using System.Net;
using System.Linq;

public class NetworkingUI : MonoBehaviour
{
    private VivoxNetworkManager _vivoxNetworkManager;
    private VivoxVoiceManager _vivoxVoiceManager;
    private Dictionary<string, Button> _lobbyPlayers = new Dictionary<string, Button>();

    public TextChatUI TextChatUIObj;
    public GameObject LobbyPlayerPrefab;
    public GameObject NetworkingContentObj;
    public Button HostButton;

    #region Unity Callbacks

    private void Awake()
    {
        _vivoxNetworkManager = FindObjectOfType<VivoxNetworkManager>();
        if (!_vivoxNetworkManager)
        {
            Debug.LogError("Unable to find VivoxNetworkManager object.");
        }
        _vivoxVoiceManager = VivoxVoiceManager.Instance;
        _vivoxVoiceManager.OnParticipantAddedEvent += OnParticipantAdded;
        _vivoxVoiceManager.OnTextMessageLogReceivedEvent += OnTextMessageLogReceivedEvent;
        HostButton.onClick.AddListener(() => { HostMatch(); });
    }

    private void OnDestroy()
    {
        _vivoxVoiceManager.OnParticipantAddedEvent -= OnParticipantAdded;
        _vivoxVoiceManager.OnTextMessageLogReceivedEvent -= OnTextMessageLogReceivedEvent;

        HostButton.onClick.RemoveAllListeners();
    }

    #endregion

    /* returns true if JoinButton was actually added */
    private bool AddJoinButton(string hostUserName, string hostDisplayName, string hostIp)
    {
        if (!_lobbyPlayers.ContainsKey(hostUserName))
        {
            GameObject lobbyPlayer = Instantiate(LobbyPlayerPrefab, NetworkingContentObj.transform);
            Button playerButton = lobbyPlayer.GetComponent<Button>();
            playerButton.onClick.AddListener(() => JoinMatch(hostIp));
            lobbyPlayer.GetComponentInChildren<Text>().text = hostDisplayName + "'s Game";
            _lobbyPlayers.Add(hostUserName, playerButton);
            return true;
        }
        return false;
    }

    /* returns true if JoinButton was actually removed */
    private bool RemoveJoinButton(string hostUserName)
    {
        Button buttonToDestroy;
        if (_lobbyPlayers.TryGetValue(hostUserName, out buttonToDestroy))
        {
            buttonToDestroy.onClick.RemoveAllListeners();
            Destroy(buttonToDestroy.gameObject);
            _lobbyPlayers.Remove(hostUserName);
            return true;
        }
        return false;
    }

    private void RemoveAllJoinButtons()
    {
        foreach (var player in _lobbyPlayers)
        {
            Button buttonToDestroy = player.Value;
            _lobbyPlayers.Remove(player.Key);
            buttonToDestroy.onClick.RemoveAllListeners();
            GameObject.Destroy(buttonToDestroy);
        }
    }

    private void HostMatch()
    {
        // StartHost must fire before SendLobbyUpdate
        _vivoxNetworkManager.StartHost();
        _vivoxNetworkManager.SendLobbyUpdate(VivoxNetworkManager.MatchStatus.Open);
        _vivoxNetworkManager.LeaveAllChannels();
    }

    private void JoinMatch(string playerIp)
    {
        _vivoxNetworkManager.networkAddress = playerIp;
        _vivoxNetworkManager.StartClient();
        _vivoxNetworkManager.LeaveAllChannels();
    }

    #region Vivox Callbacks

    private void OnParticipantAdded(string username, ChannelId channel, IParticipant participant)
    {
    }

    private void OnTextMessageLogReceivedEvent(string sender, IChannelTextMessage channelTextMessage)
    {
        // Only handle MatchStatus control signals
        if (String.IsNullOrEmpty(channelTextMessage.ApplicationStanzaNamespace))
            return;

        // If we find a message with this tag we don't push that to the chat box. Messages with this tag are intended to denote an open or closed multiplayer match.
        if (channelTextMessage.ApplicationStanzaNamespace.EndsWith(VivoxNetworkManager.MatchStatus.Open.ToString()))
        {
            if (AddJoinButton(channelTextMessage.Sender.Name, channelTextMessage.Sender.DisplayName, channelTextMessage.ApplicationStanzaBody))
                TextChatUIObj.DisplayHostingMessage(channelTextMessage);
        }
        else if (channelTextMessage.ApplicationStanzaNamespace.EndsWith(VivoxNetworkManager.MatchStatus.Closed.ToString()))
        {
            if (RemoveJoinButton(channelTextMessage.Sender.Name))
                TextChatUIObj.DisplayHostingMessage(channelTextMessage);
        }
    }

    #endregion
}