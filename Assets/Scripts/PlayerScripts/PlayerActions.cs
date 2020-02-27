using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public Player player;
    public PlayerMovement playerMov;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.E))
        {
            player.canvas.optionsMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            player.canvas.optionsMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
