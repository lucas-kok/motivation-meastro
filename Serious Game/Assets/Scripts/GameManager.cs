using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]  public MenuManager menuManager;

    // States
    [SerializeField] private bool _gameIsActive = true;

    private void Start()
    {
        // If its the start scene, welcome the user instantly with the main menu
        if (SceneManager.GetActiveScene().name == "StartScene")
        {
            _gameIsActive = false;
            menuManager.OpenMainMenu();  
        } else
        {
            _gameIsActive = true;
        }
    }
    
    // Here needs to be managed what happens when starting the game 
    // This is can for example be called in the main menu (via an event on the MenuManager) 
    public void StartGame()
    {
        SceneManager.LoadScene("TestScenePlayerMovement");
        _gameIsActive = true;
    }

    private void PauseGame()
    {
        if (!_gameIsActive) return;
        _gameIsActive = false;

        menuManager.OpenInGameMenu();
    }

    private void ResumeGame()
    {
        if (_gameIsActive) return;
        _gameIsActive = true;

        menuManager.CloseInGameMenu();
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    // Helper method for client code
    public void TogglePauseResumeGame() { 

        if (_gameIsActive)
        {
            PauseGame();
        } else
        {
            ResumeGame();
        }
    }
}
