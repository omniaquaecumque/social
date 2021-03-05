using System;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoginScreenUI : MonoBehaviour
{
    private VivoxVoiceManager _vivoxVoiceManager;

    public Button LoginButton;
    public InputField DisplayNameInput;
    public GameObject LoginScreen;

    private int defaultMaxStringLength = 9;
    private bool PermissionsDenied;
    #region Unity Callbacks

    private EventSystem _evtSystem;

    private void Awake()
    {
        _evtSystem = FindObjectOfType<EventSystem>();
        _vivoxVoiceManager = VivoxVoiceManager.Instance;
        _vivoxVoiceManager.OnUserLoggedInEvent += OnUserLoggedIn;
        _vivoxVoiceManager.OnUserLoggedOutEvent += OnUserLoggedOut;

#if !(UNITY_STANDALONE || UNITY_IOS || UNITY_ANDROID || UNITY_STADIA)
        DisplayNameInput.interactable = false;
#else
        DisplayNameInput.onEndEdit.AddListener((string text) => { LoginToVivoxService(); });
#endif
        LoginButton.onClick.AddListener(() => { LoginToVivoxService(); });

        if (_vivoxVoiceManager.LoginState == VivoxUnity.LoginState.LoggedIn)
        {
            OnUserLoggedIn();
            DisplayNameInput.text = _vivoxVoiceManager.LoginSession.Key.DisplayName;
        }
        else
        {
            OnUserLoggedOut();
            var systInfoDeviceName = String.IsNullOrWhiteSpace(SystemInfo.deviceName) == false ? SystemInfo.deviceName : Environment.MachineName;

            DisplayNameInput.text = Environment.MachineName.Substring(0, Math.Min(defaultMaxStringLength, Environment.MachineName.Length));
        }
    }

    private void OnDestroy()
    {
        _vivoxVoiceManager.OnUserLoggedInEvent -= OnUserLoggedIn;
        _vivoxVoiceManager.OnUserLoggedOutEvent -= OnUserLoggedOut;

        LoginButton.onClick.RemoveAllListeners();
#if UNITY_STANDALONE || UNITY_IOS || UNITY_ANDROID || UNITY_STADIA
        DisplayNameInput.onEndEdit.RemoveAllListeners();
#endif
    }

    #endregion

    private void ShowLoginUI()
    {
        LoginScreen.SetActive(true);
        LoginButton.interactable = true;
        _evtSystem.SetSelectedGameObject(LoginButton.gameObject, null);

    }

    private void HideLoginUI()
    {
        LoginScreen.SetActive(false);
    }

    private void LoginToVivoxService()
    {
        if (Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            // The user authorized use of the microphone.
            LoginToVivox();
        }
        else
        {
            // Check if the users has already denied permissions
            if (PermissionsDenied)
            {
                PermissionsDenied = false;
                LoginToVivox();
            }
            else
            {
                PermissionsDenied = true;
                // We do not have permission to use the microphone.
                // Ask for permission or proceed without the functionality enabled.
                Permission.RequestUserPermission(Permission.Microphone);
            }
        }
    }

    private void LoginToVivox()
    {
        LoginButton.interactable = false;

        if (string.IsNullOrEmpty(DisplayNameInput.text))
        {
            Debug.LogError("Please enter a display name.");
            return;
        }
        _vivoxVoiceManager.Login(DisplayNameInput.text);
    }

    #region Vivox Callbacks

    private void OnUserLoggedIn()
    {
        HideLoginUI();
    }

    private void OnUserLoggedOut()
    {
        ShowLoginUI();
    }

    #endregion
}
