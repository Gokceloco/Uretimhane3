using System;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int coinCount;
    public CoinUI coinUI;

    public void ResetCoinManager()
    {
        coinCount = PlayerPrefs.GetInt("CoinCount");
        coinUI.SetCoinCount(coinCount);
    }
    
    public void CoinCollected()
    {
        coinCount++;
        UpdateCoinCountUI();
        PlayerPrefs.SetInt("CoinCount", coinCount);
    }

    public void UpdateCoinCountUI()
    {
        coinUI.SetCoinCount(coinCount);
    }
}
