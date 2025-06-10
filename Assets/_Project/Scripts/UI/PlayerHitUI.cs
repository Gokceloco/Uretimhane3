using DG.Tweening;
using UnityEditor.Media;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHitUI : MonoBehaviour
{
    public Color startColor;
    public Color endColor;
    private CanvasGroup _canvasGroup;

    public Image damageImage;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(1, .1f).OnComplete(()=>Hide(.1f));
        damageImage.DOColor(endColor, .1f).SetDelay(.1f);
    }

    public void Hide(float delay)
    {
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(0, .2f).SetDelay(delay).OnComplete(SetHidden);
    }    

    private void SetHidden()
    {
        damageImage.color = startColor;
        gameObject.SetActive(false);
    }

    public void PopPlayerHitUI()
    {
        Show();        
    }
}
