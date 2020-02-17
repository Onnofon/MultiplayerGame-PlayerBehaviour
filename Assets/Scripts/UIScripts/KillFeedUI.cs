using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class KillFeedUI : MonoBehaviour {
    public string player1;
    public string player2;
    public Text text;
    Stopwatch sw;
    public AudioSource twoKills;
    public AudioSource threeKills;
    public AudioSource fourKills;
    public AudioSource fiveKills;
    public AudioSource moreKills;
    public AudioSource[] audioSources;


	// Use this for initialization
	void Start () {
        sw = new Stopwatch();
        audioSources = GetComponents<AudioSource>();
        twoKills = audioSources[0];
        threeKills = audioSources[1];
        fourKills = audioSources[2];
        fiveKills = audioSources[3];
        moreKills = audioSources[4];
	}
	
	// Update is called once per frame
	void Update () {
        if (sw.ElapsedMilliseconds > 500)
        {
            text.CrossFadeAlpha(0, 3f, false);
            sw.Reset();
        }

    }

    public void showKill(string player1, string player2)
    {
        text.CrossFadeAlpha(1f, 0f, false);
        this.player1 = player1;
        this.player1 = player2;
        sw.Start();
        text.text = player1 + " has killed " + player2;

    }
}
