using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    public Player player;
    public GameObject optionsMenu;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CurrentEmotion(string emotion)
    {
        player.SetEmotion(emotion);
    }
}
