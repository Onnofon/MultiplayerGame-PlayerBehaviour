using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormChanger : MonoBehaviour
{
    private Player player;
    public bool pickaxe;
    public bool axe;
    public bool shovel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.GetComponent<Player>();
            if(pickaxe)
            {
                player.inRangeMiner = true;
            }
            else if(axe)
            {
                player.inRangeWoodCutter = true;
            }
            else if(shovel)
            {
                player.inRangeGatherer = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (pickaxe)
            {
                player.inRangeMiner = false;
            }
            else if (axe)
            {
                player.inRangeWoodCutter = false;
            }
            else if (shovel)
            {
                player.inRangeGatherer = false;
            }

        }
    }
}
