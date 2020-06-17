using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingCrate : MonoBehaviour
{
    public TradingBoard tradingBoard;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PickUp")
        {
            if (tradingBoard.itemsInCrate < 5)
            {
                for (int i = 0; i < tradingBoard.items.Count; i++)
                {
                    if (tradingBoard.items[i] == "")
                    {
                        tradingBoard.itemsInCrate++;
                        tradingBoard.items[i] = other.name;
                        tradingBoard.tradeAccepted = false;
                        tradingBoard.otherBoard.tradeAccepted = false;
                        tradingBoard.accept.color = Color.white;
                        tradingBoard.decline.color = Color.white;
                        tradingBoard.otherBoard.acceptOther.color = Color.white;
                        tradingBoard.otherBoard.declineOther.color = Color.white;
                        Destroy(other.gameObject);
                        //tradingBoard.UpdateBoardInfo();
                        break;
                    }
                }
            }
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "PickUp")
    //    {
    //        tradingBoard.itemsInCrate--;
    //        tradingBoard.items.Remove(other.gameObject);       
    //        //tradingBoard.UpdateBoardInfo();
    //    }
    //}
}
