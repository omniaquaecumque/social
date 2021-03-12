using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class hiddenBeh : NetworkBehaviour
{
    public GameObject endGame;
    CanvasGroup canvasGroup;
    private void Start()
    {
        canvasGroup = endGame.GetComponentInChildren<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    public void OnCollisionEnter(Collision collision)
    {
        // collide to hidden object
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject, 0.1f);
            canvasGroup.alpha = 1;
        }
    }

}
