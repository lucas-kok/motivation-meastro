using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingleton<GameManager>
{
    public MenuManager menuManager;

    // States
    private bool gameIsActive;

    private void Start()
    {
        if (CheckIsMainMenuScene() is true)
        {
            gameIsActive = false;
            menuManager.OpenMainMenu();  
        } else
        {
            _gameIsActive = true;
        }
    }

    public void GoToMainMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
        gameIsActive = false;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("TestScenePlayerMovement");
        gameIsActive = true;
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

    private bool CheckIsMainMenuScene() => gameIsActive = SceneManager.GetActiveScene().name == "MainMenuScene";
}
