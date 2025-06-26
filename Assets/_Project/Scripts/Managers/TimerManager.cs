using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public GameDirector gameDirector;
    private float _levelStartTime;
    private float _currentLevelTimeLimit;

    public TimerUI timerUI;

    public void LevelStarted(float curLevelTime)
    {
        _currentLevelTimeLimit = curLevelTime;
        _levelStartTime = Time.time;
    }

    private void Update()
    {
        if (gameDirector.gameState == GameState.GamePlay)
        {
            if (Time.time - _levelStartTime > _currentLevelTimeLimit)
            {
                gameDirector.TimeIsUp();
            }
            var elapsedTime = Time.time - _levelStartTime;
            var remainingTime = _currentLevelTimeLimit - elapsedTime;
            timerUI.UpdateTimer(remainingTime / _currentLevelTimeLimit, Mathf.CeilToInt(remainingTime));
        }
    }
}
