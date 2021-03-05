using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System;
using VivoxUnity;
using UnityEngine.Events;
using System.Collections;

public enum TeamColor
{
    None = 0,
    Red,
    Blue
}


//Purpose of that class is syncing data between server - client
public class TankSetup : NetworkBehaviour 
{
    public class OnParticipantProperty : UnityEvent<IParticipant, System.ComponentModel.PropertyChangedEventArgs> { }
    public OnParticipantProperty m_ParticipantPropertyEvent = new OnParticipantProperty();
	private GameObject _m_RosterItem;

    [Header("UI")]
    public GameObject m_NameText;
    public GameObject m_Crown;
    public GameObject m_SpeakingIndicator;
    public GameObject m_RosterItem
	{
		get { return _m_RosterItem; }
		set
		{
			_m_RosterItem = value;
			StartCoroutine(WaitForParticipantSetToSubscribe());
		}
	}
	
	[Header("Network")]
    [Space]
    [SyncVar]
    public Color m_Color;

    // This is the team the player is on during a game.
    [SyncVar(hook = nameof(OnTeamChanged))]
    public TeamColor m_TeamID;

    // Name that appears over a player's head.
    [SyncVar(hook = nameof(OnNameChanged))]
    public string m_PlayerDisplayName;

    [SyncVar]
    public string m_AccountID;

    //this is the player number in all of the players
    [SyncVar]
    public int m_PlayerNumber;

    //This is the local ID when more than 1 player per client
    [SyncVar]
    public int m_LocalID;

    [SyncVar]
    public bool m_IsReady = false;

    public int m_score = 0;

    private int defaultMaxStringLength = 11;
    private GameManager m_gameManager;
    private VivoxVoiceManager m_vivoxVoiceManager;
    private VivoxNetworkManager m_vivoxNetworkManager;
    private IParticipant m_participant;
    private TankHealth m_tankHealth;
    private TankMovement m_TankMovement;
    public PositionalVoice m_inGamePositionalVoice;
    public int AudibleDistance = 32;
    public int ConversationalDistance = 1;
    public float AudioFadeIntensityByDistance = 1.0f;
    public AudioFadeModel AudioFadeModel = AudioFadeModel.InverseByDistance;

    private void OnDestroy()
    {
        m_vivoxVoiceManager.OnParticipantAddedEvent -= VivoxVoiceManager_OnParticipantAddedEvent;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        m_vivoxVoiceManager = VivoxVoiceManager.Instance;
        m_gameManager = FindObjectOfType<GameManager>();
        m_vivoxNetworkManager = FindObjectOfType<VivoxNetworkManager>();
        m_SpeakingIndicator.SetActive(false);
        m_inGamePositionalVoice = GetComponent<PositionalVoice>();
        m_tankHealth = GetComponent<TankHealth>();
        m_TankMovement = GetComponent<TankMovement>();
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        CmdSetPlayerName(VivoxVoiceManager.Instance.LoginSession.Key.Name, VivoxVoiceManager.Instance.LoginSession.Key.DisplayName);

        // Local player will join the positional channel
        if (isLocalPlayer)
        {
            m_gameManager.localTank = gameObject;
            ConversationalDistance = 2;
            AudibleDistance = 38;
            var ChannelProperties = new Channel3DProperties(AudibleDistance, ConversationalDistance, AudioFadeIntensityByDistance, AudioFadeModel);
            // We want to know when we have been added to the channel
            m_vivoxVoiceManager.OnParticipantAddedEvent += VivoxVoiceManager_OnParticipantAddedEvent;

            m_vivoxVoiceManager.JoinChannel(m_vivoxNetworkManager.PositionalChannelName, ChannelType.Positional,
                VivoxVoiceManager.ChatCapability.TextAndAudio, true, ChannelProperties);
        }
    }

    // Fires for everyone thats in any channel
    private void VivoxVoiceManager_OnParticipantAddedEvent(string username, ChannelId channel, IParticipant participant)
    {
        // Here we only care about the positional channel for the next steps 
        if (channel.Name == m_vivoxNetworkManager.PositionalChannelName)
        {
            // We are in channel so setup tanks that are the local player and others to set the participant they are for vivox
            StartCoroutine(FindAndSetupTankParticipant(participant));
            
        }
    }

    private IEnumerator FindAndSetupTankParticipant(IParticipant participant)
    {
        yield return new WaitUntil(() => VivoxTankHelper.FindTankSetupInSceneByAccountId(participant.Account.Name) != null);
        var tankToSetParticipant = VivoxTankHelper.FindTankSetupInSceneByAccountId(participant.Account.Name);
        tankToSetParticipant.SetupParticipantPositionalVoice(participant);
    }


    // This takes in all participant changes for the Participant only
    public void Participant_PropertyChanged(IParticipant participant, System.ComponentModel.PropertyChangedEventArgs e)
    {
        m_ParticipantPropertyEvent.Invoke(participant, e);
        switch (e.PropertyName)
        {
            case "SpeechDetected":
                SpeechDetected(participant.SpeechDetected);
                break;
            default:
                break;
        }
    }

    private void SpeechDetected(bool isSpeaking)
    {
        if (!isLocalPlayer)
        {
            m_SpeakingIndicator.SetActive(isSpeaking);
        }
    }

    // We setup participant values for the child inGamePositionVoice so it updates the 3d position
    // Also setting the local participant so we can send updates to subscribers
    public void SetupParticipantPositionalVoice(IParticipant participant)
    {
        m_participant = participant;

        // We want to set the participant to get the positional player only for the localPlayer
        if (isLocalPlayer)
        {
            m_inGamePositionalVoice.Participant = participant;
            m_inGamePositionalVoice.m_ParticipantPropertyEvent.AddListener(Participant_PropertyChanged);
        }
    }

    private IEnumerator WaitForParticipantSetToSubscribe()
    {
        yield return new WaitUntil(() => m_participant != null && m_participant != null);
        m_RosterItem.GetComponent<RosterItem>().SetupRosterItem(m_participant);
    }

    [ClientCallback]
    public void Update()
    {
        if(!isLocalPlayer)
        {
            return;
        }

        if (gameObject.transform.position.y < -15)
        {
            var startPositions = FindObjectsOfType<NetworkStartPosition>();
            var index = UnityEngine.Random.Range(0, startPositions.Length);

            if (isLocalPlayer)
            {
                m_gameManager.IsGameInputEnabled = false;
            }

            m_TankMovement?.SetDefaults();
            gameObject.transform.position = startPositions[index].transform.position;
            gameObject.transform.rotation = startPositions[index].transform.rotation;
        }
    }

    public void SetLeader(bool leader)
    {
        RpcSetLeader(leader);
    }

    [ClientRpc]
    public void RpcSetLeader(bool leader)
    {
        //m_isLeader = leader;
    }

    [Command]
    public void CmdSetReady()
    {
        m_IsReady = true;
    }

    [Command]
    public void CmdSetScore(int scoreSet)
    {
        m_score += scoreSet;

        if (m_TeamID == TeamColor.Blue)
        {
            m_gameManager.BlueTeamScore += scoreSet;
        }
        else {
            m_gameManager.RedTeamScore += scoreSet;
        }
    }

    [Command]
    public void CmdSetPlayerName(string accountName, string displayName)
    {
        if (displayName.Length > defaultMaxStringLength)
        {
            m_PlayerDisplayName = displayName?.Substring(0, Math.Min((defaultMaxStringLength - 2), displayName.Length)) + "..";
        }
        else
        {
            m_PlayerDisplayName = displayName?.Substring(0, Math.Min(defaultMaxStringLength, displayName.Length));
        }
        m_AccountID = accountName;
    }

    private void OnTeamChanged(TeamColor newValue)
    {
        SetColor(newValue);
        if (isLocalPlayer)
        {
            TeamManager.Instance.SetLocalTeamID(newValue);
            TeamUI.Instance.InitPlayerList();
            if (newValue != TeamColor.None)
            {
                CmdSetReady();
            }
        }
    }

    private void DisplayNameText(string newName = null)
    {
        if (isLocalPlayer)
        {
            m_NameText.SetActive(false);
        }
        else
        {
            m_NameText.GetComponent<Text>().text = newName?? m_PlayerDisplayName;
        }
    }

    private void OnNameChanged(string newName)
    {
        DisplayNameText(newName);
    }

    private void SetColor(TeamColor newColor)
    {
        if (newColor == TeamColor.None)
        {
            Debug.LogError("Cannot set the player's team color to TeamColor.None");
            return;
        }
        Debug.Log($"SetColor will set the player's team color to {newColor.ToString()}");

        m_Color = newColor == TeamColor.Blue ? Color.blue : Color.red;
        GameObject m_TankRenderers = transform.Find("TankRenderers").gameObject;
        Renderer[] renderers = m_TankRenderers.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = m_Color;
        }

        if (m_TankRenderers)
            m_TankRenderers.SetActive(true);

        m_NameText.GetComponent<Text>().color = m_Color;
        m_SpeakingIndicator.GetComponent<Image>().color = m_Color;
    }

    public void ActivateCrown(bool active)
    {
        //if we try to show (not hide) the crown, we only show it we are the current leader
        //m_Crown.SetActive(active ? m_isLeader : false);
        //m_NameText.gameObject.SetActive(active);
    }

    public override void OnNetworkDestroy()
    {
        //GameManager.s_Instance.RemoveTank(gameObject);
        base.OnNetworkDestroy();
    }
}
