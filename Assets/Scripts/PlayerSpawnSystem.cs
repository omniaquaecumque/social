using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror; 
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;

public class PlayerSpawnSystem : NetworkBehaviour
{
    [SerializeField] private GameObject playerDefenderPrefab = null;
    [SerializeField] private GameObject playerSabotagerPrefab = null;

    private static List<Transform> spawnPoints = new List<Transform>();

    private int spawned = 0;
    private int nextIndex = 0;

    public static void AddSpawnPoint(Transform transform)
    {
        spawnPoints.Add(transform);
        spawnPoints = spawnPoints.OrderBy(x => x.GetSiblingIndex()).ToList();
    }

    public static void RemoveSpawnPoint(Transform transform) => spawnPoints.Remove(transform);

    public override void OnStartServer() => NetworkManagerLobby.OnServerReadied += SpawnPlayer;

    [ServerCallback]
    private void OnDestroy() => NetworkManagerLobby.OnServerReadied -= SpawnPlayer;

    [Server]
    public void SpawnPlayer(NetworkConnection conn)
    {
        Transform spawnPoint = spawnPoints.ElementAtOrDefault(nextIndex);

        if (spawnPoint == null)
        {
            Debug.LogError($"Missing spawn point for player {nextIndex}");
            return;
        }

        GameObject playerInstance = null;
        if (spawned % 2 == 0) {
            playerInstance = Instantiate(playerDefenderPrefab, spawnPoints[nextIndex].position, spawnPoints[nextIndex].rotation);
        } else {
            playerInstance = Instantiate(playerSabotagerPrefab, spawnPoints[nextIndex].position, spawnPoints[nextIndex].rotation);
        }
        
        GameManager.IncrementPlayer();
        GameManager.RegisterPlayer(playerInstance);
        NetworkServer.Spawn(playerInstance, conn);
        
        nextIndex++;
        spawned++;
    }

}
