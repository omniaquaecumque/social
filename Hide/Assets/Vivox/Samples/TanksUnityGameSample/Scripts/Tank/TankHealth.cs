using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.Collections;

public class TankHealth : NetworkBehaviour
{
    public float m_StartingHealth = 100f;             // The amount of health each tank starts with.
    public Slider m_Slider;                           // The slider to represent how much health the tank currently has.
    public Image m_FillImage;                         // The image component of the slider.
    public Color m_FullHealthColor = Color.green;     // The color the health bar will be when on full health.
    public Color m_ZeroHealthColor = Color.red;       // The color the health bar will be when on no health.
    public AudioClip m_TankExplosion;                 // The clip to play when the tank explodes.
    public ParticleSystem m_ExplosionParticles;       // The particle system the will play when the tank is destroyed.
    public GameObject m_TankRenderers;                // References to all the gameobjects that need to be disabled when the tank is dead.
    public GameObject m_HealthCanvas;
    public GameObject m_AimCanvas;
    public GameObject m_LeftDustTrail;
    public GameObject m_RightDustTrail;
    private TankSetup m_TankSetup;
    private GameManager m_gameManager;
    private TankMovement m_TankMovement;


    [SyncVar(hook = "OnCurrentHealthChanged")]
    private float m_CurrentHealth;                  // How much health the tank currently has.*
    [SyncVar]
    private bool m_ZeroHealthHappened;              // Has the tank been reduced beyond zero health yet?
    private BoxCollider m_Collider;                 // Used so that the tank doesn't collide with anything when it's dead.

    private void Awake()
    {
        m_Collider = GetComponent<BoxCollider>();
        m_gameManager = FindObjectOfType<GameManager>();
        m_TankMovement = GetComponent<TankMovement>();
        m_TankSetup = GetComponent<TankSetup>();
        OnCurrentHealthChanged(m_StartingHealth);
    }


    // This is called whenever the tank takes damage.
    public void Damage(float amount, TankSetup m_playerOriginTankSetup)
    {
        // Reduce current health by the amount of damage done.
        m_CurrentHealth -= amount;

        // If the current health is at or below zero and it has not yet been registered, call OnZeroHealth.
        if (m_CurrentHealth <= 0f && !m_ZeroHealthHappened)
        {
            OnZeroHealth();
            if (m_TankSetup.m_AccountID == m_playerOriginTankSetup.m_AccountID)
            {
                m_TankSetup.CmdSetScore(-1);
                Debug.Log("You killed yourself!");
            }
            else
            {
                m_playerOriginTankSetup.CmdSetScore(1);
                Debug.Log($"{m_playerOriginTankSetup.m_PlayerDisplayName} killed {m_TankSetup.m_PlayerDisplayName}!");
            }
        }
        else {
            if (m_TankSetup.m_AccountID == m_playerOriginTankSetup.m_AccountID)
            {
                Debug.Log("You have taken damaged from: yourself!");
            }
            else
            {
                Debug.Log($"You have taken damaged from: {m_playerOriginTankSetup.m_PlayerDisplayName}!");
            }
        }
    }


    private void SetHealthUI()
    {
        // Set the slider's value appropriately.
        m_Slider.value = m_CurrentHealth;

        // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
    }


    void OnCurrentHealthChanged(float value)
    {
        m_CurrentHealth = value;
        // Change the UI elements appropriately.
        SetHealthUI();

    }

    private void OnZeroHealth()
    {
        // Set the flag so that this function is only called once.
        m_ZeroHealthHappened = true;

        RpcOnZeroHealth();
    }

    private void InternalOnZeroHealth()
    {
        // Disable the collider and all the appropriate child gameobjects so the tank doesn't interact or show up when it's dead.
        SetTankActive(false);
    }

    [ClientRpc]
    private void RpcOnZeroHealth()
    {
        // Play the particle system of the tank exploding.
        m_ExplosionParticles.Play();

        // Create a gameobject that will play the tank explosion sound effect and then destroy itself.
        AudioSource.PlayClipAtPoint(m_TankExplosion, transform.position);

        InternalOnZeroHealth();

        StartCoroutine(ResetPlayer());
    }

    public IEnumerator ResetPlayer()
    {
        var startPositions = FindObjectsOfType<NetworkStartPosition>();
        var index = Random.Range(0, startPositions.Length);

        if (isLocalPlayer)
        {
            m_gameManager.IsGameInputEnabled = false;
        }

        yield return new WaitForSeconds(1);
        m_TankMovement?.SetDefaults();
        gameObject.transform.position = startPositions[index].transform.position;
        gameObject.transform.rotation = startPositions[index].transform.rotation;
        SetTankActive(true);

        yield return new WaitForSeconds((float)0.3);

        SetDefaults();
        if (isLocalPlayer)
        {
            m_gameManager.IsGameInputEnabled = true;
        }
    }

    private void SetTankActive(bool active)
    {
        m_Collider.enabled = active;

        m_TankRenderers.SetActive(active);
        m_HealthCanvas.SetActive(active);
        m_AimCanvas.SetActive(active);
        m_LeftDustTrail.SetActive(active);
        m_RightDustTrail.SetActive(active);

        m_TankSetup.ActivateCrown(active);
    }

    // This function is called at the start of each round to make sure each tank is set up correctly.
    public void SetDefaults()
    {
        m_CurrentHealth = m_StartingHealth;
        m_ZeroHealthHappened = false;
        SetTankActive(true);
    }
}
