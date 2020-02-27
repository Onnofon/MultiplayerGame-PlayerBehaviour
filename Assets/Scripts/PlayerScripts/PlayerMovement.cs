using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
    public bool onGround;
    public Rigidbody rb;
    public float speed;
    public float normalSpeed = 7.0f;
    public float speedSprint = 12.0f;
    public float jumpTakeOffSpeed = 6f;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            //canvas = Instantiate(canvas); //Adds canvas prefab
            //canvas.transform.position = new Vector3(0, 0, 0);
            player = this.gameObject; //Assign player with the object the script is attached to
            //canvas.GetComponent<ReloadingScript>().player = player;
            //canvas.GetComponent<UIforHealthSpeedAmmo>().thisPlayer = player;
            //StartCoroutine(waitChangeName()); //calls the object name change method

        }
        onGround = true;
        Cursor.lockState = CursorLockMode.Locked;
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

        if (Input.GetKeyDown("0"))
        {
            //EnterSpectator();
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }
}
