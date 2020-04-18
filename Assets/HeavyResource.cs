using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HeavyResource : NetworkBehaviour
{
    public Player player1;
    public Player player2;
    public Transform holdPos1;
    public Transform holdPos2;
    public bool pickedUp1;
    public bool pickedUp2;
    public BoxCollider trigger;
    // Start is called before the first frame update
    [Client]
    public void Activate()
    {
        CmdActivate();
    }

    [Command]
    void CmdActivate()
    {
        RpcActivate();
    }
    [ClientRpc]
    void RpcActivate()
    {
        pickedUp1 = false;
        trigger.enabled = true;
    }

    [Client]
    public void Deactivate()
    {
        CmdDeactivate();
    }

    [Command]
    void CmdDeactivate()
    {
        RpcDeavtivate();
    }
    [ClientRpc]
    void RpcDeavtivate()
    {
        if(!pickedUp1 && !pickedUp2)
        {
            pickedUp1 = true;
        }else if(pickedUp1 && !pickedUp2)
        {
            pickedUp2 = true;
        }
    }
    void Update()
    {
        if(pickedUp1 && !pickedUp2)
        {
            player1.playerMov.normalSpeed = 2f;
            holdPos1.transform.position = player1.holdPosition.transform.position;
        }
        if(pickedUp2 && !pickedUp1)
        {
            player2.playerMov.normalSpeed = 2f;
            holdPos2.transform.position = player1.holdPosition.transform.position;
        }

        if(pickedUp1 && pickedUp2)
        {
            trigger.enabled = false;
            player1.playerMov.normalSpeed = 7f;
            holdPos1.transform.position = player1.holdPosition.transform.position;
            player2.playerMov.normalSpeed = 7f;
            holdPos2.transform.position = player1.holdPosition.transform.position;
        }
    }
}
