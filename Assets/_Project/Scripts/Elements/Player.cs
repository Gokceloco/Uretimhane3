using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerNavigator _playerNavigator;

    private void Awake()
    {
        _playerNavigator = GetComponent<PlayerNavigator>();
    }
    internal void RestartPlayer()
    {
        gameObject.SetActive(true);
        _playerNavigator.ResetPosition();        
    }

    internal void GetHit()
    {
        gameObject.SetActive(false);
    }
}
