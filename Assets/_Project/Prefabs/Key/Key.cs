using DG.Tweening;
using UnityEngine;

public class Key : MonoBehaviour
{
    private void Start()
    {
        transform.DOMoveY(1.5f, .6f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
