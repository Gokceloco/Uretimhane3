using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public static GameDirector instance;

    [Header("Manager")]
    public LevelManager levelManager;
    public CoinManager coinManager;
    public FXManager fXManager;
    public AudioManager audioManager;
    public Player player;

    [Header("UI")]
    public MainMenu mainMenu;
    public PlayerHealthUI playerHealthUI;
    public PlayerHitUI playerHitUI;


    public CameraHolder cameraHolder;

    public GameState gameState;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameState = GameState.MainMenu;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
            mainMenu.Hide();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            LoadPreviousLevel();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            playerHealthUI.Show();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            playerHealthUI.Hide();
        }
    }

    public void RestartLevel()
    {
        gameState = GameState.GamePlay;
        levelManager.RestartLevelManager();
        player.RestartPlayer();
        playerHealthUI.Show();
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

    public void LevelCompleted()
    {
        Invoke(nameof(LoadNextLevel), 1f);
    }

    public void Lose()
    {

    }
}

public enum GameState
{
    MainMenu,
    GamePlay,
    VictoryUI,
    FailUI,
}