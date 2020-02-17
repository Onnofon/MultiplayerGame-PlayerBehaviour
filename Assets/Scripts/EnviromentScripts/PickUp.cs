using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{

    public Text pickUpText;
    public GameObject player;
    Player psScript;
    characterController characterScript;
    // Use this for initialization
    void Start()
    {
        pickUpText.text = "";
    }

    // Update is called once per frame
    void Update()
    {

        if (player != null)
        {
            psScript = player.GetComponent<Player>();
            if (psScript.pickUpHealth == true)
            {
                pickUpText.text = "Pick Up Health ";
                StartCoroutine(pickUp());
            }
            else
                if (psScript.pickUpGrenade == true)
            {
                pickUpText.text = "Pick Up Grenade ";
                StartCoroutine(pickUp());
            }
            else
                if (psScript.pickUpSpeed == true)
            {
                pickUpText.text = "Pick Up Speed ";
                StartCoroutine(pickUp());
            }
            else
            {
                pickUpText.text = " ";
            }
        }
    }

    IEnumerator pickUp()
    {
        yield return new WaitForSeconds(2f);
        if (psScript.pickUpHealth == true)
        {
            psScript.pickUpHealth = false;
        }
        yield return new WaitForSeconds(2f);
        if (psScript.pickUpGrenade == true)
        {
            psScript.pickUpGrenade = false;
        }
        yield return new WaitForSeconds(2f);
        if (psScript.pickUpSpeed == true)
        {
            psScript.pickUpSpeed = false;
        }
    }

}