using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
    public Player player;
    public bool onGround;
    public bool usingTool;
    public Rigidbody rb;
    public float speed;
    public float normalSpeed = 7.0f;
    public float speedSprint = 12.0f;
    public float jumpTakeOffSpeed = 6f;

    // Start is called before the first frame update
    void Start()
    {
        onGround = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!rb.IsSleeping() && onGround && !usingTool)
            player.playerAnim.PlayAnimation("move");
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
                player.playerAnim.PlayAnimation("jump");
                player.currentHunger -= 1;
            }
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
