using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public MenuManager menuManager;

    private void Start()
    {
        // If its the start scene, welcome the user instantly with the main menu
        if (SceneManager.GetActiveScene().name == "StartScene")
        {
            menuManager.OpenMainMenu();  
        }
    }
    
    // Here needs to be managed what happens when starting the game 
    // This is can for example be called in the main menu (via an event on the MenuManager) 
    public void StartGame()
    {
        SceneManager.LoadScene("TemplateScene");
    }   

    // Same here, but for resuming the game
    public void ResumeGame()
    {
        Debug.Log("Resuming game"); 
    }

    // Same here, but for quitting the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
