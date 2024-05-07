using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject menuPrefab;
    public Canvas UI;

    private GameObject startMenuPanel;
    private GameObject gameMenuPanel;
    private GameObject menuInstance;

    private void Start()
    {
        menuInstance = Instantiate(menuPrefab, UI.transform);
        startMenuPanel = menuInstance.transform.Find("Start Menu Panel").gameObject;
        gameMenuPanel = menuInstance.transform.Find("Game Menu Panel").gameObject;

        menuInstance.SetActive(true);
        ToggleMenu();
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TemplateScene");
    }

    public void ToggleMenu()
    {
        // Toggle menu visibility based on current scene
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "StartScene")
        {
            // Show start menu panel
            startMenuPanel.SetActive(!startMenuPanel.activeSelf);
            gameMenuPanel.SetActive(false);
        }
        else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "TemplateScene")
        {
            // Show game menu panel
            gameMenuPanel.SetActive(!gameMenuPanel.activeSelf);
            startMenuPanel.SetActive(false);
        }
    }
}
