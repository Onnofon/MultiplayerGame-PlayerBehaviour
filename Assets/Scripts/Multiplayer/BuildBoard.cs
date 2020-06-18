using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class BuildBoard : NetworkBehaviour
{
    //public List<Building> buildings = new List<Building>();
    //public Building currentBuilding;
    //public Island island;
    //public TextMeshProUGUI woodUI;
    //public TextMeshProUGUI stoneUI;
    //public TextMeshProUGUI buildingName;
    //public TextMeshProUGUI stoneIsland;
    //public TextMeshProUGUI woodIsland;
    //public GameObject[] votes;
    ////public GameObject buildButton;
    //public int listCounter;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    foreach (Transform building in island.buildings)
    //    {
    //        buildings.Add(building.gameObject.GetComponent<Building>());
    //    }
    //    currentBuilding = buildings[0];
    //}

    //[Client]
    //public void RemoveBuilding()
    //{
    //    CmdRemoveBuilding();
    //}

    //[Command]
    //private void CmdRemoveBuilding()
    //{
    //    RpcRemoveBuilding();
    //}
    //[ClientRpc]
    //private void RpcRemoveBuilding()
    //{
    //    buildings.Remove(currentBuilding);
    //    Next();
    //}
    //// Update is called once per frame
    //[Client]
    //public void Next()
    //{
    //    CmdNext();

    //}
    //[Command]
    //public void CmdNext()
    //{
    //    RpcNext();

    //}
    //[ClientRpc]
    //public void RpcNext()
    //{
    //    listCounter++;
    //    if(listCounter > buildings.Count-1)
    //    {
    //        listCounter = 0;
    //    }
    //    currentBuilding = buildings[listCounter];
    //}

    //private void Update()
    //{
    //    woodUI.text = "Wood cost: " + currentBuilding.woodCost.ToString();
    //    stoneUI.text = "Stone cost: " + currentBuilding.stoneCost.ToString();

    //    if (currentBuilding.woodCost > island.totalWood)
    //        woodIsland.color = Color.red;
    //    else
    //        woodIsland.color = Color.white;

    //    if (currentBuilding.stoneCost > island.totalStone)
    //        stoneIsland.color = Color.red;
    //    else
    //        stoneIsland.color = Color.white;

    //    stoneIsland.text = island.totalStone.ToString();
    //    woodIsland.text = island.totalWood.ToString();
    //    buildingName.text = currentBuilding.name;
    //}

    //[Client]
    //public void ConstructBuilding()
    //{
    //    CmdConstructBuilding();
    //}

    //[Command]
    //private void CmdConstructBuilding()
    //{
    //    RpcConstructBuilding();
    //}

    //[ClientRpc]
    //private void RpcConstructBuilding( )
    //{
    //    island.ConstructBuilding(currentBuilding.name);
    //}
}
