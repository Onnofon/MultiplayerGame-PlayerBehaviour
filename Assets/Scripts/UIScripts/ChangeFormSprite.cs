using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeFormSprite : PlayerShoot {

    // Use this for initialization
    public Image myImage;
    public Sprite Tank;
    public Sprite Sanic;
    public Sprite gunna;
    public float ChangeFormDelay = 5f;


    void Start () {
		myImage = GetComponent<Image>();
    } 

    // Update is called once per frame
    //void Update()
    //{
    //        if (Input.GetKeyDown("1") && Time.time >= timeStamp2)
    //        {
    //            myImage.sprite = gunna;
    //            timeStamp2 = Time.time + formDelay;
    //    }
    //    //    if (Input.GetKeyDown("2") && Time.time >= timeStamp2)
    //    //    {
    //    //        myImage.sprite = Tank;
    //    //        timeStamp2 = Time.time + formDelay;
    //    //}
    //    //    if (Input.GetKeyDown("3") && Time.time >= timeStamp2)
    //    //    {
    //    //        myImage.sprite = Sanic;
    //    //        timeStamp2 = Time.time + formDelay;
    //    //}
    //    }
    }


