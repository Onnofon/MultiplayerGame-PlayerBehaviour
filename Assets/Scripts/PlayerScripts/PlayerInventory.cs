using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInventory : NetworkBehaviour
{
    public List<string> items = new List<string>();
    public PlayerForm playerForm;
    public Transform holdItems;
    public Transform itemDrop;
    public GameObject currentHoldItem;
    public int currentSlot;
    public GameObject rock;
    public GameObject wood;
    public GameObject mushroom;
    private void Start()
    {
        items[0] = "none";
    }
    public void AddToIventory(string item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == "")
            {
                items[i] = item;
                break;
            }
        }
    }

    public void RemoveFromIventory(string item)
    {
        int count = items.Count;
        for (int i = 0; i < count; i++)
        {
            if (items[i] == item)
            {
                items.RemoveAt(i);
                return;
            }
        }
    }

    [Client]
    public void SetHoldItem(string newHoldItem)
    {
        CmdSetHoldItem(newHoldItem);
    }

    [Command]
    void CmdSetHoldItem(string newHoldItem)
    {
        RpcSetHoldItem(newHoldItem);
    }

    [ClientRpc]
    void RpcSetHoldItem(string newHoldItem)
    {
        foreach (Transform item in holdItems)
        {

            if (item.name == newHoldItem)
            {
                currentHoldItem.gameObject.SetActive(false);
                item.gameObject.SetActive(true);
                currentHoldItem = item.gameObject;

            }
        }
    }

    [Client]
    public void DropItem()
    {
        CmdDropItem();
    }

    [Command]
    void CmdDropItem()
    {
        RpcDropItem();
    }

    [ClientRpc]
    void RpcDropItem()
    {

        if (currentHoldItem.name == "Rock")
        {
            Debug.Log("Rock drop");
            var rockObject = (GameObject)Instantiate(rock, itemDrop.position, itemDrop.rotation);
            rockObject.name = "Rock";
        }
        else if (currentHoldItem.name == "Wood")
        {
            var woodObject = (GameObject)Instantiate(wood, itemDrop.position, itemDrop.rotation);
            woodObject.name = "Wood";
        }
        else if (currentHoldItem.name == "Mushroom")
        {
            var mushroomObject = (GameObject)Instantiate(mushroom, itemDrop.position, itemDrop.rotation);
            mushroomObject.name = "Mushroom";

        }
        items[currentSlot] = "";
        currentHoldItem.gameObject.SetActive(false);
        SetHoldItem(items[0]);
        currentSlot = 0;

    }
}
