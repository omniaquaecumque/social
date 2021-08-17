using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

// Manages all the text and button inputs
// Also acts like the main manager script for the game.
public class UIInputManager : MonoBehaviour
{
    public static string CachePath;

    public Button signupButton;
    public Button loginButton;
    public Button startButton;
    public Button logoutButton;
    public Button forgetButton;
    public InputField emailFieldLogin;
    public InputField passwordFieldLogin;
    public InputField usernameField;
    public InputField emailField;
    public InputField passwordField;

    private AuthenticationManager _authenticationManager;
    private GameObject _unauthInterface;
    private GameObject _authInterface;
    private LambdaManager _lambdaManager;
    private GameObject _loading;
    private GameObject _welcome;
    private GameObject _confirmEmail;
    private GameObject _signupContainer;
    private GameObject _error;

    private List<Selectable> _fields;
    private int _selectedFieldIndex = -1;

    private int _errorCount = 0;
    private string _username;
    private string _errorMsg;
    private bool _refresh = false;
    private bool _lock = false;

    private void displayComponentsFromAuthStatus(bool authStatus)
    {
        if (authStatus)
        {
            // Debug.Log("User authenticated, show welcome screen with options");
            _loading.SetActive(false);
            _unauthInterface.SetActive(false);
            _authInterface.SetActive(true);
            _welcome.SetActive(true);

            UnityMainThreadDispatcher.Instance().Enqueue(() => _welcome.GetComponent<TMPro.TextMeshProUGUI>().text = "Welcome, " + _username);
        }
        else
        {
            // Debug.Log("User not authenticated, activate/stay on login scene");
            _loading.SetActive(false);
            _unauthInterface.SetActive(true);
            _authInterface.SetActive(false);

            UnityMainThreadDispatcher.Instance().Enqueue(() => _error.GetComponent<TMPro.TextMeshProUGUI>().text = _errorMsg);
            if (_refresh && !_lock) _ = StartCoroutine(nameof(ErrorTextFade));
        }

        // clear out passwords
        passwordFieldLogin.text = "";
        passwordField.text = "";

        // set focus to email field on login form
        _selectedFieldIndex = -1;
    }

    private async void onLoginClicked()
    {
        _unauthInterface.SetActive(false);
        _loading.SetActive(true);
        // Debug.Log("onLoginClicked: " + emailFieldLogin.text + ", " + passwordFieldLogin.text);
        bool successfulLogin = await _authenticationManager.Login(emailFieldLogin.text, passwordFieldLogin.text);

        if (successfulLogin)
        {
            string accesstoken = _authenticationManager.GetAccessToken();
            _username = await _authenticationManager.GetUserNameFromProvider(accesstoken);
        }
        else 
            _errorCount++;

        //Debug.Log(_errorCount);
        if (_errorCount >= 3) forgetButton.gameObject.SetActive(true);

        if (emailFieldLogin.text == "" || passwordFieldLogin.text == "")
            _errorMsg = "Please enter username or password.";
        else
            _errorMsg = _authenticationManager.GetErrorMsg();

        displayComponentsFromAuthStatus(successfulLogin);
    }

   private async void onSignupClicked()
   {
      _unauthInterface.SetActive(false);
      _loading.SetActive(true);

      // Debug.Log("onSignupClicked: " + usernameField.text + ", " + emailField.text + ", " + passwordField.text);
      bool successfulSignup = await _authenticationManager.Signup(usernameField.text, emailField.text, passwordField.text);

      if (successfulSignup)
      {
         // here we re-enable the whole auth container but hide the sign up panel
         _signupContainer.SetActive(false);

         _confirmEmail.SetActive(true);

         // copy over the new credentials to make the process smoother
         emailFieldLogin.text = emailField.text;
         passwordFieldLogin.text = passwordField.text;

         // set focus to email field on login form
         _selectedFieldIndex = 0;
      }
      else
      {
         _confirmEmail.SetActive(false);

         _selectedFieldIndex = 3;

        if (usernameField.text == "" || emailField.text == "" || passwordField.text == "")
            _errorMsg = "Please enter all required information.";
        else
            _errorMsg = _authenticationManager.GetErrorMsg();
        }

      _loading.SetActive(false);
      _unauthInterface.SetActive(true);
      displayComponentsFromAuthStatus(false);
    }

   private void onLogoutClick()
   {
      _authenticationManager.SignOut();
      _errorMsg = "Successfully signed out.";
      displayComponentsFromAuthStatus(false);
   }

   private void onStartClick()
   {
      SceneManager.LoadScene("selection");
      Debug.Log("Changed to GameScene");

      // call to lambda to demonstrate use of credentials
      //_lambdaManager.ExecuteLambda();
   }

    // Need to change the website later
    private void onForgetClick() 
    {
        Application.OpenURL("http://google.com");
    }

    private async void RefreshToken()
    {
        bool successfulRefresh = await _authenticationManager.RefreshSession();

        if (successfulRefresh)
        {
            string accesstoken = _authenticationManager.GetAccessToken();
            _username = await _authenticationManager.GetUserNameFromProvider(accesstoken);
        }

        displayComponentsFromAuthStatus(successfulRefresh);
        _refresh = true;
    }

   void Start()
   {
        Debug.Log("UIInputManager: Start");

        var fullPath = Path.Combine(CachePath, new UserSessionCache().FileNameToUseForData());
        if (!File.Exists(fullPath))
        {
            File.Create(fullPath).Dispose();
        }

        // check if user is already authenticated 
        // We perform the refresh here to keep our user's session alive so they don't have to keep logging in.
        RefreshToken();

        signupButton.onClick.AddListener(onSignupClicked);
        loginButton.onClick.AddListener(onLoginClicked);
        startButton.onClick.AddListener(onStartClick);
        logoutButton.onClick.AddListener(onLogoutClick);
        forgetButton.onClick.AddListener(onForgetClick);
   }

   void Update()
   {
      HandleInputTabbing();
   }

   // Handles tabbing between inputs and buttons
   private void HandleInputTabbing()
   {
      if (Input.GetKeyDown(KeyCode.Tab))
      {
         CheckForAndSetManuallyChangedIndex();

         // update index to where we need to tab to
         _selectedFieldIndex++;

         if (_selectedFieldIndex >= _fields.Count)
         {
            // reset back to first input
            _selectedFieldIndex = 0;
         }
         _fields[_selectedFieldIndex].Select();
      }
   }

   // If the user selects an input via mouse click, then the _selectedFieldIndex 
   // may not be accurate as the focused field wasn't change by tabbing. Here we
   // correct the _selectedFieldIndex in case they wish to start tabing from that point on.
   private void CheckForAndSetManuallyChangedIndex()
   {
      for (var i = 0; i < _fields.Count; i++)
      {
         if (_fields[i] is InputField && ((InputField)_fields[i]).isFocused && _selectedFieldIndex != i)
         {
            // Debug.Log("_selectedFieldIndex is : " + _selectedFieldIndex + ", Reset _selectedFieldIndex to: " + i);
            _selectedFieldIndex = i;
            break;
         }
      }
   }

    IEnumerator ErrorTextFade()
    {
        _lock = true;

        Color c = _error.GetComponent<TMPro.TextMeshProUGUI>().color;
        c.a = 1f;
        _error.GetComponent<TMPro.TextMeshProUGUI>().color = c;

        yield return new WaitForSeconds(5f);

        for (float ft = 1f; ft >= 0; ft -= 0.1f)
        {
            c = _error.GetComponent<TMPro.TextMeshProUGUI>().color;
            c.a = ft;
            _error.GetComponent<TMPro.TextMeshProUGUI>().color = c;
            yield return new WaitForSeconds(.05f);
        }

        c = _error.GetComponent<TMPro.TextMeshProUGUI>().color;
        c.a = 0f;
        _error.GetComponent<TMPro.TextMeshProUGUI>().color = c;

        _lock = false;
    }

    void Awake()
    {
        CachePath = Application.persistentDataPath;

        _unauthInterface = GameObject.Find("UnauthInterface");
        _authInterface = GameObject.Find("AuthInterface");
        _loading = GameObject.Find("Loading");
        _welcome = GameObject.Find("Welcome");
        _confirmEmail = GameObject.Find("ConfirmEmail");
        _signupContainer = GameObject.Find("SignupContainer");
        _error = GameObject.Find("ERROR");

        forgetButton.gameObject.SetActive(false);
        _unauthInterface.SetActive(false); // start as false so we don't just show the login screen during attempted token refresh
        _authInterface.SetActive(false);
        _welcome.SetActive(false);
        _confirmEmail.SetActive(false);
        _signupContainer.SetActive(true);
        _error.SetActive(true);

        _authenticationManager = FindObjectOfType<AuthenticationManager>();
        _lambdaManager = FindObjectOfType<LambdaManager>();

        _fields = new List<Selectable> { emailFieldLogin, passwordFieldLogin, loginButton, emailField, usernameField, passwordField, signupButton };
    }
}
