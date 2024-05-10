using UnityEngine;
using UnityEngine.UI;

public class MenuManager : GenericSingleton<MenuManager>
{
    public Canvas ui;
    public GameObject menu;

    public GameObject optionsPanel;
    public GameObject progressPanel;

    public Button startButton;
    public Button resumeButton;

    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(ui);
    }

    public void OpenMenu(bool isMainMenu)
    {
        startButton.gameObject.SetActive(isMainMenu);
        resumeButton.gameObject.SetActive(!isMainMenu);

        menu.SetActive(true);
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
    }

    public void CloseAllPanels()
    {
        optionsPanel.SetActive(false);
        progressPanel.SetActive(false);
    }

    public void OpenOptions()
    {
        CloseAllPanels();
        optionsPanel.SetActive(true);
    }

    public void CloseOptions() => optionsPanel.SetActive(false);

    public void OpenProgress()
    {
        CloseAllPanels();
        progressPanel.SetActive(true);
    }

    public void CloseProgress() => progressPanel.SetActive(false);
}
