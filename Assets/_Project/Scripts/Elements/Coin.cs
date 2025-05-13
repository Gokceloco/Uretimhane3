using DG.Tweening;
using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private CoinManager _coinManager;
    private void Start()
    {
        _coinManager = GameDirector.instance.coinManager;
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
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
