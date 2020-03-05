using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    public Player player;
    public GameObject optionsMenu;
    public TextMeshProUGUI text;
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

    public void DisplayCost(string wood, string stone)
    {
        text.gameObject.SetActive(true);
        text.text = "Press F to Constuct building for Wood: " + wood + " Stone: " + stone;
    }
}
