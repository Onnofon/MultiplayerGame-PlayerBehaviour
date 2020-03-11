using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class BuildBoard : NetworkBehaviour
{
    public List<Building> buildings = new List<Building>();
    public Building currentBuilding;
    public Island island;
    public TextMeshProUGUI woodUI;
    public TextMeshProUGUI stoneUI;
    public TextMeshProUGUI buildingName;
    public TextMeshProUGUI stoneIsland;
    public TextMeshProUGUI woodIsland;
    public GameObject[] votes;
    //public GameObject buildButton;
    public int listCounter;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform building in island.buildings)
        {
            buildings.Add(building.gameObject.GetComponent<Building>());
        }
        currentBuilding = buildings[0];
        UpdateUI();
    }

    [Client]
    public void RemoveBuilding(string building)
    {
        CmdRemoveBuilding(building);
    }

    [Command]
    private void CmdRemoveBuilding(string building)
    {
        RpcRemoveBuilding(building);
    }
    [ClientRpc]
    private void RpcRemoveBuilding(string currentbuilding)
    {
        buildings.Remove(currentBuilding);
        UpdateUI();
    }
    // Update is called once per frame
    [Client]
    public void Next()
    {
        CmdNext();

    }
    [Command]
    public void CmdNext()
    {
        RpcNext();

    }
    [ClientRpc]
    public void RpcNext()
    {
        listCounter++;
        if(listCounter > buildings.Count)
        {
            listCounter = 0;
        }
        currentBuilding = buildings[listCounter];
        UpdateUI();
    }

    //[Client]
    //public void Previous()
    //{
    //    CmdPrevious();
    //}

    //[Command]
    //public void CmdPrevious()
    //{
    //    RpcPrevious();
    //}
    //[ClientRpc]
    //public void RpcPrevious()
    //{
    //    listCounter--;
    //    if (listCounter >= 0)
    //    {
    //        currentBuilding = buildings[listCounter];
    //    }
    //    else if(listCounter < 0)
    //    {
    //        currentBuilding = buildings[buildings.Count];
    //        listCounter = buildings.Count;
    //    }

    //    UpdateUI();
    //}
    [Client]
    private void UpdateUI()
    {
        CmdUpdateUI();
    }
    [Command]
    private void CmdUpdateUI()
    {
        RpcUpdateUI();
    }
    [ClientRpc]
    private void RpcUpdateUI()
    {
        foreach (var item in votes)
        {
            item.SetActive(false);
        }

        if (currentBuilding.votes != 3)
        {

            //woodUI.text = "Wood: " + currentBuilding.woodCost.ToString();
            //stoneUI.text = "Stone: " + currentBuilding.stoneCost.ToString();
            //buildingName.text = currentBuilding.name;
            for (int i = 0; i < currentBuilding.votes; i++)
            {
                votes[i].SetActive(true);
            }
        }
        else
        {
            //buildButton.SetActive(true);
        }
    }

    [Client]
    public void Vote()
    {
        CmdVote();
    }

    [Command]
    private void CmdVote()
    {
        RpcVote();
    }
    [ClientRpc]
    private void RpcVote()
    {
        currentBuilding.votes++;
        UpdateUI();
    }

    private void Update()
    {
        woodUI.text = "Wood cost: " + currentBuilding.woodCost.ToString();
        stoneUI.text = "Stone cost: " + currentBuilding.stoneCost.ToString();

        if (currentBuilding.woodCost > island.totalWood)
            woodIsland.color = Color.red;
        else
            woodIsland.color = Color.white;

        if (currentBuilding.stoneCost > island.totalStone)
            stoneIsland.color = Color.red;
        else
            stoneIsland.color = Color.white;

        stoneIsland.text = island.totalStone.ToString();
        woodIsland.text = island.totalWood.ToString();
        buildingName.text = currentBuilding.name;
    }

    [Client]
    public void ConstructBuilding()
    {
        CmdConstructBuilding();
    }

    [Command]
    private void CmdConstructBuilding()
    {
        RpcConstructBuilding();
    }

    [ClientRpc]
    private void RpcConstructBuilding( )
    {
        island.ConstructBuilding(currentBuilding.name);
    }
}
