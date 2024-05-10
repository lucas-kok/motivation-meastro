using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingleton<GameManager>
{
    public MenuManager menuManager;
    public LevelLoadingManager levelLoadingManager; // Link this object in the scene to get level animations

    // States
    private bool _gameIsActive;

    private void Start()
    {
        if (CheckIsMainMenuScene() is true)
        {
            _gameIsActive = false;
            menuManager.OpenMenu(true);  
        } else
        {
            _gameIsActive = true;
        }

        if (levelLoadingManager != null)
        {
            levelLoadingManager.Initialize();
            levelLoadingManager.PlayLoadLevelAnimation();
        }
    }

    public void GoToMainMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
        _gameIsActive = false;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("DecisionRoomScene");
        _gameIsActive = true;

        menuManager.CloseMenu();
    }

    

    public void PauseGame()
    {
        if (!_gameIsActive) return;
        _gameIsActive = false;

        menuManager.OpenMenu(CheckIsMainMenuScene());
    }

    public void ResumeGame()
    {
        if (_gameIsActive) return;
        _gameIsActive = true;

        menuManager.CloseMenu();
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleMenu()
    {
        if (menuManager == null)
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
