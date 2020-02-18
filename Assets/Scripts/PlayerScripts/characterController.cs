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
    public GameObject shield;
    public GameObject canvas;
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
        shield.SetActive(false);
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

        if (Input.GetKeyDown("escape"))
            {
                Cursor.lockState = CursorLockMode.None;
            }
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
        speed = 7.0f;
        jumpTakeOffSpeed = 6f;
        gun.SetActive(true);
        shield.SetActive(false);
        gameObject.GetComponent<Renderer>().material.color = Color.red;
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
