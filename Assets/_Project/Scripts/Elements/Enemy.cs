using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Properties")]
    public EnemyState enemyState;
    public int startHealth;
    public float attackRate;
    public float attackRange;
    public float angleThreshold;

    [Header("Elements")]
    public Collider deadCollider;
    public List<Light> eyeLights;

    private Collider _aliveCollider;
    private int _currentHealth;
    private NavMeshAgent _navAgent;
    private Player _player;
    private Transform _transform;    
    private float _attackTimer;
    private HealthBar _healthBar;
    private Animator _animator;    
    private bool _isAttackInProgress;

    public float flashDuration;
    public Material flashMaterial;
    public Material originalMaterial1;
    public Material originalMaterial2;
    public List<Renderer> renderers1;
    public List<Renderer> renderers2;

    private Coroutine _getHitCoroutine;
    public float getHitStopDuration;

    private bool _isStopped;

    private void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        _transform = transform;
        _healthBar = GetComponentInChildren<HealthBar>();
        _animator = GetComponentInChildren<Animator>();
        _aliveCollider = GetComponent<Collider>();
    }

    public void Start()
    {
        _player = GameDirector.instance.player;
        _currentHealth = startHealth;
        _navAgent.isStopped = true;
        _healthBar.UpdateHealthBar(1);
    }

    private void Update()
    {
        if (enemyState == EnemyState.Dead || _isStopped)
        {
            return;
        }
        var distanceToPlayer = Vector3.Distance(_player.transform.position, _transform.position);        

        if (distanceToPlayer < attackRange)
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
        else if (distanceToPlayer < 20 && enemyState != EnemyState.Walking && !_isAttackInProgress)
        {
            StartWalking();
            _attackTimer = attackRate;
        }

        if (enemyState == EnemyState.Walking)
        {
            _navAgent.SetDestination(_player.transform.position);
        }
    }
    private void Attack()
    {
        _isAttackInProgress = true;
        _animator.SetTrigger("Attack");
        _attackTimer = 0;
    }
    internal void AttackCompleted()
    {
        _isAttackInProgress = false;
        var angle = Vector3.Angle(_transform.position - _player.transform.position, _transform.forward);
        var distance = Vector3.Distance(_transform.position, _player.transform.position);
        if (angle > angleThreshold && distance < attackRange)
        {
            _player.GetHit();
        }
    }
    void StartWalking()
    {
        enemyState = EnemyState.Walking;
        _animator.SetTrigger("Walk");
        _navAgent.isStopped = false;
        GameDirector.instance.audioManager.PlayZomibeGrowlSFX();
    }
    public void GetHit(int damage)
    {
        GameDirector.instance.audioManager.PlayGetHitSFX();
        _currentHealth -= damage;
        _healthBar.UpdateHealthBar((float)_currentHealth / startHealth);
        if (_currentHealth <= 0 && enemyState != EnemyState.Dead)
        {
            Die();
        }
        if (enemyState != EnemyState.Dead)
        {
            _getHitCoroutine = StartCoroutine(EnemyGetHitCoroutine());
        }
    }
    private void Die()
    {      
        _navAgent.isStopped = true;
        _navAgent.enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        Destroy(gameObject, 5);
        if (Random.value < .5f)
        {
            _animator.SetTrigger("FallBack1");
        }
        else
        {
            _animator.SetTrigger("FallBack2");
        }
        _transform.LookAt(_player.transform.position);
        enemyState = EnemyState.Dead;
        DisableAliveCollider();        
        Invoke(nameof(ExpireEnemy), 3);
        foreach (var l in eyeLights)
        {
            l.DOIntensity(0,1f);
        }
        if (_getHitCoroutine != null)
        {
            StopCoroutine(_getHitCoroutine);
        }
        foreach (var r in renderers1)
        {
            r.material = originalMaterial1;
        }
        foreach (var r in renderers2)
        {
            r.material = originalMaterial2;
        }
    }
    void ExpireEnemy()
    {
        var rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotation
            | RigidbodyConstraints.FreezePositionX
            | RigidbodyConstraints.FreezePositionZ;

        deadCollider.enabled = false;
    }
    private void DisableAliveCollider()
    {
        _aliveCollider.enabled = false;
        GetComponent<Rigidbody>().useGravity = true;
        deadCollider.enabled = true;
    }

    IEnumerator EnemyGetHitCoroutine()
    {
        foreach (var r in renderers1)
        {
            r.material = flashMaterial;
        }
        foreach (var r in renderers2)
        {
            r.material = flashMaterial;
        }
        if (_navAgent.enabled)
        {
            _navAgent.isStopped = true;
        }
        yield return new WaitForSeconds(flashDuration);
        foreach (var r in renderers1)
        {
            r.material = originalMaterial1;
        }
        foreach (var r in renderers2)
        {
            r.material = originalMaterial2;
        }
        yield return new WaitForSeconds(getHitStopDuration);
        if (_navAgent.enabled)
        {
            _navAgent.isStopped = false;
        }
    }

    public void StopEnemy()
    {
        _isStopped = true;
    }
}
public enum EnemyState
{
    Idle,
    Walking,
    Attacking,
    Dead,
}