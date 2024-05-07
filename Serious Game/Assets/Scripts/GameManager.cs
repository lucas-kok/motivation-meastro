using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // UI 
    public GameObject menuPrefab;

    private Canvas ui;
    private GameObject startMenuPanel;
    private GameObject gameMenuPanel;
    private GameObject menuInstance;

    // Menu screen 
    public GameObject OptionsController;

    /// <summary>
    /// We instantiate a MenuInstance that will always be reused. 
    /// A menu contains panels, that vary based on the scene 
    /// (e.g. a start menu panel and a game menu panel during active gameplay).
    /// </summary>
    private void Start()
    {
        ui = GameObject.Find("UI").GetComponent<Canvas>();
        menuInstance = Instantiate(menuPrefab, ui.transform);
        startMenuPanel = menuInstance.transform.Find("Start Menu Panel").gameObject;
        gameMenuPanel = menuInstance.transform.Find("Game Menu Panel").gameObject;

        menuInstance.SetActive(true);
        ToggleMenu();
    }

    public void ToggleMenu()
    {
        // The menu panel is different for the main menu (StartScene) 
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "StartScene")
        {
            startMenuPanel.SetActive(!startMenuPanel.activeSelf);
            gameMenuPanel.SetActive(false);
        }
        // If we're not in the start scene, we're in gameplay. Show a suitable panel. 
        else
        {
            gameMenuPanel.SetActive(!gameMenuPanel.activeSelf);
            startMenuPanel.SetActive(false);
        }
    }

    // Navigation methods
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TemplateScene");
    }

    public void GoToOptions()
    {
        menuInstance.SetActive(false); 
        Instantiate(OptionsController);
    }
}
