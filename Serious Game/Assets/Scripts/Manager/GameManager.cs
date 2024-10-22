using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The gamemanager is the central brain that receives actions and changes in the game, and offloads this to other managers, controllers and state classes.
/// </summary>
public class GameManager : MonoBehaviour
{
    // Managers 
    public MenuManager menuManager;
    public PlayerManager playerManager;

    // Controllers
    public LevelLoadingAnimationController levelLoadingAnimationController; // Link this object in the scene to get level animations
    private List<string> scenesToExcludeEnterLoadingAnimation = new List<string> {
        SceneType.MAIN_MENU_SCENE.GetSceneName(), 
        SceneType.BACKSTORY_SCENE.GetSceneName(),
        SceneType.IMPACT_SCENE.GetSceneName(),
    };
    private List<string> scenesToExcludeExitLoadingAnimation = new List<string> {
        SceneType.MAIN_MENU_SCENE.GetSceneName(),
        SceneType.BACKSTORY_SCENE.GetSceneName(),
        SceneType.IMPACT_SCENE.GetSceneName(),
    };

    // Singletons 
    private GameState _gameState;
    private CoroutineUtility _coroutineUtility;

    public GameObject[] doors;

    private void Start()
    {
        // Assign singletons
        _coroutineUtility = CoroutineUtility.Instance;
        _gameState = GameState.Instance;

        // If we are in the main menu scene, we should not start the game yet, instead open the main menu
        if (SceneManager.GetActiveScene().name.Equals(SceneType.MAIN_MENU_SCENE.GetSceneName()))
        {
            _gameState.GameIsActive = false;
            menuManager.OpenMenu(MenuType.MAIN_MENU);
        }
        else
        {
            _gameState.GameIsActive = true;

            // For every scene, always execute starting animation
            PlayLevelLoadingAnimation();
        }
    }

    // Start game from main menu means a fresh start of the game 
    public void StartGame()
    {
        menuManager.CloseMenu();

        // We start the game with a decision room and initialize a clean gamestate  
        _gameState.Initialize();
        StartNextScene(SceneType.BACKSTORY_SCENE);
    }

    public void StartTutorial()
    {
        menuManager.CloseMenu();
        StartNextScene(SceneType.TUTORIAL_SCENE);
    }

    public void PauseGame()
    {
        _gameState.Pause();
        menuManager.OpenMenu(MenuType.IN_GAME_MENU);
    }

    public void ResumeGame()
    {
        _gameState.Resume();
        menuManager.CloseMenu();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CloseGame()
    {
        _gameState.GameIsActive = false;
        StartNextScene(SceneType.MAIN_MENU_SCENE);
    }

    public void TogglePauseResume()
    {
        if (_gameState.GameIsActive)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    // When player exits backstory scene
    public void OnExitBackstory()
    {
        GoToNextRoom(SceneType.DECISION_ROOM_SCENE);
    }

    // When the players survives a challenge room
    public void OnReachChallengeRoomExitDoor()
    {
        _gameState.IncrementPlayedChallengeRoomCount();

        GoToNextRoom(_gameState.NextRoomShouldBeFinalRoom() ? SceneType.FINAL_ROOM_SCENE : SceneType.DECISION_ROOM_SCENE);
    }

    // When the player reaches the tutorial room exit door
    public void OnReachTutorialRoomExitDoor() => GoToNextRoom(SceneType.MAIN_MENU_SCENE);

    // When the player made a decision
    public void OnReachDecisionRoomExitDoor()
    {
        _gameState.IncrementPlayedDecisionRoomCount();
        GoToNextRoom(SceneType.IMPACT_SCENE);
    }

    // When the status scene gets exited
    public void OnExitImpactScene()
    {
        if (_gameState.NextRoomShouldBeChallengeRoom())
        {
            _gameState.UpdateGameDifficulty();
            GoToNextRoom(SceneType.CHALLENGE_ROOM_SCENE);
        }
        else
        {
            GoToNextRoom(SceneType.DECISION_ROOM_SCENE);
        }
    }

    private void GoToNextRoom(SceneType sceneType)
    {
        if (!_gameState.GameIsActive) return;

        StartNextScene(sceneType);
    }

    public async void StartNextScene(SceneType sceneType)
    {
        if (levelLoadingAnimationController != null && _coroutineUtility != null && !scenesToExcludeExitLoadingAnimation.Contains(SceneManager.GetActiveScene().name) && !sceneType.Equals(SceneType.MAIN_MENU_SCENE))
        {
            playerManager?.SetCanMove(false);

            await _coroutineUtility.RunCoroutineAndWait(levelLoadingAnimationController, () => levelLoadingAnimationController.PlayExitLevelAnimation());
        }

        if (sceneType == SceneType.MAIN_MENU_SCENE)
        {
            Time.timeScale = 1;
        }

        SceneManager.LoadScene(sceneType.GetSceneName());
    }

    public async void RestartScene()
    {
        if (levelLoadingAnimationController != null && _coroutineUtility != null)
        {
            await _coroutineUtility.RunCoroutineAndWait(levelLoadingAnimationController, () => levelLoadingAnimationController.PlayExitLevelAnimation());
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayLevelLoadingAnimation();
    }

    public async void PlayLevelLoadingAnimation()
    {
        if (levelLoadingAnimationController == null || scenesToExcludeEnterLoadingAnimation.Contains(SceneManager.GetActiveScene().name))
        {
            return;
        }

        levelLoadingAnimationController.Initialize();
        if (playerManager != null) playerManager.SetCanMove(false);
        await _coroutineUtility.RunCoroutineAndWait(levelLoadingAnimationController, () => levelLoadingAnimationController.PlayLoadLevelAnimation());
        if (playerManager != null) playerManager.SetCanMove(true);
    }

    public void LockAllDoors()
    {
        foreach (GameObject door in doors)
        {
            door.GetComponent<DoorBehaviour>().Lock();
        }
    }

    public void UnlockAllDoors()
    {
        foreach (GameObject door in doors)
        {
            door.GetComponent<DoorBehaviour>().Unlock();
        }
    }
}
