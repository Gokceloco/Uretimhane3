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
    public TimerManager timerManager;
    public Player player;

    [Header("UI")]
    public MainMenu mainMenu;
    public PlayerHealthUI playerHealthUI;
    public PlayerHitUI playerHitUI;
    public MessageUI messageUI;
    public InventoryUI inventoryUI;
    public GreandeCoolDownUI greandeCoolDownUI;
    public VictoryUI victoryUI;
    public FailUI failUI;

    public CameraHolder cameraHolder;

    public GameState gameState;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameState = GameState.MainMenu;
        HideInGameUI();

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            mainMenu.Show();
            mainMenu.EnableResumeButton();
            mainMenu.startButtonTMP.text = "RESTART";
            gameState = GameState.MainMenu;
            HideInGameUI();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Time.timeScale = .25f;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LevelFailed(2);
        }
    }

    public void RestartLevel()
    {        
        ShowInGameUI();
        levelManager.RestartLevelManager();
        player.RestartPlayer();
        playerHealthUI.Show();
        Invoke(nameof(ChangeGameStateToGamePlay), .1f);
        timerManager.LevelStarted(levelManager.GetCurrentLevel().levelTimeLimit);
    }

    void ChangeGameStateToGamePlay()
    {
        gameState = GameState.GamePlay;
    }

    public void LoadNextLevel()
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
        gameState = GameState.VictoryUI;
        victoryUI.Show(.5f);
    }

    public void LevelFailed(float delay)
    {
        gameState = GameState.FailUI;
        failUI.Show(delay);
    }

    public void ShowInGameUI()
    {
        coinManager.coinUI.Show();
        inventoryUI.Show();
        greandeCoolDownUI.Show();
    }
    public void HideInGameUI()
    {
        coinManager.coinUI.Hide();
        inventoryUI.Hide();
        greandeCoolDownUI.Hide();
    }

    public void TimeIsUp()
    {
        player.TimeIsUp();
        LevelFailed(2);
        messageUI.Show("TIME IS UP!", 3);
    }
}

public enum GameState
{
    MainMenu,
    GamePlay,
    VictoryUI,
    FailUI,
}