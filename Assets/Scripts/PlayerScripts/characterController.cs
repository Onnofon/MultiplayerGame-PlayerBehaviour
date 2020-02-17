using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class characterController : NetworkBehaviour
{
    // Use this for initialization

    public string form;
    public bool onGround;
    private Rigidbody rb;
    public float speed;
    public float normalSpeed = 7.0f;
    public float speedSprint = 12.0f;
    public float jumpTakeOffSpeed = 6f;
    public float health;
    public GameObject gun;
    public GameObject sword;
    public GameObject shield;
    public GameObject canvas;
    public GameObject tankModel;
    GameObject player;
    public bool changing = false;
    public float ChangeFormDelay = 5f;
    public float timeStamp;
    public int kills = 0;
    public int deaths = 0;
    public int consecKills = 0;
    public bool pickUpSpeed = false;
    public AudioSource lightsaberon;
    public AudioSource bulletform;

    void Start()
    {
        form = "AR";
        gun.SetActive(true);    
        sword.SetActive(false);
        shield.SetActive(false);
        tankModel.SetActive(true);
        //originalPointLightColour = pointLight.color;

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        onGround = true;
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.GetComponent<Renderer>().material.color = Color.red;

        if (isLocalPlayer)
        {
            canvas = Instantiate(canvas); //Adds canvas prefab
            canvas.transform.position = new Vector3(0, 0, 0);
            player = this.gameObject; //Assign player with the object the script is attached to
            canvas.GetComponent<ReloadingScript>().player = player;
            canvas.GetComponent<PickUp>().player = player;
            canvas.GetComponent<UIforHealthSpeedAmmo>().thisPlayer = player;
            StartCoroutine(waitChangeName()); //calls the object name change method

        }
        
    }

    IEnumerator waitChangeName()
    {
        yield return new WaitForSeconds(GameManager.waitTime); //waits for seconds x player number
        CmdChangeName(GameManager.customName); //calls the name change method on the server with the name from the player lobby
    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        speed = Input.GetKey(KeyCode.LeftShift) ? speedSprint : normalSpeed;
        transform.Translate(straffe, 0, translation);

        if (onGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.velocity = new Vector3(0f, jumpTakeOffSpeed, 0f);
                onGround = false;
            }
        }
        //if (Input.GetKeyDown("1") && Time.time >= timeStamp)
        //{
        //    bulletform.Play();
        //    FormAttack();
        //    timeStamp = Time.time + ChangeFormDelay;
        //}

        //if (Input.GetKeyDown("2") && Time.time >= timeStamp)
        //{
        //    bulletform.Play();
        //    FormHeavy();
        //    timeStamp = Time.time + ChangeFormDelay;
        //}
        //if (Input.GetKeyDown("3") && Time.time >= timeStamp)
        // {
        //    lightsaberon.Play();
        //     FormUtility();
        //     timeStamp = Time.time + ChangeFormDelay;
        // }

        if (Input.GetKeyDown("escape"))
            {
                Cursor.lockState = CursorLockMode.None;
            }
    }

    [Command]
    void CmdChangeName(string nieuweNaam) //tells server to call the RpcChangeName for all clients
    {
        RpcChangeName(nieuweNaam);
    }

    [ClientRpc] 
    void RpcChangeName(string nieuweNaam)
    {
        Player keepPlayer = GameManager.players[this.name]; //remembers the current player
        GameManager.players.Remove(this.name); //removes the key + value
        GameManager.players.Add(nieuweNaam, keepPlayer); //adds the remembered player with a new key
        this.name = nieuweNaam; //updates the object name with the new name
    }

    [Client]
    void FormAttack()
    {

        CmdformAttack();
    }

    [Command]
    void CmdformAttack()
    {
        RpcformAttack();
    }

    [ClientRpc]
    void RpcformAttack()
    {
        form = "AR";
        //speed = 7.0f;
        jumpTakeOffSpeed = 6f;
        sword.SetActive(false);
        gun.SetActive(true);
        shield.SetActive(false);
        tankModel.SetActive(true);
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    [Client]
    void FormHeavy()
    {
        CmdformHeavy();
    }

    [Command]
    void CmdformHeavy()
    {
        RpcformHeavy();
    }

    [ClientRpc]
    void RpcformHeavy()
    {
        form = "Tank";
        //speed = 4.0f;
        jumpTakeOffSpeed = 3f;
        shield.SetActive(true);
        sword.SetActive(false);
        gun.SetActive(false);
        tankModel.SetActive(true);
        gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }

   [Client]
    void FormUtility()
    {
        CmdformUtility();
    }

    [Command]
    void CmdformUtility()
    {
        RpcformUtility();
    }

    [ClientRpc]
    void RpcformUtility()
    {
        form = "Utility";
        //speed = 10.0f;
        jumpTakeOffSpeed = 8f;
        gun.SetActive(false);
        sword.SetActive(true);
        shield.SetActive(false);
        tankModel.SetActive(false);
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }  
}
