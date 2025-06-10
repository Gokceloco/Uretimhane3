using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameDirector gameDirector;
    private CanvasGroup _canvasGroup;

    public Button resumeButton;
    public TextMeshProUGUI startButtonTMP;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(1, .2f).SetUpdate(true);
    }

    public void Hide()
    {
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(0, .2f).OnComplete(() => gameObject.SetActive(false));
    }

    public void StartButtonPressed()
    {
        Time.timeScale = 1;
        GameDirector.instance.RestartLevel();
        Hide();
    }

    public void ResumeButtonPressed()
    {
        Time.timeScale = 1;
        Hide();
        gameDirector.gameState = GameState.GamePlay;
        gameDirector.ShowInGameUI();
    }
    public void QuitButtonPressed()
    {
        Application.Quit();
    }

    public void EnableResumeButton()
    {
        resumeButton.interactable = true;
    }
}
