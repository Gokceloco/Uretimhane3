using DG.Tweening;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    public TextMeshProUGUI coinTMP;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(1, .2f);
    }

    public void Hide()
    {
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(0, .2f).OnComplete(() => gameObject.SetActive(false)).SetUpdate(true);
    }

    public void SetCoinCount(int count)
    {
        coinTMP.text = count.ToString();

        coinTMP.transform.DOKill();
        coinTMP.transform.localScale = Vector3.one;
        coinTMP.transform.DOScale(1.3f, .1f).SetLoops(2, LoopType.Yoyo);
    }
}
