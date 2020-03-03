using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PickUp : NetworkBehaviour
{
    public Player player;
    public Rigidbody rb;
    public BoxCollider col;
    private NetworkIdentity objNetId;
    public bool pickedUp;

    [Client]
    public void Activate()
    {
        CmdActivate();
    }

    [Command]
    void CmdActivate()
    {
        objNetId = this.gameObject.GetComponent<NetworkIdentity>();
        objNetId.AssignClientAuthority(connectionToClient);
        RpcActivate();
        objNetId.RemoveClientAuthority(connectionToClient);

    }
    [ClientRpc]
    void RpcActivate()
    {
        pickedUp = false;
        rb.useGravity = true;
        col.enabled = true;
        rb.freezeRotation = false;
        this.transform.parent = null;
    }

    [Client]
    public void Deactivate()
    {
        CmdDeactivate();
    }

    [Command]
    void CmdDeactivate()
    {
        objNetId = this.gameObject.GetComponent<NetworkIdentity>();
        objNetId.AssignClientAuthority(connectionToClient);
        
        RpcDeavtivate();
        objNetId.RemoveClientAuthority(connectionToClient);

    }
    [ClientRpc]
    void RpcDeavtivate()
    {
        col.enabled = false;
        rb.useGravity = false;
        rb.freezeRotation = true;
        pickedUp = true;
        
    }

    private void Update()
    {
        if (pickedUp)
        {
            this.transform.position = player.holdPosition.transform.position;
            this.transform.parent = player.transform;
        }
    }
}
