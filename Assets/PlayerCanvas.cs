using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    public Player player;
    public GameObject optionsMenu;
    public TextMeshProUGUI text;
    public TextMeshProUGUI tradeText;
    public Image hungerBar;
    public GameObject tradePanel;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        hungerBar.fillAmount = (float)player.currentHunger / (float)player.maxHunger;
    }

    public void CurrentEmotion(string emotion)
    {
        player.SetEmotion(emotion);
    }

    public void CurrentEmote(string emote)
    {
        player.SetEmote(emote);
    }

    public void DisplayCost(string wood, string stone)
    {
        text.text = "Press F to Constuct building for Wood: " + wood + " Stone: " + stone;
        text.gameObject.SetActive(true);      
    }

    public void TradeOption(string currentItem)
    {
        text.text = "Press F to request a trade for your: " + currentItem;
        text.gameObject.SetActive(true);
    }

    public void TradeRequest(string theirOffer, string playerItem)
    {
        tradePanel.SetActive(true);
        tradeText.text = "The other player offers: " + theirOffer +  " for your: " + player.pickup.name;
    }

    public void Decline()
    {
        tradePanel.SetActive(false);
    }

    public void Accept()
    {
        tradePanel.SetActive(false);
        player.TradeAccepted();
    }
}