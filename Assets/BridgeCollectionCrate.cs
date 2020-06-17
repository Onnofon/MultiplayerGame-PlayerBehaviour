using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeCollectionCrate : MonoBehaviour
{
    public BuildSign buildingSign;
    public BuildSign buildingSign2;
    public Collider col;
    public MeshRenderer mesh;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Rock")
        {
            buildingSign.currentStone++;
            if (buildingSign.building.stoneCost > 0)
                buildingSign.resources.Add(other.gameObject);
            buildingSign2.currentStone++;
            if (buildingSign2.building.stoneCost > 0)
                buildingSign2.resources.Add(other.gameObject);

        }
        if (other.name == "Wood")
        {
            buildingSign.currentWood++;
            if (buildingSign.building.woodCost > 0)
                buildingSign.resources.Add(other.gameObject);
            buildingSign2.currentWood++;
            if (buildingSign2.building.woodCost > 0)
                buildingSign2.resources.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Rock")
        {
            buildingSign.currentStone--;
            if (buildingSign.resources.Contains(other.gameObject))
            {
                buildingSign.resources.Remove(other.gameObject);
            }
            buildingSign2.currentStone--;
            if (buildingSign2.resources.Contains(other.gameObject))
            {
                buildingSign2.resources.Remove(other.gameObject);
            }
        }
        if (other.name == "Wood")
        {
            buildingSign.currentWood--;
            if (buildingSign.resources.Contains(other.gameObject))
            {
                buildingSign.resources.Remove(other.gameObject);
            }
            buildingSign2.currentWood--;
            if (buildingSign2.resources.Contains(other.gameObject))
            {
                buildingSign2.resources.Remove(other.gameObject);
            }
        }
    }
}
