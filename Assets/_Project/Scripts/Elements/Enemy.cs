using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public EnemyState enemyState;
    public int startHealth;

    private int _currentHealth;

    private NavMeshAgent _navAgent;

    private Player _player;
    private Transform _transform;

    public float attackRate;
    private float _attackTimer;
    private HealthBar _healthBar;

    private void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        _transform = transform;
        _healthBar = GetComponentInChildren<HealthBar>();
    }

    public void Start()
    {
        _player = GameDirector.instance.player;
        _currentHealth = startHealth;
        _navAgent.isStopped = true;
    }

    private void Update()
    {
        var distanceToPlayer = Vector3.Distance(_player.transform.position, _transform.position);
        if (distanceToPlayer < 3.5)
        {
            enemyState = EnemyState.Attacking;
            _navAgent.isStopped = true;

            if (_attackTimer < attackRate + 1)
            {
                _attackTimer += Time.deltaTime;
            }

            if (_attackTimer > attackRate) 
            {
                Attack();
            }
        }
        else if (distanceToPlayer < 13)
        {
            StartWalking();
        }

        if (enemyState == EnemyState.Walking)
        {
            _navAgent.SetDestination(_player.transform.position);
        }
    }

    private void Attack()
    {
        print("In Attack");
        _attackTimer = 0;
        Invoke(nameof(StartWalking), 2);
    }

    void StartWalking()
    {
        enemyState = EnemyState.Walking;
        _navAgent.isStopped = false;
    }

    public void GetHit(int damage)
    {
        _currentHealth -= damage;
        _healthBar.UpdateHealthBar((float)_currentHealth / startHealth);
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