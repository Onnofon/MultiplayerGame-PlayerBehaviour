using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Farm : NetworkBehaviour
{
    public int grain;
    public Crop[] soilSlots;

    [Client]
    private void AddGrain()
    {
        CmdAddGrain();
    }
    [Command]
    private void CmdAddGrain()
    {
        RpcAddGrain();
    }
    [ClientRpc]
    private void RpcAddGrain()
    {
        grain++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Grain")
        {
            AddGrain();
            Destroy(other.gameObject);
        }
    }

    [Client]
    public void PlantGrain()
    {
        if (grain > 0)
        {
            Debug.Log("small go 1");
            CmdPlantGrain();
            grain--;
        }
    }

    [Command]
    void CmdPlantGrain()
    {
        Debug.Log("small go 2");
        RpcPlantGrain();
    }
    [ClientRpc]
    void RpcPlantGrain()
    {
        Debug.Log("small go 3");
        foreach (Crop slot in soilSlots)
        {
            if(slot.isUnplanted)
            {
                Debug.Log("GO");
                slot.isUnplanted = false;
                break;
            }
        }
    }
}
