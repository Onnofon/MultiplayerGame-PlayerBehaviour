using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restarter : Player
{

    public int thresholdlow;
    public int thresholdtop;

    void FixedUpdate()
    {
        if (transform.position.y < thresholdlow)
        {
            Die();
        }

        if (transform.position.y < thresholdtop)
        {
            Die();
        }
    }
}
