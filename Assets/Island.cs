using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Island : NetworkBehaviour
{
    public int totalWood;
    public int totalStone;
    public TextMeshProUGUI woodUI;
    public TextMeshProUGUI stoneUI;
    public Transform buildings;
    // Start is called before the first frame update

    [Client]
    public void ConstructBuilding(string newBuilding)
    {
        CmdConstructBuilding(newBuilding);
    }
    private Building toBeConstructedBuilding;
    [Command]
    private void CmdConstructBuilding(string newBuilding)
    {
        RpcConstructBuilding(newBuilding);
    }

    [ClientRpc]
    private void RpcConstructBuilding(string newBuilding)
    {
        foreach (Transform building in buildings)
        {

            if (building.name == newBuilding)
            {
                toBeConstructedBuilding = building.gameObject.GetComponent<Building>();
                if(toBeConstructedBuilding.woodCost <= totalWood && toBeConstructedBuilding.stoneCost <= totalStone)
                {
                    totalWood -= toBeConstructedBuilding.woodCost;
                    totalStone -= toBeConstructedBuilding.stoneCost;
                    toBeConstructedBuilding.building.SetActive(true);
                    toBeConstructedBuilding.building.gameObject.transform.parent = null;
                    Destroy(toBeConstructedBuilding.gameObject);
                }
                else
                {
                    Debug.Log("Not enough bish");
                }

            }
        }
    }

    private void Update()
    {
        woodUI.text = "Wood: " + totalWood.ToString();
        stoneUI.text = "Stone: " + totalStone.ToString();
    }
}
