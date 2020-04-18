using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Building : NetworkBehaviour
{
    public int woodCost;
    public int stoneCost;
    public GameObject building;
    private Island island;

    private void Start()
    {
        island = this.transform.root.GetComponent<Island>();
    }
}
