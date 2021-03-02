using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

// Custom Network Manager Callbacks example
public class CustomNetworkManager : NetworkManager
{
    public GameObject chatUI;
    public Text chatText;
    public GameObject cameraLocation;

    public override void OnStartServer()
    {
        chatUI.SetActive(true);
        chatText.text += "Server Started!\n";
    }

    public override void OnStopServer()
    {
        chatUI.SetActive(false);
        // transform main camera back to camera location
        Transform cameraTransform = Camera.main.gameObject.transform;
        cameraTransform.parent = cameraLocation.transform;
        cameraTransform.position = cameraLocation.transform.position;
        cameraTransform.rotation = cameraLocation.transform.rotation;
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        chatText.text += conn;
        chatText.text += " Connected to Server\n";
        // Spwan player
        if (autoCreatePlayer)
        {
            ClientScene.AddPlayer(conn);
        }
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        chatText.text += conn;
        chatText.text += " Disconnected from Server\n";
    }
}
