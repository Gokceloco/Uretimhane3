using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyState enemyState;
    public int startHealth;

    private int _currentHealth;    

    public void Start()
    {
        _currentHealth = startHealth;
    }

    public void GetHit(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth < 0 && enemyState != EnemyState.Dead)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        enemyState = EnemyState.Dead;
    }
}
public enum EnemyState
{
    Idle,
    Walking,
    Attacking,
    Dead,
}