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
    public List<MeshRenderer> meshComponents = new List<MeshRenderer>();
    public List<Collider> colComponents = new List<Collider>();
    public GameObject canvas;

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
            foreach (MeshRenderer item in building.meshComponents)
            {
                item.enabled = true;
            }
            foreach (Collider item in building.colComponents)
            {
                item.enabled = true;
            }

            foreach (MeshRenderer item in meshComponents)
            {
                item.enabled = false;
            }
            foreach (Collider item in colComponents)
            {
                item.enabled = false;
            }
            canvas.gameObject.SetActive(false);

        }
        else
        {
            Debug.Log("Not enough");
        }

        
    }

    [Client]
    public void DeconstructBuilding()
    {
        CmdDeconstructBuilding();
    }

    [Command]
    private void CmdDeconstructBuilding()
    {
        RpcDeconstructBuilding();
    }

    [ClientRpc]
    private void RpcDeconstructBuilding()
    {
        StartCoroutine(Deconstruct());
    }

    IEnumerator Deconstruct()
    {
        foreach (MeshRenderer item in building.meshComponents)
        {
            item.enabled = false;
        }
        foreach (Collider item in building.colComponents)
        {
            item.enabled = false;
        }

        foreach (MeshRenderer item in meshComponents)
        {
            item.enabled = true;
        }
        foreach (Collider item in colComponents)
        {
            item.enabled = true;
        }
        canvas.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        currentStone = 0;
        currentWood = 0;
    }
}
