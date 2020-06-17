using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class TradingBoard : NetworkBehaviour
{
    public TextMeshPro theirOffer;
    public TextMeshPro yourOffer;
    public TextMeshPro accept;
    public TextMeshPro decline;
    public TextMeshPro acceptOther;
    public TextMeshPro declineOther;
    public List<string> items = new List<string>();
    public TradingBoard otherBoard;
    public int itemsInCrate;
    public bool tradeAccepted;
    private void Update()
    {
        yourOffer.text = items[0] + "\n" + items[1] + "\n" + items[2] + "\n" + items[3] + "\n" + items[4];
        theirOffer.text = otherBoard.items[0] + "\n" + otherBoard.items[1] + "\n" + otherBoard.items[2] + "\n" + otherBoard.items[3] + "\n" + otherBoard.items[4];
    }

    [Client]
    public void UpdateBoardInfo(bool accept)
    {
        CmdUpdateBoardInfo(accept);
    }
    [Command]
    private void CmdUpdateBoardInfo(bool accept)
    {
        RpcUpdateBoardInfo(accept);
    }
    [ClientRpc]
    private void RpcUpdateBoardInfo(bool acceptTrade)
    {
        if (acceptTrade)
        {
            accept.color = Color.green;
            decline.color = Color.white;
            otherBoard.acceptOther.color = Color.green;
            otherBoard.declineOther.color = Color.white;
            tradeAccepted = true;
            if (otherBoard.tradeAccepted)
            {
                TradeAccepted();
                otherBoard.TradeAccepted();
                tradeAccepted = false;
                otherBoard.tradeAccepted = false;
            }
        }
        else
        {
            accept.color = Color.white;
            decline.color = Color.red;
            otherBoard.acceptOther.color = Color.white;
            otherBoard.declineOther.color = Color.red;
            tradeAccepted = false;
        }
    }

    public Transform itemDrop;
    public GameObject rock;
    public GameObject wood;
    public GameObject mushroom;

    [Client]
    public void TradeAccepted()
    {
        CmdTradeAccepted();
    }
    [Command]
    private void CmdTradeAccepted()
    {
        RpcTradeAccepted();
    }
    [ClientRpc]
    private void RpcTradeAccepted()
    {
        StartCoroutine(SpawnItems(true));
    }

    [Client]
    public void Withdraw()
    {
        CmdWithdraw();
    }
    [Command]
    private void CmdWithdraw()
    {
        RpcWithdraw();
    }
    [ClientRpc]
    private void RpcWithdraw()
    {
        StartCoroutine(SpawnItems(false));
    }

    private Transform spawnLoc;
    IEnumerator SpawnItems(bool trade)
    {
        if (trade)
            spawnLoc = otherBoard.itemDrop;
        else
            spawnLoc = itemDrop;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == "Rock")
            {
                var rockObject = (GameObject)Instantiate(rock, spawnLoc.position, spawnLoc.rotation);
                rockObject.name = "Rock";
                items[i] = "";
            }
            else if (items[i] == "Wood")
            {
                var woodObject = (GameObject)Instantiate(wood, spawnLoc.position, spawnLoc.rotation);
                woodObject.name = "Wood";
                items[i] = "";
            }
            else if (items[i] == "Mushroom")
            {
                var mushroomObject = (GameObject)Instantiate(mushroom, spawnLoc.position, spawnLoc.rotation);
                mushroomObject.name = "Mushroom";
                items[i] = "";
            }
            yield return new WaitForSeconds(0.1f);
        }       
        yield return new WaitForSeconds(0.2f);
        itemsInCrate = 0;
    }


    private Player player;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player = other.GetComponent<Player>();
            if (player.player.tradingBoard == null)
            {
                player.player.tradingBoard = this;
                player.player.inRangeTradeSign = true;
            }
            else
            {
                player.player.inRangeTradeSign = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player.player.inRangeTradeSign = false;

        }
    }
}
