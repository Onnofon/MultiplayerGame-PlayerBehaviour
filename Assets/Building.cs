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
    public MeshRenderer mesh;

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
            other.gameObject.SendMessage("SetBuilding", this);
        }

        inRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inRange = false;
        other.gameObject.SendMessage("RemoveText");
        other.gameObject.SendMessage("SetBuilding", null);
    }

    [Client]
    public void ConstructBuilding(GameObject player)
    {
        CmdConstructBuilding(player);
    }

    [Command]
    private void CmdConstructBuilding(GameObject player)
    {
        RpcConstructBuilding(player);
    }

    [ClientRpc]
    private void RpcConstructBuilding(GameObject player)
    {
        island.ConstructBuilding(this.name, player);
    }
}
