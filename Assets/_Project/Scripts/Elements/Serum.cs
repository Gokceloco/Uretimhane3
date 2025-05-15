using DG.Tweening;
using UnityEngine;

public class Serum : MonoBehaviour
{
    private void Start()
    {
        transform.DOMoveY(.5f, .5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
        transform.localScale = Vector3.zero;
        transform.DOScale(1, .2f).SetEase(Ease.OutBack);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameDirector.instance.LevelCompleted();
            GameDirector.instance.fXManager.PlaySerumCollectedFX(transform.position);
            gameObject.SetActive(false);
        }
    }
}
