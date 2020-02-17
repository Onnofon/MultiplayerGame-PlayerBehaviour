using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : Player {

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "DamageZone")
        {
            Die();

        }
        
    }
}

