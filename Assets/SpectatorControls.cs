using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public Player selectedPlayer;
    public List<Player> allPlayers = new List<Player>();


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
        StartCoroutine(PlayerListCd());
    }

    [Client]
    public void BroadCastMessageAll(string message)
    {
        CmdBroadCastMessageAll(message);
    }

    [Command]
    public void CmdBroadCastMessageAll(string message)
    {
        RpcBroadCastMessageAll(message);
    }

    [ClientRpc]
    public void RpcBroadCastMessageAll(string message)
    {
        foreach (Player item in allPlayers)
        {
            if (item != null)
            {
                item.SpectatorMessage(message);
            }
        }
    }

    [Client]
    public void BroadCastMessageTeam(string message, bool team1)
    {
        CmdBroadCastMessageTeam(message, team1);
    }

    [Command]
    public void CmdBroadCastMessageTeam(string message, bool team1)
    {
        RpcBroadCastMessageTeam(message, team1);
    }

    [ClientRpc]
    public void RpcBroadCastMessageTeam(string message, bool team1)
    {
        if (team1)
        {
            foreach (Player item in island1.players)
            {
                if (item != null)
                {
                    item.SpectatorMessage(message);
                }
            }
        }
        else
        {
            foreach (Player item in island2.players)
            {
                if (item != null)
                {
                    item.SpectatorMessage(message);
                }
            }
        }
    }

    [Client]
    public void BroadCastMessageIndividual(string message, string name)
    {
        CmdBroadCastMessageIndividual(message, name);
    }

    [Command]
    public void CmdBroadCastMessageIndividual(string message, string name)
    {
        RpcBroadCastMessageIndividual(message, name);
    }

    [ClientRpc]
    public void RpcBroadCastMessageIndividual(string message, string name)
    {
        foreach (Player item in allPlayers)
        {
            if (item.name == name)
            {
                item.SpectatorMessage(message);
            }
        }
    }

    [Client]
    public void ChangeRole(string role, string name)
    {
        CmdChangeRole(role, name);
    }

    [Command]
    public void CmdChangeRole(string role, string name)
    {
        RpcChangeRole(role, name);
    }

    [ClientRpc]
    public void RpcChangeRole(string role, string name)
    {
        foreach (Player item in allPlayers)
        {
            if(item.name == name)
            {
                item.SetForm(role);
            }
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

    IEnumerator PlayerListCd()
    {
        yield return new WaitForSeconds(5f);

        foreach (var item in island1.players)
        {
            allPlayers.Add(item);
        }

        foreach (var item in island2.players)
        {
            allPlayers.Add(item);
        }
    }

    //[Client]
    //public void ListAllPlayers()
    //{
    //    CmdListAllPlayers();
    //}
    //[Command]
    //public void CmdListAllPlayers()
    //{
    //    RpcListAllPlayers();
    //}
    //[ClientRpc]
    //public void RpcListAllPlayers()
    //{
    //    foreach (var item in island1.players)
    //    {
    //        allPlayers.Add(item);
    //    }

    //    foreach (var item in island2.players)
    //    {
    //        allPlayers.Add(item);
    //    }
    //}

}
