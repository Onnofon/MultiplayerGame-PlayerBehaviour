using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PullableRock : NetworkBehaviour
{
    public GameObject rockParent;
    public int playerCount;
    public List<PlayerActions> playerActions = new List<PlayerActions>();
    
    void Update()
    {
        if (playerCount >= 1)
        {
            for (int i = 0; i < playerActions.Count; i++)
            {
                if (!playerActions[i].isPulling)
                {
                    break;
                }
                else
                {
                    rockParent.transform.position -= new Vector3(0, 0, 0.004f);
                }
            }
        }
                        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerActions[playerCount] = other.gameObject.GetComponent<PlayerActions>();
            playerCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            playerCount--;
            playerActions[playerCount] = null; 
            
        }
    }

    [Client]
    private void Pulled()
    {
        CmdPulled();
    }

    [Command]
    private void CmdPulled()
    {
        RpcPulled();
    }

    [ClientRpc]
    private void RpcPulled()
    {       
        //rockParent.transform.position -= new Vector3(0, 0, 0.004f);
    }
}
