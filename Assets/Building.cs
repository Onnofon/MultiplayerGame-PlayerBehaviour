using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Building : NetworkBehaviour
{
    public int woodCost;
    public int stoneCost;
    public int[] costs;
    public GameObject building;
    public Island island;

    private void Start()
    {
        costs[0] = woodCost;
        costs[1] = stoneCost;
    }
    private bool inRange;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.SendMessage("DisplayCost", costs);
        }

        inRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inRange = false;
        other.gameObject.SendMessage("RemoveText", costs);
    }

    private void Update()
    {
        if(inRange)
        {
            if(Input.GetKeyDown(KeyCode.G))
            {
                ConstructBuilding();
            }
        }
    }

    [Client]
    private void ConstructBuilding()
    {
        CmdConstructBuilding();
    }

    [Command]
    private void CmdConstructBuilding()
    {
        RpcConstructBuilding();
    }

    [ClientRpc]
    private void RpcConstructBuilding()
    {
        island.ConstructBuilding(this.name);
    }
}
