using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MeleeAnim : NetworkBehaviour { 

    private int fireRate = 1;
    public float nextTimeToFire = 0f;
    public AudioSource swing;

    void Update()
    {
        Attacking();
    }

    void Attacking()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            swing.Play();
        }
    }
}
