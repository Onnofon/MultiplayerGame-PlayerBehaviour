﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PickUp : NetworkBehaviour
{
    public Player player;
    public Rigidbody rb;
    public BoxCollider col;
    public BoxCollider trigger;
    public bool pickedUp;
    public int woodValue;
    public int stoneValue;

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
        this.transform.parent = null;
        pickedUp = false;
        rb.useGravity = true;
        col.enabled = true;
        trigger.enabled = true;
        rb.freezeRotation = false;
        player = null;
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
        col.enabled = false;
        rb.useGravity = false;
        pickedUp = true;
        
    }

    private void Update()
    {
        if (pickedUp)
        {
            trigger.enabled = false;
            rb.freezeRotation = true;
            this.transform.position = player.holdPosition.transform.position;
            this.transform.parent = player.transform;
        }
    }
}