using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameDirector gameDirector;
    public int startHealth;
    private int _currentHealth;

    private PlayerNavigator _playerNavigator;

    private bool _isDead;

    private void Awake()
    {
        _playerNavigator = GetComponent<PlayerNavigator>();
    }

    internal void RestartPlayer()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        _isDead = false;
        _currentHealth = startHealth;
        gameDirector.playerHealthUI.UpdateHealth(1);
        gameObject.SetActive(true);
        _playerNavigator.ResetPosition();        
    }

    internal void GetHit()
    {
        if (_isDead)
        {
            return;
        }
        _currentHealth -= 1;
        if (_currentHealth <= 0)
        {
            Die();
        }
        gameDirector.audioManager.PlayGetHitSFX();
        gameDirector.cameraHolder.ShakeCamera(.5f, .5f);
        gameDirector.playerHealthUI.UpdateHealth((float)_currentHealth / startHealth);
        gameDirector.playerHitUI.PopPlayerHitUI();
    }

    private void Die()
    {
        _isDead = true;
        gameObject.SetActive(false);
    }
}
