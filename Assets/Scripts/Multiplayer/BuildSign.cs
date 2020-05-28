using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class BuildSign : NetworkBehaviour
{
    public TextMeshProUGUI woodUI;
    public TextMeshProUGUI stoneUI;
    public TextMeshProUGUI buildingName;
    public Building building;
    public int currentStone;
    public int currentWood;
    public List<GameObject> resources = new List<GameObject>();


    private void Update()
    {
        woodUI.text = "Wood cost: " + building.woodCost.ToString();
        stoneUI.text = "Stone cost: " + building.stoneCost.ToString();

        if (building.woodCost > currentWood)
            woodUI.color = Color.red;
        else
            woodUI.color = Color.white;

        if (building.stoneCost > currentStone)
            stoneUI.color = Color.red;
        else
            stoneUI.color = Color.white;
        
        buildingName.text = building.name;
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
    private void RpcConstructBuilding()
    {

        if (building.woodCost <= currentWood && building.stoneCost <= currentStone)
        {
            foreach (var item in resources)
            {
                Destroy(item);
            }
            building.mesh.enabled = true;
            building.col.enabled = true;
            building.gameObject.transform.parent = null;
            this.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Not enough");
        }

        
    }
}
