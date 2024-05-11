using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingleton<GameManager>
{
    public MenuManager MenuManager;
    public LevelLoadingManager LevelLoadingManager; // Link this object in the scene to get level animations

    // States
    private bool _gameIsActive;
    private AppLogger _logger;

    private void Start()
    {
        if (CheckIsMainMenuScene() is true)
        {
            _gameIsActive = false;
            MenuManager.OpenMenu(true);  
        } else
        {
            _gameIsActive = true;
        }

        _logger = AppLogger.Instance;

        if (LevelLoadingManager != null)
        {
            LevelLoadingManager.Initialize();
            LevelLoadingManager.StartCoroutine("PlayLoadLevelAnimation");
        }
    }

    public void GoToMainMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
        _gameIsActive = false;
    }

    public void StartGame()
    {
        InitializePlayerDecisions();
        
        SceneManager.LoadScene("DecisionRoomScene");
        _gameIsActive = true;

        MenuManager.CloseMenu();
    }

    public void StartNextScene()
    {
        if (LevelLoadingManager != null)
        {
            LevelLoadingManager.StartCoroutine("PlayExitLevelAnimation");
        }
    }
    
    public void InitializePlayerDecisions()
    {
        var filePath = "Assets/Data/PlayerScenarios.json";
        var emptyJsonData = JsonUtility.ToJson(new List<ScenarioRecord>());
        File.WriteAllText(filePath, emptyJsonData);
    }

    public void PauseGame()
    {
        if (!_gameIsActive) return;
        _gameIsActive = false;

        MenuManager.OpenMenu(CheckIsMainMenuScene());
    }

    public void ResumeGame()
    {
        if (_gameIsActive) return;
        _gameIsActive = true;

        MenuManager.CloseMenu();
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleMenu()
    {
        if (MenuManager == null)
        { 
            Debug.Log("You didn't start the game from the MainMenuScene.");
            Debug.LogError("Start from the MainMenuScene if you want to have a menu later on...");
            return; 
        }

        if (_gameIsActive)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    private bool CheckIsMainMenuScene() => _gameIsActive = SceneManager.GetActiveScene().name == "MainMenuScene";
}
