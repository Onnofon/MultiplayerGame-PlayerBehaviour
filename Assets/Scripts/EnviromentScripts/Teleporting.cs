using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporting : MonoBehaviour {

    public GameObject teleToTele;

    public float disableTimer = 0f;
    public float timeDisabled;

    //The timer that determines wheter the teleporter is active or not
    private void Update()
    {
        if (disableTimer > 0f)
        {
            disableTimer -= Time.deltaTime;
        }
    }

    //An object collides with the Teleporter
    private void OnCollisionEnter(Collision collision)
    {
        if (disableTimer <= 0)
        {
            collision.gameObject.transform.position = teleToTele.transform.position + new Vector3(0, 2, 0); //The collided object is moved on top of the teleToTele object
            disableTimer = timeDisabled; //Resets the timer
        }
    }
}
