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
    public GameObject[] votes;
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
        if (listCounter <= buildings.Count)
        {
            currentBuilding = buildings[listCounter];
            
        }
        else if(listCounter > buildings.Count)
        {
            currentBuilding = buildings[0];
            listCounter = 0;
        }
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
        //woodUI.text = "Wood: " + currentBuilding.woodCost.ToString();
        //stoneUI.text = "Stone: " + currentBuilding.stoneCost.ToString();
        //buildingName.text = currentBuilding.name;
        for (int i = 0; i < currentBuilding.votes; i++)
        {
            votes[i].SetActive(true);
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
        woodUI.text = "Wood: " + currentBuilding.woodCost.ToString();
        stoneUI.text = "Stone: " + currentBuilding.stoneCost.ToString();
        buildingName.text = currentBuilding.name;
    }
}
