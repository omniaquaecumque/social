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
    //private int spawned = 0;
    //private int playerLayer = 9;

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
        /*if (spawned == 1) {
            playerLayer = 10;            
        }        
        
        playerInstance.layer = playerLayer;
        //playerInstance.transform.GetChild(0).gameObject.layer = 9;
        GameObject visual = playerInstance.transform.GetChild(0).gameObject;
        visual.layer = playerLayer;
        foreach (Transform child in visual.transform) {
            child.gameObject.layer = playerLayer;
        }
        GameObject wheels = visual.transform.GetChild(12).gameObject; 
        foreach (Transform w in wheels.transform) {
            w.gameObject.layer = playerLayer;
        }     
        GameObject wheelTransforms = wheels.transform.GetChild(0).gameObject;     
        foreach (Transform t in wheelTransforms.transform) {
            t.gameObject.layer = playerLayer;
        }       
        GameObject wheelColliders = wheels.transform.GetChild(1).gameObject; 
        foreach (Transform c in wheelColliders.transform) {
            c.gameObject.layer = playerLayer;
        }                         
        playerInstance.transform.GetChild(1).gameObject.layer = playerLayer;
        spawned += 1;
        */
    /*
        GameObject cameraLocation = playerInstance.transform.GetChild(3).gameObject;
        Transform cameraTransform = Camera.main.gameObject.transform;  
        cameraTransform.parent = cameraLocation.transform;  
        cameraTransform.position = cameraLocation.transform.position;  
        cameraTransform.rotation = cameraLocation.transform.rotation;*/
        
        NetworkServer.Spawn(playerInstance, conn);
        
        nextIndex++;
    }

}
