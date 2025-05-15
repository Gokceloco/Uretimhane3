using UnityEngine;

public class FXManager : MonoBehaviour
{
    public ParticleSystem coinCollectedPS;
    public ParticleSystem serumCollectedPS;
    public ParticleSystem bulletCollectedPS;

    public void PlayCoinCollectedFX(Vector3 pos)
    {
        var newPS = Instantiate(coinCollectedPS);
        newPS.transform.position = pos;
        newPS.Play();
    }
    public void PlaySerumCollectedFX(Vector3 pos)
    {
        var newPS = Instantiate(serumCollectedPS);
        newPS.transform.position = pos;
        newPS.Play();
    }
    public void PlayBulletImpactFX(Vector3 pos, Vector3 dir, Color color)
    {
        var newPS = Instantiate(bulletCollectedPS);
        newPS.transform.position = pos;
        newPS.transform.LookAt(pos - dir);
        var main = newPS.main;
        main.startColor = color;
        newPS.Play();
    }
}
