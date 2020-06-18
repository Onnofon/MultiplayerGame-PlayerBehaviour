using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpectatorControls : NetworkBehaviour
{
    public SpectatorCanvas specCanvas;
    public Island island1;
    public Island island2;

    public GameObject rock;
    public GameObject wood;
    public GameObject mushroom;
    public Transform itemDrop;
    public Player player;
    public Behaviour[] scripts;


    private void Start()
    {
        if (isLocalPlayer)
        {
            specCanvas = Instantiate(specCanvas); //Adds canvas prefab
            specCanvas.transform.position = new Vector3(0, 0, 0);
            specCanvas.GetComponent<SpectatorCanvas>().spectator = this;

            player.canvas.gameObject.SetActive(false);
            foreach (Behaviour item in scripts)
            {
                item.enabled = false;
            }
        }

        island1 = FindObjectOfType<Island>();
        island2 = island1.otherIsland;
        //RemoveFromList();
    }

    [Client]
    public void BroadCastMessage(string message)
    {
        CmdBroadCastMessage(message);
    }

    [Command]
    public void CmdBroadCastMessage(string message)
    {
        RpcBroadCastMessage(message);
    }

    [ClientRpc]
    public void RpcBroadCastMessage(string message)
    {
        foreach (Player item in island1.players)
        {
            item.SpectatorMessage(message);
        }

        foreach (Player item in island2.players)
        {
            item.SpectatorMessage(message);
        }
    }

    [Client]
    public void SpawnResource(string resource)
    {
        CmdSpawnResource(resource);
    }

    [Command]
    public void CmdSpawnResource(string resource)
    {
        RpcSpawnResource(resource);
    }

    [ClientRpc]
    public void RpcSpawnResource(string resource)
    {
        if (resource == "Rock")
        {
            Debug.Log("Rock drop");
            var rockObject = (GameObject)Instantiate(rock, itemDrop.position, itemDrop.rotation);
            rockObject.name = "Rock";
        }
        else if (resource == "Wood")
        {
            var woodObject = (GameObject)Instantiate(wood, itemDrop.position, itemDrop.rotation);
            woodObject.name = "Wood";
        }
        else if (resource == "Mushroom")
        {
            var mushroomObject = (GameObject)Instantiate(mushroom, itemDrop.position, itemDrop.rotation);
            mushroomObject.name = "Mushroom";

        }
    }
    [Client]
    public void RemoveFromList()
    {
        CmdRemoveFromList();
    }
    [Command]
    public void CmdRemoveFromList()
    {
        RpcRemoveFromList();
    }
    [ClientRpc]
    public void RpcRemoveFromList()
    {
        Debug.Log("Helo");
        island1.players.Remove(player);
        island2.players.Remove(player);
    }

}
