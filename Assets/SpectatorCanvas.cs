using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpectatorCanvas : MonoBehaviour
{
    public GameObject messagePanel;
    public GameObject toolsPanel;
    public GameObject rolesPanel;
    public GameObject resourcesPanel;
    public SpectatorControls spectator;
    public TMP_InputField input;
    public Player player;
    public TextMeshProUGUI selectedPlayer;
    public GameObject islandInfo;

    public void MessagePanel(bool open)
    {
        if (open)
        {
            messagePanel.SetActive(true);
            islandInfo.SetActive(true);
            toolsPanel.SetActive(false);
        }
        else
        {
            messagePanel.SetActive(false);
            islandInfo.SetActive(false);
        }
    }
    void Update()
    {
        if (spectator.island1.islandActive && spectator.island2.islandActive)
        {
            for (int i = 0; i < spectator.island1.players.Count; i++)
            {
                if (spectator.island1.players[i] != null)
                {
                    island1Players[i].text = spectator.island1.players[i].name;
                }
            }
            for (int i = 0; i < spectator.island2.players.Count; i++)
            {
                if (spectator.island2.players[i] != null)
                {
                    island2Players[i].text = spectator.island2.players[i].name;
                }
            }          
        }
    }
    public void SendMessageAll()
    {
        spectator.BroadCastMessageAll(input.text);
        messagePanel.SetActive(false);
        islandInfo.SetActive(false);
    }
    

    public void SendMessageTeam(bool team1)
    {
        spectator.BroadCastMessageTeam(input.text, team1);
        messagePanel.SetActive(false);
        islandInfo.SetActive(false);
    }

    public void SendMessageSingle()
    {
        if (player != null)
        {
            spectator.BroadCastMessageIndividual(input.text, player.name);
            islandInfo.SetActive(false);
            messagePanel.SetActive(false);
        }
    }

    public void ToolsPanel(bool open)
    {
        if (open)
        {
            toolsPanel.SetActive(true);
        }
        else
        {
            toolsPanel.SetActive(false);
        }
    }

    public void ResourcesPanel(bool open)
    {
        if(open)
        {
            resourcesPanel.SetActive(true);
            toolsPanel.SetActive(false);
        }
        else
        {
            resourcesPanel.SetActive(false);
        }
    }

    public void SpawnResource(string resource)
    {
        spectator.SpawnResource(resource);
    }

    public void RolesPanel(bool open)
    {
        if (open)
        {
            islandInfo.SetActive(true);
            rolesPanel.SetActive(true);
            toolsPanel.SetActive(false);
            //AddPlayers();
        }
        else
        {
            rolesPanel.SetActive(false);
            islandInfo.SetActive(false);
        }
    }

    public void ChangeRole(string role)
    {
        if (player != null)
        {
            spectator.ChangeRole(role, player.name);
            rolesPanel.SetActive(false);
            islandInfo.SetActive(false);
        }
    }

    public List<TextMeshProUGUI> island1Players;
    public List<TextMeshProUGUI> island2Players;

    public void PickPlayer(int playerName)
    {
        if (playerName <= 2)
        {
            player = spectator.island1.players[playerName];
            selectedPlayer.text = player.name;
        }
        else
        {
            player = spectator.island2.players[playerName-3];
            selectedPlayer.text = player.name;
        } 
    }
}
