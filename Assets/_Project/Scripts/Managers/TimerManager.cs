using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public GameDirector gameDirector;
    private float _levelStartTime;
    private float _currentLevelTimeLimit;

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
            print(Time.time - _levelStartTime);
        }
    }
}
