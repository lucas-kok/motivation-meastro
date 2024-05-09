using UnityEngine;
using UnityEngine.UI;

public class MenuManager : GenericSingleton<MenuManager>
{
    public Canvas ui;
    public GameObject menu;
    public Button startButton;
    public Button resumeButton; 

    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(ui);
    }

    public void OpenMenu(bool isMainMenu)
    {
        if (isMainMenu)
        {
            startButton.gameObject.SetActive(true); 
            resumeButton.gameObject.SetActive(false);
        } else
        {
            resumeButton.gameObject.SetActive(true);
            startButton.gameObject.SetActive(false);
        }

        menu.SetActive(true);
    }

    public void CloseMenu() => menu.SetActive(false);
}
