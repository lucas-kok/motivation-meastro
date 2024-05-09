using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuManager : MonoBehaviour
{
    public Canvas UI;

    // MainMenu
    public GameObject MainMenuPrefab;
    private GameObject _mainMenuInstance;

    // InGameMenu
    public GameObject InGameMenuPrefab;
    private GameObject _inGameMenuInstance;

    // Click event types
    public UnityEvent OnStartGameEvent;
    public UnityEvent OnResumeGameEvent;
    public UnityEvent OnQuitGameEvent;
    
    // Open and close menus 
    public void OpenMainMenu()
    {
        _mainMenuInstance = Instantiate(MainMenuPrefab, UI.transform);
    }

    public void CloseMainMenu()
    {
        Destroy(_mainMenuInstance);
    }

    public void OpenInGameMenu()
    {
        _inGameMenuInstance = Instantiate(InGameMenuPrefab, UI.transform);
    }

    public void CloseInGameMenu()
    {
        Destroy(_inGameMenuInstance);
    }

    // Button click events
    public void OnStartGame()
    {
        OnStartGameEvent.Invoke();
    }

    public void OnResumeGame()
    {
        OnResumeGameEvent.Invoke();
    }

    public void OnQuitGame()
    {
        OnQuitGameEvent.Invoke();
    }

    public void OnSelectOptions()
    {
        Debug.Log("Go to options screen");
    } 
}
