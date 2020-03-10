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
    public void Next()
    {
        listCounter++;
        if (listCounter <= buildings.Count)
        {
            currentBuilding = buildings[listCounter];
        }
        else
        {
            currentBuilding = buildings[0];
            listCounter = 0;
        }

        UpdateUI();

    }

    public int listCounter;
    public void Previous()
    {
        listCounter--;
        if (listCounter >= 0)
        {
            currentBuilding = buildings[listCounter];
        }
        else
        {
            currentBuilding = buildings[buildings.Count];
            listCounter = buildings.Count;
        }

        UpdateUI();
    }
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
        woodUI.text = "Wood: " + currentBuilding.woodCost.ToString();
        stoneUI.text = "Stone: " + currentBuilding.stoneCost.ToString();
        buildingName.text = currentBuilding.name;
    }
}
