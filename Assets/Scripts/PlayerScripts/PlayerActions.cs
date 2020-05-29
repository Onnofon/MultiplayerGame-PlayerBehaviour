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
    public bool toolOffCd;
    public bool isPulling;
    // Start is called before the first frame update
    void Start()
    {
        toolOffCd = true;
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
        else if (Input.GetMouseButtonDown(0) && !player.pickupInRange && toolOffCd && playerInv.currentSlot == 0)
        {
            UseTool();
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
        if (Input.GetKeyDown(KeyCode.F) && player.inRangeTradeSign)
        {
            Trade(true);
        }
        if(Input.GetKeyDown(KeyCode.F) && (player.inRangeGatherer || player.inRangeMiner || player.inRangeWoodCutter))
        {
            ChangeForm();
        }

        if (Input.GetKeyDown(KeyCode.G) && player.inRangeTradeSign)
        {
            Trade(false);
        }

        if (Input.GetKeyDown(KeyCode.H) && player.inRangeTradeSign)
        {
            Withdraw();
        }

        if (Input.GetKey(KeyCode.P))
        {
            Pull(true);
        }
        else
        {
            Pull(false);
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
        if (Input.GetKeyDown(KeyCode.E) && playerInv.currentSlot > 0 && playerInv.currentHoldItem != null)
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
    void UseTool()
    {
        CmdUseTool();
    }

    [Command]
    void CmdUseTool()
    {
        RpcUseTool();
    }
    [ClientRpc]
    void RpcUseTool()
    {
        toolOffCd = false;
        playerMov.usingTool = true;
        StartCoroutine(ToolSwing());
    }

    [Client]
    void ChangeForm()
    {
        CmdChangeForm();
    }

    [Command]
    void CmdChangeForm()
    {
        RpcChangeForm();
    }
    [ClientRpc]
    void RpcChangeForm()
    {
        if(player.inRangeWoodCutter)
        {
            playerForm.Woodcutter();
        }
        else if(player.inRangeGatherer)
        {
            playerForm.Gatherer();
        }
        else if(player.inRangeMiner)
        {
            playerForm.Miner();
        }
    }

    IEnumerator ToolSwing()
    {
        player.playerAnim.PlayAnimation("use");
        playerForm.toolCol.enabled = true;
        yield return new WaitForSeconds(2f);
        playerForm.toolCol.enabled = false;
        playerMov.usingTool = false;
        toolOffCd = true;

    }

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
    void Trade(bool accept)
    {
        CmdTrade(accept);
    }

    [Command]
    void CmdTrade(bool accept)
    {
        RpcTrade(accept);
    }
    [ClientRpc]
    void RpcTrade(bool accept)
    {
        player.tradingBoard.UpdateBoardInfo(accept);
    }

    [Client]
    void Withdraw()
    {
        CmdWithdraw();
    }

    [Command]
    void CmdWithdraw()
    {
        RpcWithdraw();
    }
    [ClientRpc]
    void RpcWithdraw()
    {
        player.tradingBoard.Withdraw();
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
    void Pull(bool pulling)
    {
        CmdPull(pulling);
    }

    [Command]
    void CmdPull(bool pulling)
    {
        RpcPull(pulling);
    }
    [ClientRpc]
    void RpcPull(bool pulling)
    {
        if (pulling)
            isPulling = true;
        else
            isPulling = false;
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
        player.pickupInRange = false;
        player.pickup = null;
    }
}
