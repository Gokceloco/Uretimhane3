using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    public AudioSource machinegunShootAS;
    public AudioSource coinCollectedAS;
    public AudioSource getHitAS;
    public AudioSource zombieGrowlAS;

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
}
