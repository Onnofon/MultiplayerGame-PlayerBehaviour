using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerActions : NetworkBehaviour
{
    public Player player;
    public PlayerMovement playerMov;
    public bool pickedUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.E))
        {
            player.canvas.optionsMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            player.canvas.optionsMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Input.GetKey("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if(Input.GetKeyDown(KeyCode.Q) && player.pickupInRange && !pickedUp)
        {
            pickedUp = true;
            Debug.Log("Gimme");
            PickUp();
        }

        if (Input.GetKeyDown(KeyCode.F) && pickedUp)
        {
            pickedUp = false;
            DropPickUp();
        }
    }

    [Client]
    void PickUp()
    {
        CmdPickup();
    }

    [Command]
    void CmdPickup()
    {
        RpcPickup();

    }
    [ClientRpc]
    void RpcPickup()
    {
        if (player.pickup != null)
        {
            player.pickup.player = player;
            player.pickup.Deactivate();
        }
    }

    [Client]
    void DropPickUp()
    {
        CmdDrop();
    }

    [Command]
    void CmdDrop()
    {
        RpcDrop();
    }
    [ClientRpc]
    void RpcDrop()
    {
        if (player.pickup != null)
        {
            player.pickup.Activate();
        }

    }
}
