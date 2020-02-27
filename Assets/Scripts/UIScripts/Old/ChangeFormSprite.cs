using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeFormSprite : PlayerShoot
{

    // Use this for initialization
    public Image myImage;
    public Sprite Tank;
    public Sprite Sanic;
    public Sprite gunna;
    public float ChangeFormDelay = 5f;


    void Start()
    {
        myImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1") && Time.time >= formTimeStamp)
        {
            myImage.sprite = gunna;
            formTimeStamp = Time.time + formDelay;
        }
        if (Input.GetKeyDown("2") && Time.time >= formTimeStamp)
        {
            myImage.sprite = Tank;
            formTimeStamp = Time.time + formDelay;
        }
    }
}


