using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Building : NetworkBehaviour
{
    public int woodCost;
    public int stoneCost;
    public List<MeshRenderer> meshComponents = new List<MeshRenderer>();
    public List<Collider> colComponents = new List<Collider>();

}
