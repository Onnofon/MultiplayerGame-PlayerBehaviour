using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    public TextMeshProUGUI playerName;
    public PlayerSetup playerSetup;
    public Player player;
    public PlayerMovement playerMov;
    public PlayerCanvas canvas;
    public PlayerForm playerForm;
    public int thresholdLow;
    public int thresholdTop;
    public Transform emotions;
    public Transform emotes;
    public GameObject currentEmotion;
    public Transform holdPosition;
    public PlayerActions playerActions;
    public CapsuleCollider triggerCol;
    public bool pendingTradeOffer;
    public PlayerAnimator playerAnim;
    public Behaviour spectatorScript;
    //Checking players current health
    [SyncVar]
    public int currentHealth;
    public int maxHealth;
    public int currentHunger;
    public int maxHunger;
    public GameObject body;
    public GameObject head;
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
            StartCoroutine(waitChangeName());

        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    IEnumerator waitChangeName()
    {
        yield return new WaitForSeconds(GameManager.waitTime); //waits for seconds x player number
        CmdChangeName(GameManager.customName); //calls the name change method on the server with the name from the player lobby
    }

    [Command]
    void CmdChangeName(string newName) //tells server to call the RpcChangeName for all clients
    {
        RpcChangeName(newName);
    }

    [ClientRpc]
    void RpcChangeName(string newName)
    {
        Player keepPlayer = GameManager.players[this.name]; //remembers the current player
        GameManager.players.Remove(this.name); //removes the key + value
        GameManager.players.Add(newName, keepPlayer); //adds the remembered player with a new key
        this.name = newName; //updates the object name with the new name
        playerName.text = this.name;
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
            //Die();
        }

        if (transform.position.y > thresholdTop)
        {
            //Die();

        }

        if (!pickupInRange)
        {
            pickup = null;
        }

        if(playerName.text == "Spectator" && !isSpectator)
        {
            ChangeToSpectator();
        }
    }
    private bool isSpectator;
    public Behaviour specControls;
    [Client]
    public void ChangeToSpectator()
    {
        CmdChangeToSpectator();
    }
    [Command]
    public void CmdChangeToSpectator()
    {
        RpcChangeToSpectator();
    }
    [ClientRpc]
    public void RpcChangeToSpectator()
    {
        //canvas.gameObject.SetActive(false);
        isSpectator = true;
        spectatorScript.enabled = true;
        specControls.enabled = true;
        head.SetActive(false);
        body.SetActive(false);
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
            //Die();
        }
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
                    if (i <= 0)
                    {
                        img.color = new Color(1, 1, 1, 0);
                    }
                }
            }
        }
    }

    public bool pickupInRange;
    public bool inRangeBuildingSign;
    public bool inRangeTradeSign;
    public bool inRangeFarm;
    public bool inRangeWoodCutter;
    public bool inRangeMiner;
    public bool inRangeGatherer;
    public Farm farm;
    public BuildSign buildSign;
    public TradingBoard tradingBoard;
    public GameObject pickup;
    public HeavyResource heavyResource;
    public bool inRangePlayer;
    public Player otherPlayer;

    [System.Obsolete]
    private void OnCollisionEnter(Collision col)
    {
        if (!playerActions.pickedUp)
        {
            if (col.other.tag == "PickUp")
            {
                pickupInRange = true;
                pickup = col.other.gameObject;
            }
        }

        if (col.other.tag == "BuildingSign")
        {
            inRangeBuildingSign = true;
            if(buildSign == null)
            buildSign = col.other.gameObject.GetComponent<BuildSign>();
        }

        //if (col.other.tag == "TradingBoard")
        //{
        //    inRangeTradeSign = true;
        //    if(tradingBoard == null)
        //    tradingBoard = col.other.gameObject.GetComponent<TradingBoard>();
        //}

        if (col.other.tag == "Farm")
        {
            inRangeFarm = true;
            if(farm == null)
            farm = col.other.gameObject.GetComponent<Farm>();
        }

        if (col.other.tag == "Player")
        {
            inRangePlayer = true;
            otherPlayer = col.other.gameObject.GetComponent<Player>();
            //canvas.TradeOption(playerActions.playerInv.items[playerActions.playerInv.currentSlot]);
        }
    }

    [System.Obsolete]
    private void OnCollisionExit(Collision col)
    {
        if (col.other.tag == "PickUp")
        {
            pickupInRange = false;
        }

        if (col.other.tag == "BuildSign")
        {
            inRangeBuildingSign = false;
        }
    }

    private string tempSlot;
    [Client]
    public void TradeAccepted(string theirOffer)
    {
        CmdTradeAccepted(theirOffer);
    }

    [Command]
    void CmdTradeAccepted(string theirOffer)
    {
        RpcTradeAccepted(theirOffer);
    }
    [ClientRpc]
    void RpcTradeAccepted(string theirOffer)
    {
        tempSlot = playerActions.playerInv.items[playerActions.playerInv.currentSlot];
        playerActions.playerInv.items[playerActions.playerInv.currentSlot] = theirOffer;
        pendingTradeOffer = false;
    }


    [Client]
    public void SpectatorMessage(string text)
    {
        CmdSpectatorMessage(text);
    }

    [Command]
    public void CmdSpectatorMessage(string text)
    {
        RpcSpectatorMessage(text);
    }

    [ClientRpc]
    public void RpcSpectatorMessage(string text)
    {
        canvas.BroadcastedMessage(text);
    }

    [Client]
    public void SetForm(string text)
    {
        CmdSetForm(text);
    }

    [Command]
    public void CmdSetForm(string text)
    {
        RpcSetForm(text);
    }

    [ClientRpc]
    public void RpcSetForm(string text)
    {
        if (text == "Woodcutter")
        {
            playerForm.Woodcutter();
        }
        else if (text == "Gatherer")
        {
            playerForm.Gatherer();
        }
        else if (text == "Miner")
        {
            playerForm.Miner();
        }
    }

    //[Client]
    //public void PlayAnimation(string anim)
    //{
    //    CmdPlayAnimation(anim);
    //}

    //[Command]
    //public void CmdPlayAnimation(string anim)
    //{
    //    RpcPlayAnimation(anim);
    //}
    //[ClientRpc]
    //public void RpcPlayAnimation(string anim)
    //{
    //    playerAnim.PlayAnimation(anim);
    //}
}
