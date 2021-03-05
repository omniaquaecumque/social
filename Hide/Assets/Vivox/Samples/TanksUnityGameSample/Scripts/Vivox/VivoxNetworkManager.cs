using System.Collections;
using UnityEngine;
using Mirror;
using System.Linq;
using VivoxUnity;
using System.Net.Sockets;
using System.Net;

public class VivoxNetworkManager : NetworkManager
{
    public enum MatchStatus
    {
        Open,
        Closed,
        Seeking
    }

    private VivoxVoiceManager m_vivoxVoiceManager;
    public string PositionalChannelName { get; private set; }
    public string LobbyChannelName = "lobbyChannel";
    public string clientLocalIpAddress
    {
        get
        {
            string localIpAddress = null;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530); //Google public DNS and port
                IPEndPoint localEndPoint = socket.LocalEndPoint as IPEndPoint;
                localIpAddress = localEndPoint.Address.ToString();
            }
            if (string.IsNullOrWhiteSpace(localIpAddress))
            {
                Debug.LogError("Unable to find clientLocalIpAddress");
            }
            return localIpAddress;
        }
    }
    public string clientPublicIpAddress
    {
        get {
            string publicIpAddress = new WebClient().DownloadString("https://api.ipify.org");
            if (string.IsNullOrWhiteSpace(publicIpAddress))
            {
                Debug.LogError("Unable to find clientPublicIpAddress");
            }
            return publicIpAddress.Trim();
        }
    }

    public string HostingIp
    {
        get
        {
            if (NetworkServer.active || NetworkClient.serverIp == "localhost")
            {
                return clientLocalIpAddress;
            }
            // Your connected to a remote host. Let's get their ip
            return NetworkClient.serverIp ?? clientLocalIpAddress;
        }
    }

    public override void Awake()
    {
        base.Awake();
        m_vivoxVoiceManager = VivoxVoiceManager.Instance;
    }

    public void SendLobbyUpdate(VivoxNetworkManager.MatchStatus status)
    {
        var lobbyChannelSession = m_vivoxVoiceManager.ActiveChannels.FirstOrDefault(ac => ac.Channel.Name == LobbyChannelName);
        if (lobbyChannelSession != null)
        {
            // NB: the message in the first argument will never get printed and is just for readability in the logs
            m_vivoxVoiceManager.SendTextMessage($"<{m_vivoxVoiceManager.LoginSession.LoginSessionId.DisplayName}:{status}>", lobbyChannelSession.Key, $"MatchStatus:{status}", (status == VivoxNetworkManager.MatchStatus.Open ? HostingIp : "blank"));
        }
        else
        {
            Debug.LogError($"Cannot send MatusStatus.{status}: not joined to {LobbyChannelName}");
        }
    }

    public override void OnServerAddPlayer(NetworkConnection conn, AddPlayerMessage extraMessage)
    {
        base.OnServerAddPlayer(conn, extraMessage);

        StartCoroutine(AddNewPlayer(conn));
    }


    private IEnumerator AddNewPlayer(NetworkConnection conn)
    {
        var player = conn.playerController.gameObject.GetComponent<TankSetup>();

        TeamManager.Instance.AssignTeam(player);

        // Wait until the player object is ready before adding them to the team roster.
        yield return new WaitUntil(() => player.m_IsReady);
        TeamManager.Instance.Players.Add(player);
    }

    public void LeaveAllChannels(bool includeLobby = true)
    {
        foreach (var channelSession in m_vivoxVoiceManager.ActiveChannels)
        {
            if (channelSession.AudioState == ConnectionState.Connected || channelSession.TextState == ConnectionState.Connected
                && (includeLobby || (includeLobby == false && channelSession.Channel.Name != LobbyChannelName)))
            {
                channelSession.Disconnect();
            }
        }
    }

    private void VivoxVoiceManager_OnParticipantAddedEvent(string username, ChannelId channel, IParticipant participant)
    {
        if (channel.Name == PositionalChannelName && participant.IsSelf)
        {
            StartCoroutine(AwaitLobbyRejoin());
        }
        else if (channel.Name == LobbyChannelName && participant.IsSelf && NetworkServer.active)
        {
            // if joined the lobby channel and we're hosting a match
            // we should send an update since we could've missed a request when briefly out of channel
            SendLobbyUpdate(VivoxNetworkManager.MatchStatus.Open);
        }
    }

    private void VivoxVoiceManager_OnTextMessageLogReceivedEvent(string sender, IChannelTextMessage channelTextMessage)
    {
        if (channelTextMessage.ApplicationStanzaNamespace.EndsWith(VivoxNetworkManager.MatchStatus.Seeking.ToString()) && NetworkServer.active)
        {
            SendLobbyUpdate(VivoxNetworkManager.MatchStatus.Open);
        }
    }

    private IEnumerator AwaitLobbyRejoin()
    {
        IChannelSession lobbyChannel = m_vivoxVoiceManager.ActiveChannels.FirstOrDefault(ac => ac.Channel.Name == LobbyChannelName);
        // Lets wait until we have left the lobby channel before tyring to join it.
        yield return new WaitUntil(() => lobbyChannel == null
        || (lobbyChannel.AudioState != ConnectionState.Connected && lobbyChannel.TextState != ConnectionState.Connected));

        // Always send if hosting, since we could have missed a request.
        m_vivoxVoiceManager.JoinChannel(LobbyChannelName, ChannelType.NonPositional, VivoxVoiceManager.ChatCapability.TextOnly, false);
    }

    public override void OnStartClient()
    {
        Debug.Log("Starting client");

        PositionalChannelName = "Positional" + HostingIp;
        m_vivoxVoiceManager.OnParticipantAddedEvent += VivoxVoiceManager_OnParticipantAddedEvent;
        m_vivoxVoiceManager.OnTextMessageLogReceivedEvent += VivoxVoiceManager_OnTextMessageLogReceivedEvent;
        base.OnStartClient();
    }
    public override void OnStopClient()
    {
        Debug.Log("Stopping client");
        m_vivoxVoiceManager.OnParticipantAddedEvent -= VivoxVoiceManager_OnParticipantAddedEvent;
        m_vivoxVoiceManager.OnTextMessageLogReceivedEvent -= VivoxVoiceManager_OnTextMessageLogReceivedEvent;
        LeaveAllChannels(false);
        base.OnStopClient();
    }
}
