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
    public MessageUI messageUI;
    public InventoryUI inventoryUI;
    public GreandeCoolDownUI greandeCoolDownUI;


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
    }

    public void RestartLevel()
    {
        gameState = GameState.GamePlay;
        ShowInGameUI();
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
}

public enum GameState
{
    MainMenu,
    GamePlay,
    VictoryUI,
    FailUI,
}