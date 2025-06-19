using System;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public EnemyDetector enemyDetector;
    public int damage;
    public float throwForce;
    public float upForce;
    public float torqueForce;

    private Rigidbody _rb;
    private Player _player;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public void StartGrenade()
    {
        _player = GameDirector.instance.player;
        _rb.AddForce(_player.transform.forward * throwForce + Vector3.up * upForce);
        _rb.AddTorque(transform.right * torqueForce);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") 
            || collision.gameObject.CompareTag("Ground") 
            || collision.gameObject.CompareTag("Door"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        GameDirector.instance.fXManager.PlayGrenadeExplosionFX(transform.position);

        foreach (var e in enemyDetector.enemiesInRange)
        {
            e.GetHit(damage);
        }

        Destroy(gameObject);
    }
}
