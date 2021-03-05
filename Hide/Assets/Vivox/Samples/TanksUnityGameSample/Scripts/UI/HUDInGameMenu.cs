using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HUDInGameMenu : MonoBehaviour
{
    public GameObject MenuButton;
    public GameObject MissleButton;
    public GameObject Joystick;
    public GameObject BoostButton;
    public GameObject InGameMenuPanel;
    public Button ResumeButton;
    public Button SwitchTeamButton;
    public Button QuitButton;
    public GameObject ConfirmationPanel;
    public Button ConfirmYesButton;
    public Button ConfirmNoButton;

    private bool _inputPressed;
    private GameManager _gameManager;
    EventSystem m_EventSystem;
    private VivoxNetworkManager _vivoxNetworkManager;
 
    void Awake()
    {
        //Hide menus on awake
        InGameMenuPanel.SetActive(false);
        ConfirmationPanel.SetActive(false);
        _gameManager = FindObjectOfType<GameManager>();
        _vivoxNetworkManager = FindObjectOfType<VivoxNetworkManager>();
        if (!_vivoxNetworkManager)
        {
            Debug.LogError("Unable to find VivoxNetworkManager object.");
        }
        if (!_gameManager)
        {
            Debug.LogError("Unable to find GameManager object.");
        }
        _gameManager.BoostButton = BoostButton.GetComponent<Button>();
        _gameManager.ShootButton = MissleButton.GetComponent<Button>();


#if UNITY_IOS || UNITY_ANDROID
        MenuButton.SetActive(true);
        Joystick.SetActive(true);
        MissleButton.SetActive(true);
        BoostButton.SetActive(true);

#else
        MenuButton.SetActive(false);
        Joystick.SetActive(false);
        MissleButton.SetActive(false);
        BoostButton.SetActive(false);
#endif
    }

    void Start()
    {
        //Fetch the current EventSystem. Make sure your Scene has one.
        m_EventSystem = EventSystem.current;

        // Bind all the buttons
        MenuButton.gameObject.GetComponent<Button>().onClick.AddListener(MenuInputAction);
        ResumeButton.onClick.AddListener(ResumeButtonAction);
        SwitchTeamButton.onClick.AddListener(SwitchTeamButtonAction);
        QuitButton.onClick.AddListener(QuitButtonAction);
        ConfirmYesButton.onClick.AddListener(ConfirmYesButtonAction);
        ConfirmNoButton.onClick.AddListener(ConfirmNoAction);
    }

    void OnDestroy()
    {
        MenuButton.gameObject.GetComponent<Button>().onClick.RemoveListener(MenuInputAction);
        ResumeButton.onClick.RemoveListener(ResumeButtonAction);
        SwitchTeamButton.onClick.RemoveListener(SwitchTeamButtonAction);
        QuitButton.onClick.RemoveListener(QuitButtonAction);
        ConfirmYesButton.onClick.RemoveListener(ConfirmYesButtonAction);
        ConfirmNoButton.onClick.RemoveListener(ConfirmNoAction);
    }


    // Detects if the keyboard key or console button was pressed
    void Update()
    {
        // While on standalone or editor the p or escape key will open and close the menu
#if UNITY_STANDALONE || UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape) )
        {
            MenuInputAction();
        }
#endif
        // While on consoles the start button will open and close the menu
#if UNITY_XBOXONE || UNITY_PS4 || UNITY_SWITCH
        if (Input.GetButtonDown("Options"))
        {
            MenuInputAction();
        }
#endif
    }


    // Toggles InGameMenu to be in active or deactivated state based on inverse state or override that with forceIsActive 
    private void ToggleInGameMenu(bool? forceIsActive = null)
    {
        bool isSettingTo = forceIsActive ?? !InGameMenuPanel.activeInHierarchy && !ConfirmationPanel.activeInHierarchy;

        // If forceIsActive is set we will set it to forceIsActive otherwise invert the current active state of InGameMenuPanel  
        InGameMenuPanel.SetActive(isSettingTo);

        // If menu is active then set focus on ResumeButton otherwise set focus on MenuButton
        m_EventSystem.SetSelectedGameObject(isSettingTo ? ResumeButton.gameObject : MenuButton.gameObject);

        // Set the game manager to not take input in the game if a menu is active and vice versa
        _gameManager.IsGameInputEnabled = !(InGameMenuPanel.activeInHierarchy || ConfirmationPanel.activeInHierarchy);
    }

    // Toggles ConfirmMenu to be in active or deactivated state based on inverse state or override that with forceIsActive 
    private void ToggleConfirmMenu(bool? forceIsActive = null)
    {
        bool isSettingTo = forceIsActive ?? !InGameMenuPanel.activeInHierarchy && !ConfirmationPanel.activeInHierarchy;

        // If forceIsActive is set we will set it to forceIsActive otherwise invert the current active state of InGameMenuPanel  
        ConfirmationPanel.SetActive(isSettingTo);

        // If menu is active then set focus on ConfirmNoButton otherwise set focus on ResumeButton
        m_EventSystem.SetSelectedGameObject(isSettingTo ? ConfirmNoButton.gameObject : ResumeButton.gameObject);

        // Set the game manager to not take input in the game if a menu is active and vice versa
        _gameManager.IsGameInputEnabled = !(InGameMenuPanel.activeInHierarchy || ConfirmationPanel.activeInHierarchy);
    }

    // When Menu Input has fired
    private void MenuInputAction()
    {
        ToggleInGameMenu();
    }

    // Resume button on the InGameMenu
    private void ResumeButtonAction()
    {
        ToggleInGameMenu(false);
        ToggleConfirmMenu(false);
        // Remove focus on ui elements 
        m_EventSystem.SetSelectedGameObject(null);
    }

    // Switch Teams button on the InGameMenu
    private void SwitchTeamButtonAction()
    {
        throw new NotImplementedException();
    }

    // Quit button on the InGameMenu
    private void QuitButtonAction()
    {
        ToggleInGameMenu(false);
        ToggleConfirmMenu(true);
    }

    private void ConfirmYesButtonAction()
    {
        if(_vivoxNetworkManager.networkAddress == "localhost")
        {
            _vivoxNetworkManager.SendLobbyUpdate(VivoxNetworkManager.MatchStatus.Closed);
            _vivoxNetworkManager.StopHost();
        }
        else
        {
            _vivoxNetworkManager.StopClient();
        }
    }

    private void ConfirmNoAction()
    {
        ToggleInGameMenu(true);
        ToggleConfirmMenu(false);
    }
}
