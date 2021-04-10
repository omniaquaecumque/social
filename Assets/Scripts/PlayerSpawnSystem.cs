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
    private int spawned = 0;
    [SerializeField] private GameObject playerPrefab = null;

    private static List<Transform> spawnPoints = new List<Transform>();

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

        GameObject playerInstance = Instantiate(playerPrefab, spawnPoints[nextIndex].position, spawnPoints[nextIndex].rotation);
        if (spawned == 0) {
            playerInstance.transform.GetChild(1).gameObject.layer = 9;
            spawned += 1;
        } 
        else if (spawned == 1) {
            playerInstance.transform.GetChild(1).gameObject.layer = 10;
            spawned += 1;            
        }
        NetworkServer.Spawn(playerInstance, conn);

        nextIndex++;
    }
}
