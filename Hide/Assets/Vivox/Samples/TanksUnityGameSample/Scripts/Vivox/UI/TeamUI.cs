using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TeamUI : MonoBehaviour
{
    public GameObject playerFab;
    public static TeamUI Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        TeamManager.OnPlayerAdded -= OnPlayerListUpdated;
        TeamManager.OnPlayerRemoved -= OnPlayerListUpdated;
    }

    public void InitPlayerList()
    {
        TeamManager.OnPlayerAdded -= OnPlayerListUpdated;
        TeamManager.OnPlayerRemoved -= OnPlayerListUpdated;
        TeamManager.OnPlayerAdded += OnPlayerListUpdated;
        TeamManager.OnPlayerRemoved += OnPlayerListUpdated;
    }

    private void OnPlayerListUpdated(TankSetup joiningPlayer)
    {
        if (TeamManager.Instance.LocalTeamID == joiningPlayer.m_TeamID)
        {
            ClearRoster();
            var teammates = TeamManager.Instance.LocalTeamID == TeamColor.Blue ? TeamManager.Instance.BlueTeam : TeamManager.Instance.RedTeam;
            if (teammates.Count() > 0)
            {
                foreach (var teammate in teammates)
                {
                    InstantiateTeammateUIElement(teammate);
                }
            }
        }
        // Don't do anything if the player joining isn't on the same team as the local player.
    }

    private void ClearRoster(TankSetup player = null)
    {
        var uiElements = transform.GetComponentsInChildren<RectTransform>();
        if (uiElements.Length > 0)
        {
            foreach (var element in uiElements)
            {
                if (element.gameObject != this.gameObject)
                {
                    Destroy(element.gameObject);
                }
            }
        }
    }

    private void InstantiateTeammateUIElement(TankSetup player)
    {
        GameObject newRosterObject = Instantiate(playerFab, this.gameObject.transform);
        StartCoroutine(WaitForTankSetupInSceneByAccountId(player.m_AccountID, newRosterObject));
    }

    private IEnumerator WaitForTankSetupInSceneByAccountId(string accountID, GameObject newRosterObject)
    {
        TankSetup tankSetup = null;
        while (tankSetup == null)
        {
            yield return new WaitForSeconds((float)0.1);
            tankSetup = VivoxTankHelper.FindTankSetupInSceneByAccountId(accountID);
        }

        tankSetup.m_RosterItem = newRosterObject;
        newRosterObject.GetComponent<RectTransform>().sizeDelta = new Vector2(250, 100);
        var rosterItemText = newRosterObject.GetComponentInChildren<Text>();
        rosterItemText.text = tankSetup.m_PlayerDisplayName;
        rosterItemText.color = Color.white;
        rosterItemText.fontSize = 32;

        newRosterObject.GetComponent<RosterItem>().ChatStateImage.GetComponent<Image>().color = Color.white;
        var backgroundColor = newRosterObject.GetComponent<Image>();
        var bgColor = tankSetup.m_TeamID == TeamColor.Blue ? Color.blue : Color.red;
        bgColor.a = 0.25f;
        backgroundColor.color = bgColor;
    }
}
