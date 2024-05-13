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
        _logger = AppLogger.Instance;
        _coroutineUtility = CoroutineUtility.Instance;
        _gameState = GameState.Instance;

        if (SceneManager.GetActiveScene().name.Equals(SceneType.MAIN_MENU_SCENE))
        {
            _gameState.GameIsActive = false;
            menuManager.OpenMenu(MenuType.MAIN_MENU);
        }
        else
        {
            _gameState.GameIsActive = true;
        }

        PlayLevelLoadingAnimation();
    }
    public void StartGame()
    {
        menuManager.CloseMenu();

        // We start the game with a decision room and initialize a clean gamestate  
        _gameState.Initialize();
        SceneManager.LoadScene(SceneType.DECISION_ROOM_SCENE.GetSceneName());
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

    // When the players survives a challenge room
    public void OnReachChallengeRoomExitDoor()
    {
        _gameState.IncrementPlayedChallengeRoomCount();

        GoToNextRoom(_gameState.NextRoomShouldBeFinalRoom() ? SceneType.FINAL_ROOM_SCENE : SceneType.CHALLENGE_ROOM_SCENE);
    }

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

    private void GoToNextRoom(SceneType roomType)
    {
        if (!_gameState.GameIsActive) return;

        SceneManager.LoadScene(roomType.GetSceneName());
    }

    public async void StartNextScene(string sceneName)
    {
        if (levelLoadingAnimationController != null && playerManager != null && _coroutineUtility != null)
        {
            playerManager.SetCanMove(false);
            await _coroutineUtility.RunCoroutineAndWait(levelLoadingAnimationController, "PlayExitLevelAnimation");
            SceneManager.LoadScene(sceneName);
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

    //public void PauseGame()
    //{
    //    if (!_gameIsActive) return;
    //    _gameIsActive = false;

    //    menuManager.OpenMenu(CheckIsMainMenuScene());
    //}


    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleMenu()
    {
        if (menuManager == null)
        {
            Debug.Log("You didn't start the game from the MainMenuScene.");
            Debug.LogError("Start from the MainMenuScene if you want to have a menu later on...");
            return;
        }

        //if (_gameIsActive)
        //{
        //    PauseGame();
        //}
        //else
        //{
        //    ResumeGame();
        //}
    }
}
