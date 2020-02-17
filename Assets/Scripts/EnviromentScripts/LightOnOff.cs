using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LightOnOff : NetworkBehaviour {
    public float minTime;
    public float maxTime;
    public float timeLeft;
    bool onOff = false;

    private void Start()
    {
        gameObject.GetComponent<Light>().enabled = onOff;
    }

    void Update()
    {

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            if (onOff == false)
            {
                onOff = !onOff;
                timeLeft = Random.Range(minTime, maxTime);
            }
            else
            {
                onOff = !onOff;
                gameObject.GetComponent<Light>().enabled = onOff;
                timeLeft = Random.Range(minTime, maxTime);
            }
        }
    }
}
