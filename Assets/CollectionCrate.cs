using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionCrate : MonoBehaviour
{
    public BuildSign buildingSign;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Rock")
        {
            buildingSign.currentStone++;
            if (buildingSign.building.stoneCost > 0)
                buildingSign.resources.Add(other.gameObject);
        }
        if (other.name == "Wood")
        {
            buildingSign.currentWood++;
            if (buildingSign.building.woodCost > 0)
                buildingSign.resources.Add(other.gameObject);
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
        }
        if (other.name == "Wood")
        {
            buildingSign.currentWood--;
            if (buildingSign.resources.Contains(other.gameObject))
            {
                buildingSign.resources.Remove(other.gameObject);
            }
        }
    }
}
