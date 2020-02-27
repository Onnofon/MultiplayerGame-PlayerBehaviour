using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIforHealthSpeedAmmo : MonoBehaviour {

    public GameObject thisPlayer;
    public Text health;
    public Text speed;
    public Text ammo;
    public Text kills;
    public Text deaths;
    public Image speedBar;
    public Image healthBar;
    public Text respawn;
    public Text grenades;
    PlayerShoot psScript;
    Player pScript;
    characterController cScript;
    private float currentHealth;
    public int respawnTimer = 3;
    public Image BlackScreen;
	// Use this for initialization
	void Start () {
        BlackScreen.enabled = false;
	}
	
	void Update () {
        if (thisPlayer != null) // Prevents errors in lobby
        {
            psScript = thisPlayer.GetComponent<PlayerShoot>();
            pScript = thisPlayer.GetComponent<Player>();
            cScript = thisPlayer.GetComponent<characterController>();

            currentHealth = pScript.getHealth;
            if (psScript.bulletform)
            {
                ammo.text = ("  Ammo: " + psScript.ammoAR + " / " + psScript.maxAmmoAR); // Display ammo when in attack form
            }

            healthBar.fillAmount = currentHealth / 100; // Healthbar
            speedBar.fillAmount = cScript.speed / 12.5f; // Speedbar
            kills.text = "Kills: " + cScript.kills;
            deaths.text = "Deaths: " + cScript.deaths;
            grenades.text = psScript.grenades + "/" + "2"; // Display amount of grenades

            if (currentHealth <= 0)
            {
                StartCoroutine(Die());
            }
            if (currentHealth > 0)
            {
                BlackScreen.enabled = false;
                respawn.gameObject.SetActive(false);
                respawnTimer = 3;
            }
        }        
    }
    IEnumerator Die() 
    {
        respawn.gameObject.SetActive(true);
        BlackScreen.enabled = true; // Activate blackscreen
        respawn.text = "Respawning in: " + respawnTimer; // Initiate respawning text + Countdown
        yield return new WaitForSeconds(1);
        respawnTimer = 2;
        respawn.text = "Respawning in: " + respawnTimer;
        yield return new WaitForSeconds(1);
        respawnTimer = 1;
        respawn.text = "Respawning in: " + respawnTimer;
        yield return new WaitForSeconds(1);
        respawn.text = "";
        respawn.gameObject.SetActive(false);
    }
}
