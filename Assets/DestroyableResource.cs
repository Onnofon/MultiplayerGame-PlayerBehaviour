using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DestroyableResource : NetworkBehaviour
{
    public int health;
    public List<GameObject> itemDrops = new List<GameObject>();
    public GameObject rock;
    public GameObject wood;
    public GameObject mushroom;
    public Transform spawnLoc;
    private bool canDamage = true;
    private bool destroyable = true;

    [ClientRpc]
    public void RpcTakeDamage(int damage)
    {
        if (canDamage)
        {
            canDamage = false;
            health -= damage;
            if (health < 0)
            {
                StartCoroutine(DropItems());
            }
            canDamage = true;
        }
    }

    IEnumerator DropItems()
    {
        if (destroyable)
        {
            destroyable = false;
            for (int i = 0; i < itemDrops.Count; i++)
            {
                if (itemDrops[i].name == "Rock")
                {
                    var rockObject = (GameObject)Instantiate(rock, spawnLoc.position, spawnLoc.rotation);
                    rockObject.name = "Rock";
                }
                else if (itemDrops[i].name == "Wood")
                {
                    var woodObject = (GameObject)Instantiate(wood, spawnLoc.position, spawnLoc.rotation);
                    woodObject.name = "Wood";
                }
                else if (itemDrops[i].name == "Mushroom")
                {
                    var mushroomObject = (GameObject)Instantiate(mushroom, spawnLoc.position, spawnLoc.rotation);
                    mushroomObject.name = "Mushroom";
                }
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.1f);
            Destroy(this.gameObject);
            destroyable = true;
        }
    }
}
