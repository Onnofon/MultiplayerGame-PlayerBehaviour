﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerForm : NetworkBehaviour
{
    public GameObject tool;
    public BoxCollider toolCol;
    public GameObject axe;
    public GameObject pickaxe;
    public GameObject bag;

    private void Start()
    {
        tool = axe;
        pickaxe.gameObject.SetActive(false);
        bag.gameObject.SetActive(false);
        Woodcutter();
    }

    [Client]
    public void Woodcutter()
    {

        CmdWoodcutter();
    }

    [Command]
    private void CmdWoodcutter()
    {
        RpcWoodcutter();
    }

    [ClientRpc]
    private void RpcWoodcutter()
    {
        //axe.gameObject.SetActive(true);
        //pickaxe.gameObject.SetActive(false);
        //bag.gameObject.SetActive(false);
        tool = axe;
        toolCol = tool.gameObject.GetComponent<BoxCollider>();
    }

    [Client]
    public void Miner()
    {

        CmdMiner();
    }

    [Command]
    private void CmdMiner()
    {
        RpcMiner();
    }

    [ClientRpc]
    private void RpcMiner()
    {
        //axe.gameObject.SetActive(false);
        //pickaxe.gameObject.SetActive(true);
        //bag.gameObject.SetActive(false);
        tool = pickaxe;
        toolCol = tool.gameObject.GetComponent<BoxCollider>();
    }

    [Client]
    public void Gatherer()
    {

        CmdGatherer();
    }

    [Command]
    private void CmdGatherer()
    {
        RpcGatherer();
    }

    [ClientRpc]
    private void RpcGatherer()
    {
        //axe.gameObject.SetActive(false);
        //pickaxe.gameObject.SetActive(false);
        //bag.gameObject.SetActive(true);
        tool = bag;
        toolCol = tool.gameObject.GetComponent<BoxCollider>();
    }

    [Client]
    public void NoForm()
    {

        CmdNoform();
    }

    [Command]
    private void CmdNoform()
    {
        RpcNoForm();
    }

    [ClientRpc]
    private void RpcNoForm()
    {
        //axe.gameObject.SetActive(false);
        //pickaxe.gameObject.SetActive(false);
        //bag.gameObject.SetActive(false);
        tool = null;
    }
}
