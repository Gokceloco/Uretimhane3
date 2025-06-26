using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    public Image fillbar;
    public TextMeshProUGUI timeTMP;
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
        _canvasGroup.DOFade(0, .2f).OnComplete(() => gameObject.SetActive(false));
    }

    public void UpdateTimer(float time, int remainingTime)
    {
        fillbar.fillAmount = time;
        var min = remainingTime / 60;
        var sec = remainingTime % 60;
        if (min < 10 && sec >= 10)
        {
            timeTMP.text = "0" + min + ":" + sec;
        }
        else if (min < 10 && sec < 10)
        {
            timeTMP.text = "0" + min + ":" + "0" + sec;
        }
        else if (min >= 10 && sec < 10)
        {
            timeTMP.text = min + ":" + "0" + sec;
        }
        else
        {
            timeTMP.text = min + ":" + sec;
        }
    }
}
