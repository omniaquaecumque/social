using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VivoxUnity;

public class LobbyScreenUI : MonoBehaviour
{

    private VivoxVoiceManager _vivoxVoiceManager;
    private VivoxNetworkManager _vivoxNetworkManager;


    private EventSystem _evtSystem;

    public Button LogoutButton;
    public GameObject LobbyScreen;

    #region Unity Callbacks

    private void Awake()
    {
        _evtSystem = EventSystem.current;
        if (!_evtSystem)
        {
            Debug.LogError("Unable to find EventSystem object.");
        }
        _vivoxVoiceManager = VivoxVoiceManager.Instance;
        _vivoxNetworkManager = FindObjectOfType<VivoxNetworkManager>();
        if (!_vivoxNetworkManager)
        {
            Debug.LogError("Unable to find VivoxNetworkManager object.");
        }
        _vivoxVoiceManager.OnUserLoggedInEvent += OnUserLoggedIn;
        _vivoxVoiceManager.OnUserLoggedOutEvent += OnUserLoggedOut;
        LogoutButton.onClick.AddListener(() => { LogoutOfVivoxService(); });

        if (_vivoxVoiceManager.LoginState == LoginState.LoggedIn)
        {
            OnUserLoggedIn();
        }
        else
        {
            OnUserLoggedOut();
        }
    }

    private void OnDestroy()
    {
        _vivoxVoiceManager.OnUserLoggedInEvent -= OnUserLoggedIn;
        _vivoxVoiceManager.OnUserLoggedOutEvent -= OnUserLoggedOut;
        _vivoxVoiceManager.OnParticipantAddedEvent -= VivoxVoiceManager_OnParticipantAddedEvent;

        LogoutButton.onClick.RemoveAllListeners();
    }

    #endregion

    private void JoinLobbyChannel()
    {
        // Do nothing, participant added will take care of this
        _vivoxVoiceManager.OnParticipantAddedEvent += VivoxVoiceManager_OnParticipantAddedEvent;
        _vivoxVoiceManager.JoinChannel(_vivoxNetworkManager.LobbyChannelName, ChannelType.NonPositional, VivoxVoiceManager.ChatCapability.TextAndAudio);
    }

    private void LogoutOfVivoxService()
    {
        LogoutButton.interactable = false;

        _vivoxVoiceManager.DisconnectAllChannels();

        _vivoxVoiceManager.Logout();
    }

    #region Vivox Callbacks

    private void VivoxVoiceManager_OnParticipantAddedEvent(string username, ChannelId channel, IParticipant participant)
    {
        if (channel.Name == _vivoxNetworkManager.LobbyChannelName && participant.IsSelf)
        {
            // if joined the lobby channel and we're not hosting a match
            // we should request invites from hosts
            _vivoxNetworkManager.SendLobbyUpdate(VivoxNetworkManager.MatchStatus.Seeking);
        }
    }

    private void OnUserLoggedIn()
    {
        LobbyScreen.SetActive(true);
        LogoutButton.interactable = true;
        _evtSystem.SetSelectedGameObject(LogoutButton.gameObject, null);

        var lobbychannel = _vivoxVoiceManager.ActiveChannels.FirstOrDefault(ac => ac.Channel.Name == _vivoxNetworkManager.LobbyChannelName);
        if ((_vivoxVoiceManager && _vivoxVoiceManager.ActiveChannels.Count == 0) 
            || lobbychannel == null)
        {
            JoinLobbyChannel();
        }
        else
        {
            if (lobbychannel.AudioState == ConnectionState.Disconnected)
            {
                // Ask for hosts since we're already in the channel and part added won't be triggered.
                _vivoxNetworkManager.SendLobbyUpdate(VivoxNetworkManager.MatchStatus.Seeking);

                lobbychannel.BeginSetAudioConnected(true, true, ar =>
                {
                    Debug.Log("Now transmitting into lobby channel");
                });
            }

        }
    }

    private void OnUserLoggedOut()
    {
        _vivoxVoiceManager.DisconnectAllChannels();

        LobbyScreen.SetActive(false);
    }

    #endregion
}
