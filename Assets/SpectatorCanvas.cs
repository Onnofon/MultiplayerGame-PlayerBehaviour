using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpectatorCanvas : MonoBehaviour
{
   public GameObject messagePanel;
   public GameObject toolsPanel;
   public SpectatorControls spectator;
   public TMP_InputField input;
   public void CancelMessage()
    {
        messagePanel.SetActive(false);
    }

    public void OpenMessage()
    {
        messagePanel.SetActive(true);
        toolsPanel.SetActive(false);
    }

    public void SendMessage()
    {
        spectator.BroadCastMessage(input.text);
        messagePanel.SetActive(false);
    }

    public void OpenTools()
    {
        toolsPanel.SetActive(true);
    }
}
