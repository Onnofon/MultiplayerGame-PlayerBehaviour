using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    public Player player;
    public PlayerCanvas canvas;
    public int thresholdLow;
    public int thresholdTop;
    public Transform emotions;
    public GameObject currentEmotion;
    public Transform holdPosition;
    public PlayerActions playerActios;

    //Checking players current health
    [SyncVar]
    private int currentHealth;
    public int maxHealth = 100;

    //Checking if the players are dead
    [SyncVar]
    private bool _isDead = false;
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            canvas = Instantiate(canvas); //Adds canvas prefab
            canvas.transform.position = new Vector3(0, 0, 0);
            //canvas.GetComponent<UIforHealthSpeedAmmo>().thisPlayer = player;
            canvas.GetComponent<PlayerCanvas>().player = this.player;

        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;
    public void Setup()
    {
        //killFeed = GameObject.Find("KillFeed").GetComponent<KillFeedUI>();
        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }

        SetDefaults();
    }

    public int getHealth
    {
        get { return this.currentHealth; }
    }

    public void SetDefaults()
    {
        isDead = false;

        currentHealth = maxHealth;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        //Collider _col = GetComponent<Collider>();
        //if (_col != null)
        //{
        //    _col.enabled = true;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (transform.position.y < thresholdLow)
        {
            Die();
        }

        if (transform.position.y > thresholdTop)
        {
            Die();

        }
    }

    [ClientRpc]
    public void RpcTakeDamage(int _amount, string otherPlayer)
    {
        currentHealth -= _amount;
        Debug.Log(transform.name + " now has " + currentHealth + " health.");

        //Killfeed progress
        if (currentHealth <= 0)
        {
            //killFeed.showKill(otherPlayer, gameObject.name);
            this.gameObject.GetComponent<characterController>().deaths++;
            GameManager.GetPlayer(otherPlayer).GetComponent<characterController>().kills++;
            Die();
        }
    }

    public void Die()
    {

    }

    [Client]
    public void SetEmotion(string newEmotion)
    {
        CmdSetEmotion(newEmotion);
    }

    [Command]
    void CmdSetEmotion(string newEmotion)
    {
        RpcSetEmotion(newEmotion);
    }

    [ClientRpc]
    void RpcSetEmotion(string newEmotion)
    {
        foreach (Transform emotion in emotions)
        {

            if (emotion.name == newEmotion)
            {
                currentEmotion.gameObject.SetActive(false);
                emotion.gameObject.SetActive(true);
                currentEmotion = emotion.gameObject;

            }
        }
    }

    public bool pickupInRange;
    public PickUp pickup;
    private void OnTriggerEnter(Collider other)
    {
        if (!playerActios.pickedUp)
        {
            if (other.tag == "PickUp")
            {
                pickupInRange = true;
                pickup = other.GetComponent<PickUp>();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (playerActios.pickedUp)
        {
            if (other.tag == "PickUp")
            {
                pickupInRange = false;
                pickup = null;
            }
        }
    }
}
