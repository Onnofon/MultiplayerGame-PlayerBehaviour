using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class IslandResources : NetworkBehaviour
{
    //private PickUp pickUp;
    //public Island island;

    //[Client]
    //private void AddStone(int stone)
    //{
    //    CmdAddStone(stone);
    //}

    //[Client]
    //private void AddWood(int wood)
    //{
    //    CmdAddWood(wood);
    //}

    //[Command]
    //private void CmdAddStone(int stone)
    //{
    //    RpcAddStone(stone);
    //}
    //[Command]
    //private void CmdAddWood(int wood)
    //{
    //    RpcAddWood(wood);
    //}
    //[ClientRpc]
    //private void RpcAddStone(int stone)
    //{
    //    island.totalStone += stone;
    //}
    //[ClientRpc]
    //private void RpcAddWood(int wood)
    //{
    //    island.totalWood += wood;
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "PickUp")
    //    {
    //        pickUp = other.GetComponent<PickUp>();
    //        if (pickUp.stoneValue == 0)
    //        {
    //            AddWood(pickUp.woodValue);
    //        }   
    //        else
    //        {
    //            AddStone(pickUp.stoneValue);
    //        }
    //        Destroy(other.gameObject);
    //    }
    //}
}
