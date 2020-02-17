using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Prototype.NetworkLobby;

public class SettingLevel : MonoBehaviour {

    public LobbyManager lobby;
    public Dropdown drop;

    //The level that is to be used is equal to the value of the assigned Dropdown
    public void SetLevel()
    {
        if (drop.value == 0)
            lobby.playScene = "Level 01 Maze";
        if (drop.value == 1)
            lobby.playScene = "Level 02 Circle";
        if (drop.value == 2)
            lobby.playScene = "Level 03 Arena";
    }
}
