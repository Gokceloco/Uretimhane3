using DG.Tweening;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform shootStartTransform;

    public Bullet bulletPrefab;

    public float attackRate;

    private float _attackTimer;

    public ParticleSystem muzzlePS;
    public Light weaponShootLight;

    public ParticleSystem shellPS;

    private void Update()
    {
        if (GameDirector.instance.gameState != GameState.GamePlay)
        {
            return;
        }
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
        GameDirector.instance.audioManager.PlayMachinegunShootSFX();

        weaponShootLight.DOKill();
        weaponShootLight.intensity = 0;
        weaponShootLight.DOIntensity(50,.1f).SetLoops(2, LoopType.Yoyo);
        muzzlePS.Play();

        GameDirector.instance.cameraHolder.ShakeCamera(.2f,.2f);
        shellPS.Play();
    }
}
