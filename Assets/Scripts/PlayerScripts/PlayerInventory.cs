using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInventory : NetworkBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    public PlayerForm playerForm;
    public Transform holdItems;
    public GameObject currentHoldItem;

    private void Start()
    {
        items[1] = playerForm.tool;
    }

    public void AddToIventory(GameObject item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i] == null)
            {
                items[i] = item;
                break;
            }
        }
    }

    public void RemoveFromIventory(GameObject item)
    {
        int count = items.Count;
        for (int i = 0; i < count; i++)
        {
            if (items[i].name == item.name)
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
}
