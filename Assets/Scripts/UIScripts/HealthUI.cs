using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

    // Use this for initialization
    public Text text;
    private GameObject player1;
    private GameObject player2;
    private int currentHealth;
    private int changeHealth;

    void Start () {
        text = GetComponent<Text>();
        text.text = "";
    }
	
	// Update is called once per frame
	void Update () {
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");

        if (player1 != null && player2 != null)
        {
            text.text = "Player 1 HP: " + player1.GetComponent<Player>().getHealth + "\nPlayer 2 HP: " + player2.GetComponent<Player>().getHealth;
        }	

        if (player1 == null && player2 != null)
        {
            text.text = "\nPlayer 2 HP: " + player2.GetComponent<Player>().getHealth;
        }

        if (player1 != null && player2 == null)
        {
            text.text = "Player 1 HP: " + player1.GetComponent<Player>().getHealth;
        }
	}
}
