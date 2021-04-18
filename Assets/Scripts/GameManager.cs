using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class GameManager : MonoBehaviour
{
    private static bool gameStarted = false;
    public static bool gameEnded = false; 
    public float restartDelay = 1f;

    public Camera sceneCam; 
    public GameObject completeDefender; 
    public GameObject completeSabotager; 

    private static int numPlayers = 0;
    public static int fallen = 0;
    public static List<GameObject> players = new List<GameObject>();

    public void CompleteDefenderLevel() 
    {
        completeDefender.SetActive(true);
    } 

    public void CompleteSabotagerLevel() 
    {
        completeSabotager.SetActive(true);
    }     

    public static void EndGame() 
    {
        if (gameEnded == false) 
        {
            gameEnded = true; 
            //Invoke("Restart", restartDelay);
        }
    }

    public void Restart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void RegisterPlayer(GameObject p)
    {
        if (!gameStarted) 
            gameStarted = true;
        players.Add(p);
    }

    public void SetCam() 
    {
        sceneCam.enabled = true;
    }

    public static void IncrementPlayer() {
        numPlayers += 1;
    }

    public static void DecrementPlayer() {
        numPlayers -= 1;
    }    

    public void Plus() {
        fallen += 1;
    }

    void Update() 
    {   
        int num = GameObject.FindGameObjectsWithTag("Player").Length;
        //Debug.Log(numPlayers);
        Debug.Log(num);
        if (num == 0 && gameStarted) {
            CompleteSabotagerLevel();
        }
    }
}
