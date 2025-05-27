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
        GameDirector.instance.audioManager.PlayGetHitSFX();
        gameObject.SetActive(false);
        GameDirector.instance.cameraHolder.ShakeCamera(.5f, .5f);
    }
}
