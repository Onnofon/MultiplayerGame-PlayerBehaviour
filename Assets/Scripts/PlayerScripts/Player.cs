using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    // Use this for initialization
    KillFeedUI killFeed;
    public GameObject player;
    PlayerShoot psScript;
    public bool pickUpHealth = false;
    public bool pickUpSpeed = false;
    public bool pickUpGrenade = false;
    characterController cScript;
    public int thresholdlow;
    public int thresholdtop;
    public AudioSource damage;
    public AudioSource death;

    //Checking if the players are dead
    [SyncVar]
    private bool _isDead = false;
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }


    [SerializeField]
    public int maxHealth = 100;

    //Checking players current health
    [SyncVar]
    private int currentHealth;


    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    //Loop to disable player prefab components
    public void Setup()
    {
        killFeed = GameObject.Find("KillFeed").GetComponent<KillFeedUI>();
        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }

        SetDefaults();
    }

    //Sends damage over the server
    [ClientRpc]
    public void RpcTakeDamage(int _amount, string otherPlayer)
    {
        currentHealth -= _amount;
        damage.Play();
        Debug.Log(transform.name + " now has " + currentHealth + " health.");

        //Killfeed progress
        if (currentHealth <= 0)
        {
            killFeed.showKill(otherPlayer, gameObject.name);
            this.gameObject.GetComponent<characterController>().deaths++;
            this.gameObject.GetComponent<characterController>().consecKills = 0;
            GameManager.GetPlayer(otherPlayer).GetComponent<characterController>().kills++;
            GameManager.GetPlayer(otherPlayer).GetComponent<characterController>().consecKills++;
            if (GameManager.GetPlayer(otherPlayer).GetComponent<characterController>().consecKills == 2)
            {
                killFeed.GetComponent<KillFeedUI>().twoKills.Play();
            }

            if (GameManager.GetPlayer(otherPlayer).GetComponent<characterController>().consecKills == 3)
            {
                killFeed.GetComponent<KillFeedUI>().threeKills.Play();
            }

            if (GameManager.GetPlayer(otherPlayer).GetComponent<characterController>().consecKills == 4)
            {
                killFeed.GetComponent<KillFeedUI>().fourKills.Play();
            }
            if (GameManager.GetPlayer(otherPlayer).GetComponent<characterController>().consecKills ==5)
            {
                killFeed.GetComponent<KillFeedUI>().fiveKills.Play();
            }
            if (GameManager.GetPlayer(otherPlayer).GetComponent<characterController>().consecKills > 5)
            {
                killFeed.GetComponent<KillFeedUI>().moreKills.Play();
            }

            Die();
        }
    }

    //When player dies components get disabled
    public void Die()
    {
        isDead = true;
        death.Play();
        this.gameObject.transform.position = new Vector3(70, 70,  70);
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }

        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = false;

        Debug.Log(transform.name + " is dead");

        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        pickUpHealth = false;
        pickUpSpeed = false;
        pickUpGrenade = false;
        yield return new WaitForSeconds(3f);
        SetDefaults();
        Transform _spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = _spawnPoint.position;
        transform.rotation = _spawnPoint.rotation;
    }

    //Components that were turned off turned on
    public void SetDefaults()
    {
        isDead = false;

        currentHealth = maxHealth;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        Collider _col = GetComponent<Collider>();
        if (_col != null)
        {
            _col.enabled = true;
        }

        //Weapon information 
        psScript = player.GetComponent<PlayerShoot>();
        psScript.ammoAR = psScript.maxAmmoAR;
        psScript.ammoTank = psScript.maxAmmoTank;
        psScript.ammoUtility = psScript.maxAmmoUtility;

    }

    public int getHealth
    {
        get { return this.currentHealth; }
    }

    //Collision for pickups and activation powerups
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up Health"))
        {
            currentHealth += 10;
            pickUpHealth = true;
        }

        if (other.gameObject.CompareTag("Pick Up Grenade"))
        {
            pickUpGrenade = true;
            psScript.grenades += 1;
            psScript.grenadesCount += 1;
        }

        if (other.gameObject.CompareTag("Pick Up Speed"))
        {
            GetComponent<characterController>().speed += 2.5f;
            pickUpSpeed = true;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "DamageZone")
        {
            Die();

        }
    }

    //Keeps checking player health and location
    void Update()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (transform.position.y < thresholdlow)
        {
            Die();
        }

        if (transform.position.y > thresholdtop)
        {
            Die();
        }
    }
}
