using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class NetworkManagerHide : NetworkManager
{
    [SerializeField] private int minPlayers = 2;
    [Scene] [SerializeField] private string menuScene = string.Empty;

    [Header("Room")]
    [SerializeField] private LobbyRoom roomPlayerPrefab = null;

    [Header("Game")]
    [SerializeField] private GamePlayer findPlayerPrefab = null;
    [SerializeField] private GamePlayer guidePlayerPrefab = null;

    public static event Action OnClientConnected;
    public static event Action OnClientDisconnected;
    public static event Action<NetworkConnection> OnServerReadied;

    public List<LobbyRoom> RoomPlayers { get; } = new List<LobbyRoom>();
    public List<GamePlayer> GamePlayers { get; } = new List<GamePlayer>();
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        OnClientConnected?.Invoke();
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);

        OnClientDisconnected?.Invoke();
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        if (numPlayers >= maxConnections)
        {
            conn.Disconnect();
            return;
        }

        if (SceneManager.GetActiveScene().path != menuScene)
        {
            conn.Disconnect();
            return;
        }
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        if (SceneManager.GetActiveScene().path == menuScene)
        {
            bool isLeader = RoomPlayers.Count == 0;

            LobbyRoom roomPlayerInstance = Instantiate(roomPlayerPrefab);

            roomPlayerInstance.IsLeader = isLeader;

            NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
        }
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        if (conn.identity != null)
        {
            var player = conn.identity.GetComponent<LobbyRoom>();

            RoomPlayers.Remove(player);

            NotifyPlayersOfReadyState();
        }

        base.OnServerDisconnect(conn);
    }

    public override void OnStopServer()
    {
        //OnServerStopped?.Invoke();

        RoomPlayers.Clear();
        GamePlayers.Clear();
    }

    public void NotifyPlayersOfReadyState()
    {
        foreach (var player in RoomPlayers)
        {
            player.HandleReadyToStart(IsReadyToStart());
        }
    }

    private bool IsReadyToStart()
    {
        if (numPlayers < minPlayers) { return false; }

        foreach (var player in RoomPlayers)
        {
            if (!player.IsReady) { return false; }
        }

        return true;
    }

    public void StartGame()
    {
        if (SceneManager.GetActiveScene().path == menuScene)
        {
            if (!IsReadyToStart()) { return; }

            ServerChangeScene("playMap");
        }
    }

    public override void ServerChangeScene(string newSceneName)
    {
        // From menu to game
        if (SceneManager.GetActiveScene().path == menuScene && newSceneName.StartsWith("playMap"))
        {
            for (int i = RoomPlayers.Count - 1; i >= 0; i--)
            {
                var conn = RoomPlayers[i].connectionToClient;
                GamePlayer gameplayerInstance;
                if (RoomPlayers[i].IsLeader)
                {
                    gameplayerInstance = Instantiate(guidePlayerPrefab);
                }
                else
                {
                    gameplayerInstance = Instantiate(findPlayerPrefab);
                }
                gameplayerInstance.SetDisplayName(RoomPlayers[i].DisplayName);

                NetworkServer.Destroy(conn.identity.gameObject);

                NetworkServer.ReplacePlayerForConnection(conn, gameplayerInstance.gameObject, true);
            }
        }

        base.ServerChangeScene(newSceneName);
    }

    /*
    public override void OnServerSceneChanged(string sceneName)
    {
        if (sceneName.StartsWith("playMap"))
        {
            Debug.Log("hi");
            GameObject hide = GameObject.Find("Hidden");
            hide?.GetComponent<AssignHidden>().Assign();
        }
    }

    public void ChangeTag(GameObject hide)
    {
        for(int i= GamePlayers.Count-1; i>0; i--)
        {
            GamePlayers[i].ChangeTag(hide);
        }
    }*/

    public override void OnServerReady(NetworkConnection conn)
    {
        base.OnServerReady(conn);

        OnServerReadied?.Invoke(conn);
    }

    public void winGame()
    {
        for(int i = GamePlayers.Count-1; i>=0; i--)
        {
            GamePlayers[i].ShowWinMsg();
        }
    }
}
