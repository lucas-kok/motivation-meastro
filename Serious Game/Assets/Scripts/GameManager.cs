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

    // Singletons 
    private GameState _gameState;
    private CoroutineUtility _coroutineUtility;
    private AppLogger _logger;

    private void Start()
    {
        // Assign singletons
        _logger = AppLogger.Instance;
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

        // Dev warning 
        Debug.LogWarning("IF YOU DIDNT START FROM MAINMENUSCENE: don't count on the gameloop to be working: 'Start game' has to be used from the main menu to start fresh!");
    }

    // Start game from main menu means a fresh start of the game 
    public void StartGame()
    {
        menuManager.CloseMenu();

        // We start the game with a decision room and initialize a clean gamestate  
        _gameState.Initialize();
        StartNextScene(SceneType.DECISION_ROOM_SCENE);
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

        if (_gameState.NextRoomShouldBeChallengeRoom())
        {
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
        if (levelLoadingAnimationController != null && _coroutineUtility != null)
        {
            playerManager?.SetCanMove(false);

            if (sceneType is not SceneType.MAIN_MENU_SCENE)
            {
                await _coroutineUtility.RunCoroutineAndWait(levelLoadingAnimationController, "PlayExitLevelAnimation");
            }

            SceneManager.LoadScene(sceneType.GetSceneName());
        }
    }

    public async void RestartScene()
    {
        if (levelLoadingAnimationController != null && _coroutineUtility != null)
        {
            await _coroutineUtility.RunCoroutineAndWait(levelLoadingAnimationController, "PlayExitLevelAnimation");
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayLevelLoadingAnimation();
    }

    public async void PlayLevelLoadingAnimation()
    {
        if (levelLoadingAnimationController == null)
        {
            return;
        }

        levelLoadingAnimationController.Initialize();
        if (playerManager != null) playerManager.SetCanMove(false);
        await _coroutineUtility.RunCoroutineAndWait(levelLoadingAnimationController, "PlayLoadLevelAnimation");
        if (playerManager != null) playerManager.SetCanMove(true);
    }
}
