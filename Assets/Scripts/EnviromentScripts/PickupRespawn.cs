using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRespawn : MonoBehaviour
{

    public GameObject pickup;
    bool active = true;
    public float time = 10f;

    void Update()
    {
        if(active == true)
        {
            pickup.SetActive(true);
        }
        else
        {
            pickup.SetActive(false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.tag == "Player")
            {
                active = false;
                Respawn();
                StartCoroutine(Respawn());
            }
        
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(time);
        active = true;
    }
}


