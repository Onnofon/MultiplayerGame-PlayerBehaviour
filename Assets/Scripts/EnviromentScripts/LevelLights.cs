using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelLights : MonoBehaviour
{
    public float minTime;
    public float maxTime;
    public float timeLeft;
    bool onOff = false;

    private void Start()
    {
        gameObject.GetComponent<Light>().enabled = onOff; //Set the staring state of the Light
    }

    void Update()
    {
        timeLeft -= Time.deltaTime; //Reduce the amount with the amount of time that has passed
        if (timeLeft <= 0)
        {
            //If onOff is false then the Light will be switched ON and the timer reset
            if (onOff == false)
            {
                onOff = !onOff;
                gameObject.GetComponent<Light>().enabled = onOff;
                timeLeft = minTime;
            }
            //If onOff is true then the Light will be switched OFF and the timer reset
            else
            {
                onOff = !onOff;
                gameObject.GetComponent<Light>().enabled = onOff;
                timeLeft = maxTime;
            }
        }
    }
}
