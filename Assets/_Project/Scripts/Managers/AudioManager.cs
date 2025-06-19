using System;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    public AudioSource machinegunShootAS;
    public AudioSource shotgunShootAS;
    public AudioSource coinCollectedAS;
    public AudioSource getHitAS;
    public AudioSource zombieGrowlAS;
    public AudioSource explosionAS;

    public void PlayMachinegunShootSFX()
    {
        machinegunShootAS.Play();
    }
    public void PlayCoinCollectedSFX()
    {
        coinCollectedAS.Play();
    }
    public void PlayGetHitSFX()
    {
        getHitAS.Play();
    }
    public void PlayZomibeGrowlSFX()
    {
        zombieGrowlAS.Play();
    }

    public void PlayShotgunShootSFX()
    {
        shotgunShootAS.Play();
    }

    public void PlayExplosionSFX()
    {
        explosionAS.Play();
    }
}
