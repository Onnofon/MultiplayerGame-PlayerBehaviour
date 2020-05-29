using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public int toolDamage;
    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.name == "Axe" && other.tag == "Tree")
        {
            other.gameObject.SendMessage("RpcTakeDamage", toolDamage);
        }

        if (this.gameObject.name == "Pickaxe" && other.tag == "Boulder")
        {
            other.gameObject.SendMessage("RpcTakeDamage", toolDamage);
        }

        if (this.gameObject.name == "shovel" && other.tag == "DigSpot")
        {
            other.gameObject.SendMessage("RpcTakeDamage", toolDamage);
        }
        if (other.tag == "Building")
        {
            other.gameObject.SendMessage("RpcTakeDamage", toolDamage);
        }
    }
}
