using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadingScript : MonoBehaviour {

    public Text rText;
    public GameObject player;
    PlayerShoot psScript;
	// Use this for initialization
	void Start () {
        rText.text = "";
	}

    // Update is called once per frame
    void Update() {

        if (player != null)
        {
            psScript = player.GetComponent<PlayerShoot>();
            if (psScript.reloading == true)
            {
                rText.text = "Reloading";
            }
            else rText.text = "";
        }
    }
}
