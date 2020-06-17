using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameObjectActiveTest : NetworkBehaviour
{
    public GameObject building;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Build();
        }
    }

    void Build()
    {
        CmdBuild();
    }

    void CmdBuild()
    {
        RpcBuild();

    }

    void RpcBuild()
    {
        building.SetActive(true);
    }
}
