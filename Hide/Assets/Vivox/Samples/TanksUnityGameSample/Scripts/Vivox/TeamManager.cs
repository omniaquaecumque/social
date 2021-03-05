using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VivoxUnity;

public class PlayerSyncList : SyncList<TankSetup>{}

public class TeamManager : NetworkBehaviour
{
    public static TeamManager Instance;

    public static Action<TankSetup> OnPlayerAdded;
    public static Action<TankSetup> OnPlayerRemoved;

    public PlayerSyncList Players { get; private set; } = new PlayerSyncList();
    public List<TankSetup> BlueTeam => Players.Where(p => p.m_TeamID == TeamColor.Blue).ToList();
    public List<TankSetup> RedTeam => Players.Where(p => p.m_TeamID == TeamColor.Red).ToList();
    public TeamColor LocalTeamID { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        Players.Callback += OnPlayerListUpdated;
    }

    private void OnDestroy()
    {
        Players.Callback -= OnPlayerListUpdated;
    }

    /// <summary>
    /// This is fired on all clients when the "Players" SyncList is updated on the server.
    /// </summary>
    private void OnPlayerListUpdated(SyncList<TankSetup>.Operation op, int itemIndex, TankSetup item)
    {
        switch (op)
        {
            case global::Mirror.SyncList<global::TankSetup>.Operation.OP_ADD:
                {
                    OnPlayerAdded?.Invoke(item);
                }
                break;
            case global::Mirror.SyncList<global::TankSetup>.Operation.OP_CLEAR:
                break;
            case global::Mirror.SyncList<global::TankSetup>.Operation.OP_INSERT:
                break;
            case global::Mirror.SyncList<global::TankSetup>.Operation.OP_REMOVE:
                {
                    OnPlayerRemoved?.Invoke(item);
                }
                    break;
            case global::Mirror.SyncList<global::TankSetup>.Operation.OP_REMOVEAT:
                break;
            case global::Mirror.SyncList<global::TankSetup>.Operation.OP_SET:
                break;
            case global::Mirror.SyncList<global::TankSetup>.Operation.OP_DIRTY:
                break;
            default:
                break;
        }
    }

    [Server]
    public void AssignTeam(TankSetup player)
    {
        TeamColor team;
        if (BlueTeam.Count() == RedTeam.Count())
        {
            team = (TeamColor)UnityEngine.Random.Range(1, Enum.GetValues(typeof(TeamColor)).Length);
        }
        else
        {
            team = BlueTeam.Count > RedTeam.Count ? TeamColor.Red : TeamColor.Blue;
        }
        player.m_TeamID = team;
    }

    public void SetLocalTeamID(TeamColor team)
    {
        if (team != TeamColor.None)
        {
            LocalTeamID = team;
        }
    }
}
