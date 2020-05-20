using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerActions : NetworkBehaviour
{
    public Player player;
    public PlayerMovement playerMov;
    public PlayerInventory playerInv;
    public PlayerForm playerForm;
    public bool pickedUp;
    public bool isPulling;
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
            if (!player.pendingTradeOffer)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        if (Input.GetKey("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        //if (Input.GetMouseButtonDown(0) && player.pickupInRange && !pickedUp)
        //{
        //    pickedUp = true;
        //    Debug.Log("Gimme");
        //    if(player.pickup != null)
        //    {
        //        PickUp();
        //    }
        //}

        //if (Input.GetMouseButtonDown(1) && pickedUp)
        //{
        //    pickedUp = false;
        //    DropPickUp();
        //}

        //if(Input.GetKeyDown(KeyCode.F) && tempBuilding != null)
        //{
        //    GetBuilding();
        //}

        if (Input.GetMouseButtonDown(0) && player.pickupInRange)
        {
            playerInv.AddToIventory(player.pickup.name);
            DeletePickup();
        }

        //if (Input.GetKeyDown(KeyCode.E) && player.inRangeBuildingBoard)
        //{
        //    NextBuilding();
        //}
        //if (Input.GetKeyDown(KeyCode.Q) && player.inRangeBuildingBoard)
        //{
        //    player.buildBoard.Previous();
        //}
        if (Input.GetKeyDown(KeyCode.F) && player.inRangeBuildingSign)
        {
            Build();
        }
        else if(Input.GetKeyDown(KeyCode.F) && player.inRangeFarm)
        {
            PlantGrain();
        }

        if (Input.GetKeyDown(KeyCode.T) && player.inRangePlayer)
        {
            Trade();
        }

        if(Input.GetKey(KeyCode.P))
        {
            isPulling = true;
        }
        else
        {
            isPulling = false;
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerInv.SetHoldItem(playerInv.items[0]);
            playerInv.currentSlot = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerInv.SetHoldItem(playerInv.items[1]);
            playerInv.currentSlot = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerInv.SetHoldItem(playerInv.items[2]);
            playerInv.currentSlot = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerInv.SetHoldItem(playerInv.items[3]);
            playerInv.currentSlot = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            playerInv.SetHoldItem(playerInv.items[4]);
            playerInv.currentSlot = 4;
        }
        if (Input.GetKeyDown(KeyCode.E) && playerInv.currentSlot > 0)
        {
            playerInv.DropItem();
        }

    }

    //[Client]
    //void NextBuilding()
    //{
    //    CmdNextBuilding();
    //}

    //[Command]
    //void CmdNextBuilding()
    //{
    //    RpcNextBuilding();
    //}
    //[ClientRpc]
    //void RpcNextBuilding()
    //{
    //    player.buildBoard.Next();
    //}

    [Client]
    void Build()
    {
        CmdBuild();
    }

    [Command]
    void CmdBuild()
    {
        RpcBuild();
    }
    [ClientRpc]
    void RpcBuild()
    {
        player.buildSign.ConstructBuilding();
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
        if (!player.otherPlayer.pendingTradeOffer)
        {
            player.otherPlayer.TradeRequest(player.pickup.name);
        }
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
            //player.pickup.player = player;
            //player.pickup.Deactivate();
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
            //player.pickup.Activate();
            player.pickup = null;
            player.triggerCol.enabled = true;
        }

    }

    [Client]
    void DeletePickup()
    {
        CmdDeletePickUp();
    }

    [Command]
    void CmdDeletePickUp()
    {
        RpcDeletePickup();
    }
    [ClientRpc]
    void RpcDeletePickup()
    {
        player.pickup.SetActive(false);
        player.pickup = null;
    }
}
