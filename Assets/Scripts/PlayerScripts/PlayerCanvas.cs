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
    public TextMeshProUGUI specText;
    public Image hungerBar;
    public GameObject tradePanel;
    public List<TextMeshProUGUI> inventorySlots = new List<TextMeshProUGUI>();
    public bool incomingTrade;
    public string theirOffer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hungerBar.fillAmount = (float)player.currentHunger / (float)player.maxHunger;
        for (int i = 0; i < player.playerActions.playerInv.items.Count; i++)
        {

            if (player.playerActions.playerInv.items[i] == "")
            {
                inventorySlots[i].text = "empty";
            }
            else
            {
                inventorySlots[i].text = player.playerActions.playerInv.items[i];
            }
        }


    }

    public void BroadcastedMessage(string text)
    {
        specText.text = text;
        StartCoroutine(SpectatorMessage(1f));
    }

    IEnumerator SpectatorMessage(float time)
    {
        specText.color = new Color(specText.color.r, specText.color.g, specText.color.b, 0);
        while (specText.color.a < 1.0f)
        {
            specText.color = new Color(specText.color.r, specText.color.g, specText.color.b, specText.color.a + (Time.deltaTime / time));
            yield return null;
        }

        yield return new WaitForSeconds(7f);

        specText.color = new Color(specText.color.r, specText.color.g, specText.color.b, 1);
        while (specText.color.a > 0.0f)
        {
            specText.color = new Color(specText.color.r, specText.color.g, specText.color.b, specText.color.a - (Time.deltaTime / time));
            yield return null;
        }
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
        text.text = "Press T to request a trade for your: " + currentItem;
        text.gameObject.SetActive(true);
    }

    //public void TradeRequest()
    //{
    //    tradePanel.SetActive(true);
    //    tradeText.text = "The other player offers: " + theirOffer +  " for your: " + player.pickup.name;
    //}

    public void Decline()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        incomingTrade = false;
        player.pendingTradeOffer = false;
    }

    public void Accept()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        incomingTrade = false;
        player.TradeAccepted(theirOffer);
    }
}