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
        }
        else
        {
            gameIsActive = true;
        }
    }

    public void GoToMainMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
        gameIsActive = false;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("FirstScene");
        gameIsActive = true;
        menuManager.CloseMenu();
    }

    public void PauseGame()
    {
        if (!gameIsActive) return;
        gameIsActive = false;

        menuManager.OpenMenu(CheckIsMainMenuScene());
    }

    public void ResumeGame()
    {
        if (gameIsActive) return;
        gameIsActive = true;

        menuManager.CloseMenu();
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleMenu()
    {
        if (menuManager is null)
        { 
            Debug.Log("You didn't start the game from the MainMenuScene.");
            Debug.LogError("Start from the MainMenuScene if you want to have a menu later on...");
            return; 
        }

        if (gameIsActive)
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
