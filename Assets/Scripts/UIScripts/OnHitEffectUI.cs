using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class OnHitEffectUI : MonoBehaviour {
    private Image OnHitFlash;
    private Stopwatch sw;


	// Use this for initialization
	void Start () {
        sw = new Stopwatch();
        OnHitFlash = GetComponent<Image>();
        OnHitFlash.GetComponent<CanvasRenderer>().SetAlpha(0f);
        transform.position = new Vector3(Screen.width/2, Screen.height/2, 0f);
	}
	
	// Update is called once per frame
	void Update () {
        if (sw.ElapsedMilliseconds > 200)
        {
            sw.Reset();
            FlashOut();
        }
    }

    public void FlashIn()
    {
        sw.Start();
        OnHitFlash.CrossFadeAlpha(0.8f, 0.2f, false);

    }

    private void FlashOut()
    {
        OnHitFlash.CrossFadeAlpha(0f, 0.2f, false);
    }
}
