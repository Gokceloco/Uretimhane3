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
        _playerNavigator.ResetPosition();
    }
}
