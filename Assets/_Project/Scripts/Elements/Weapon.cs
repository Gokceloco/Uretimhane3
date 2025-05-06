using System;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform shootStartTransform;

    public Bullet bulletPrefab;

    public float attackRate;

    private float _attackTimer;

    private void Update()
    {
        if (Input.GetMouseButton(0) && _attackTimer > attackRate)
        {
            Shoot();
        }

        if (_attackTimer < attackRate + 1) 
        {         
            _attackTimer += Time.deltaTime;
        }
    }

    private void Shoot()
    {
        var newBullet = Instantiate(bulletPrefab);
        var newBulletTransform = newBullet.transform;
        newBulletTransform.position = shootStartTransform.position;
        newBulletTransform.LookAt(newBulletTransform.position + shootStartTransform.forward);
        newBullet.StartBullet(this);
        _attackTimer = 0;
    }
}
