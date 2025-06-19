using DG.Tweening;
using System;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    public AudioManager audioManager;
    public ParticleSystem coinCollectedPS;
    public ParticleSystem serumCollectedPS;
    public ParticleSystem bulletImpactPS;
    public ParticleSystem explosionPS;
    public Light lightPrefab;

    public void PlayCoinCollectedFX(Vector3 pos)
    {
        var newPS = Instantiate(coinCollectedPS);
        newPS.transform.position = pos;
        newPS.Play();
        audioManager.PlayCoinCollectedSFX();
    }
    public void PlaySerumCollectedFX(Vector3 pos)
    {
        var newPS = Instantiate(serumCollectedPS);
        newPS.transform.position = pos;
        newPS.Play();
    }
    public void PlayBulletImpactFX(Vector3 pos, Vector3 dir, Color color)
    {
        var newPS = Instantiate(bulletImpactPS);
        newPS.transform.position = pos;
        newPS.transform.LookAt(pos - dir);
        var main = newPS.main;
        main.startColor = color;
        newPS.Play();
    }

    public void PlayGrenadeExplosionFX(Vector3 position)
    {
        var newPS = Instantiate(explosionPS);
        newPS.transform.position = position;
        newPS.Play();

        var newLight = Instantiate(lightPrefab);
        newLight.transform.position = position;
        newLight.intensity = 0;
        newLight.DOIntensity(100, .25f).SetLoops(2, LoopType.Yoyo).OnComplete(()=>Destroy(newLight.gameObject)); 
        
        audioManager.PlayExplosionSFX();
        GameDirector.instance.cameraHolder.ShakeCamera(1f,1f);
    }
}
