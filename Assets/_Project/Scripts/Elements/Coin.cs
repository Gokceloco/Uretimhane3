using DG.Tweening;
using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private CoinManager _coinManager;
    private FXManager _fxManager;
    private void Start()
    {
        _coinManager = GameDirector.instance.coinManager;
        _fxManager = GameDirector.instance.fXManager;
        transform.DORotate(Vector3.up * 360, 2, RotateMode.WorldAxisAdd).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collected();
        }
    }

    private void Collected()
    {
        _coinManager.CoinCollected();
        _fxManager.PlayCoinCollectedFX(transform.position);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
