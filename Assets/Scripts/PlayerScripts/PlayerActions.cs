using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerActions : NetworkBehaviour
{
    public Player player;
    public PlayerMovement playerMov;
    public bool pickedUp;
    public int votes = 1;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
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

        if (Input.GetMouseButtonDown(0) && player.pickupInRange && !pickedUp)
        {
            pickedUp = true;
            Debug.Log("Gimme");
            if(player.pickup != null)
            {
                PickUp();
            }
        }

        if (Input.GetMouseButtonDown(1) && pickedUp)
        {
            pickedUp = false;
            DropPickUp();
        }

        //if(Input.GetKeyDown(KeyCode.F) && tempBuilding != null)
        //{
        //    GetBuilding();
        //}

        if(Input.GetKeyDown(KeyCode.E) && player.inRangeBuildingBoard)
        {
            NextBuilding();
        }
        //if (Input.GetKeyDown(KeyCode.Q) && player.inRangeBuildingBoard)
        //{
        //    player.buildBoard.Previous();
        //}
        if (Input.GetKeyDown(KeyCode.F) && player.inRangeBuildingBoard)
        {
            Vote();
        }
        else if(Input.GetKeyDown(KeyCode.F) && player.inRangeFarm)
        {
            PlantGrain();
        }

        if (Input.GetKeyDown(KeyCode.T) && player.inRangePlayer)
        {
            Trade();
        }
    }

    [Client]
    void NextBuilding()
    {
        CmdNextBuilding();
    }

    [Command]
    void CmdNextBuilding()
    {
        RpcNextBuilding();
    }
    [ClientRpc]
    void RpcNextBuilding()
    {
        player.buildBoard.Next();
    }

    [Client]
    void Vote()
    {
        CmdVote();
    }

    [Command]
    void CmdVote()
    {
        RpcVote();
    }
    [ClientRpc]
    void RpcVote()
    {
        player.buildBoard.ConstructBuilding();
    }

    [Client]
    void Trade()
    {
        CmdTrade();
    }

    [Command]
    void CmdTrade()
    {
        RpcTrade();
    }
    [ClientRpc]
    void RpcTrade()
    {
        player.otherPlayer.TradeRequest(player.pickup.name);
    }

    [Client]
    void PlantGrain()
    {
        CmdPlantGrain();
    }

    [Command]
    void CmdPlantGrain()
    {
        RpcPlantGrain();
    }
    [ClientRpc]
    void RpcPlantGrain()
    {
        player.farm.PlantGrain();
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
            //player.triggerCol.enabled = false;
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
            player.pickup = null;
            player.triggerCol.enabled = true;
        }

    }
}
