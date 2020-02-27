using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class P_Controller : NetworkBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    void Start()
    {
        if (isLocalPlayer)
        {
            //canvas = Instantiate(canvas); //Adds canvas prefab
            //canvas.transform.position = new Vector3(0, 0, 0);
            player = this.gameObject; //Assign player with the object the script is attached to
            //canvas.GetComponent<ReloadingScript>().player = player;
            //canvas.GetComponent<UIforHealthSpeedAmmo>().thisPlayer = player;
            //StartCoroutine(waitChangeName()); //calls the object name change method

        }
    }


}

