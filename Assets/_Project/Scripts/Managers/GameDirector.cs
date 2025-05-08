using System;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public static GameDirector instance;

    public LevelManager levelManager;
    public Player player;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        RestartLevel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            LoadPreviousLevel();
        }
    }

    private void RestartLevel()
    {
        levelManager.RestartLevelManager();
        player.RestartPlayer();
    }

    void LoadNextLevel()
    {
        if (levelManager.levelNo < levelManager.levels.Count)
        {
            levelManager.levelNo += 1;
        }        
        RestartLevel();
    }

    void LoadPreviousLevel()
    {
        if (levelManager.levelNo > 1)
        {
            levelManager.levelNo -= 1;
        }
        RestartLevel();
    }

    public void Win()
    {
        Invoke(nameof(LoadNextLevel), 1f);
    }

    public void Lose()
    {

    }
}
