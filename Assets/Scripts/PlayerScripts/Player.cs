using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    public Player player;
    public PlayerMovement playerMov;
    public PlayerCanvas canvas;
    public int thresholdLow;
    public int thresholdTop;
    public Transform emotions;
    public Transform emotes;
    public GameObject currentEmotion;
    public Transform holdPosition;
    public PlayerActions playerActions;
    public CapsuleCollider triggerCol;

    //Checking players current health
    [SyncVar]
    public int currentHealth;
    public int maxHealth;
    public int currentHunger;
    public int maxHunger;

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
        currentHunger = maxHunger;

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

        if(!pickupInRange)
        {
            pickup = null;
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

    [Client]
    public void SetEmote(string newEmote)
    {
        CmdSetEmote(newEmote);
    }

    [Command]
    void CmdSetEmote(string newEmote)
    {
        RpcSetEmote(newEmote);
    }
    public Image img;
    [ClientRpc]
    void RpcSetEmote(string newEmote)
    {
        foreach (Transform emote in emotes)
        {

            if (emote.name == newEmote)
            {
                img = emote.gameObject.GetComponent<Image>();
                img.color = new Color(1, 1, 1, 1);

                for (float i = 5; i >= 0; i -= Time.deltaTime)
                {
                    if(i <= 0)
                    {
                        img.color = new Color(1, 1, 1, 0);
                    }
                }
            }
        }
    }

    public bool pickupInRange;
    public bool inRangeBuildingBoard;
    public bool inRangeFarm;
    public Farm farm;
    public BuildBoard buildBoard;
    public PickUp pickup;
    public HeavyResource heavyResource;
    public bool inRangePlayer;
    public Player otherPlayer;
    private void OnTriggerEnter(Collider other)
    {
        if (!playerActions.pickedUp)
        {
            if (other.tag == "PickUp" || other.tag == "Grain")
            {
                pickupInRange = true;
                pickup = other.GetComponent<PickUp>();
            }
            else if(other.tag == "HeavyResource")
            {
                pickupInRange = true;
                heavyResource = other.GetComponent<HeavyResource>();
            }
        }

        if(other.tag == "BuildingBoard")
        {
            inRangeBuildingBoard = true;
            buildBoard = other.gameObject.GetComponent<BuildBoard>();
        }

        if (other.tag == "Farm")
        {
            inRangeFarm = true;
            farm = other.gameObject.GetComponent<Farm>();
        }

        if(other.tag == "Player" && playerActions.pickedUp)
        {
            inRangePlayer = true;
            otherPlayer = other.gameObject.GetComponent<Player>();
            canvas.TradeOption(pickup.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PickUp")
        {
            pickupInRange = false;
        }

        if (other.tag == "BuildingBoard")
        {
            inRangeBuildingBoard = false;
        }

        if (other.tag == "Player")
        {
            inRangePlayer = false;
            otherPlayer = null;
            canvas.text.gameObject.SetActive(false);
        }

    }

    private PickUp tempSlot;
    [Client]
    public void TradeAccepted()
    {
        CmdTradeAccepted();
    }

    [Command]
    void CmdTradeAccepted()
    {
        RpcTradeAccepted();
    }
    [ClientRpc]
    void RpcTradeAccepted()
    {
        tempSlot = pickup;
        pickup = otherPlayer.pickup;
        otherPlayer.pickup = tempSlot;
    }

    private bool inRangeBuildingSign;
    public void DisplayCost(int[] costs)
    {
        canvas.DisplayCost(costs[0].ToString(),costs[1].ToString());
    }

    public void RemoveText()
    {
        canvas.text.gameObject.SetActive(false);
    }

    [Client]
    public void TradeRequest(string theirOffer)
    {
        CmdTradeRequest(theirOffer);
    }

    [Command]
    public void CmdTradeRequest(string theirOffer)
    {
        RpcTradeRequest(theirOffer);
    }

    [ClientRpc]
    public void RpcTradeRequest(string theirOffer)
    {
        canvas.TradeRequest(theirOffer, pickup.ToString());
    }
}
