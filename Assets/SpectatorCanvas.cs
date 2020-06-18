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

    public void MessagePanel(bool open)
    {
        if (open)
        {
            messagePanel.SetActive(true);
            toolsPanel.SetActive(false);
        }
        else
        {
            messagePanel.SetActive(false);
        }
    }
    void Update()
    {
    }
        public void SendMessage()
    {
        spectator.BroadCastMessage(input.text);
        messagePanel.SetActive(false);
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
            rolesPanel.SetActive(true);
            toolsPanel.SetActive(false);
            AddPlayers();
        }
        else
        {
            rolesPanel.SetActive(false);
        }
    }

    public void ChangeRole(string role)
    {
        if (player != null)
        {
            player.SetForm(role);
        }
    }

    public TextMeshProUGUI player1;
    public TextMeshProUGUI player2;
    public TextMeshProUGUI player3;
    public TextMeshProUGUI player4;
    public TextMeshProUGUI player5;
    public TextMeshProUGUI player6;

    public void AddPlayers()
    {
        player1.text = spectator.island1.players[0].name;
        player2.text = spectator.island1.players[1].name;
        player3.text = spectator.island1.players[2].name;
        player4.text = spectator.island2.players[0].name;
        player5.text = spectator.island2.players[1].name;
        player6.text = spectator.island2.players[2].name;
    }

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
