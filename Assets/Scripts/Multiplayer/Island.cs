using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Island : NetworkBehaviour
{
    public List<Player> players = new List<Player>();

    public int completedBuildings;
    public Island otherIsland;
    public int currentPlayers;
    public BoxCollider col;
    public bool islandActive;

    private void Start()
    {
        StartCoroutine(PlayerListCd());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.name != "Spectator")
            {
                players[currentPlayers] = other.gameObject.GetComponent<Player>();
                currentPlayers++;
            }
        }
    }

    IEnumerator PlayerListCd()
    {
        yield return new WaitForSeconds(3f);
        col.enabled = true;
        islandActive = true;
    }



    // Start is called before the first frame update

    //[Client]
    //public void ConstructBuilding(string newBuilding)
    //{
    //    CmdConstructBuilding(newBuilding);
    //}
    //private Building toBeConstructedBuilding;
    //[Command]
    //private void CmdConstructBuilding(string newBuilding)
    //{
    //    RpcConstructBuilding(newBuilding);
    //}

    //[ClientRpc]
    //private void RpcConstructBuilding(string newBuilding)
    //{
    //    foreach (Transform building in buildings)
    //    {

    //        if (building.name == newBuilding)
    //        {
    //            toBeConstructedBuilding = building.gameObject.GetComponent<Building>();
    //            if(toBeConstructedBuilding.woodCost <= totalWood && toBeConstructedBuilding.stoneCost <= totalStone)
    //            {
    //                totalWood -= toBeConstructedBuilding.woodCost;
    //                totalStone -= toBeConstructedBuilding.stoneCost;
    //                //toBeConstructedBuilding.building.SetActive(true);
    //                buildingBoard.RemoveBuilding();
    //                //toBeConstructedBuilding.building.gameObject.transform.parent = null;
    //                //Destroy(toBeConstructedBuilding.gameObject);
    //            }
    //            else
    //            {
    //                Debug.Log("Not enough");
    //            }

    //        }
    //    }
    //}
}
