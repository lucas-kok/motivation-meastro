using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public MenuManager menuManager;
    public LevelLoadingAnimationController levelLoadingAnimationController; // Link this object in the scene to get level animations
    public PlayerManager playerManager;

    // Scenes
    private static readonly string MAINMENUSCENE = "MainMenuScene";
    private static readonly string CHALLENGEROOMSCENE = "ChallengeRoomScene";
    private static readonly string DECISIONROOMSCENE = "DecisionRoomScene";


    // States < TODO: REFACTOR
    private bool _gameIsActive;

    private GameState _gameState; 
    private CoroutineUtility _coroutineUtility;
    private AppLogger _logger;

    private void Start()
    {
        if (CheckIsMainMenuScene() is true)
        {
            _gameIsActive = false;
            menuManager.OpenMenu(true);  
        } 
        else
        {
            _gameIsActive = true;
        }

        _logger = AppLogger.Instance;
        _coroutineUtility = CoroutineUtility.Instance;
        _gameState = GameState.Instance;

        PlayLevelLoadingAnimation();
    }

    public void GoToMainMenuScene()
    {
        SceneManager.LoadScene(MAINMENUSCENE);
        _gameIsActive = false;
    }

    public void StartGame()
    {
        // From menu to active
        _gameIsActive = true;
        menuManager.CloseMenu();

        InitializePlayerDecisions();

        // First time challenge room for player 
        _gameState.RestartGameLoop();
        SceneManager.LoadScene(CHALLENGEROOMSCENE);
    }

    // When the players survives a challenge room
    public void OnReachChallengeRoomExitDoor()
    {
        GoToNextRoom(nextIsChallengeRoom: false);
    }

    // When the player made a decision
    public void OnReachDecisionRoomExitDoor()
    {
        GoToNextRoom(nextIsChallengeRoom: true);
    }

    private void GoToNextRoom(bool nextIsChallengeRoom)
    {
        if (_gameIsActive)
        {
            if (nextIsChallengeRoom)
            {
                _gameState.WentToNextDecisionRoom();
                StartNextScene(CHALLENGEROOMSCENE);
            }
            else
            {
                _gameState.WentToNextDecisionRoom();
                StartNextScene(DECISIONROOMSCENE);
            }
        }
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

    public void InitializePlayerDecisions()
    {
        var filePath = "Assets/Data/PlayerScenarios.json";
        var emptyJsonData = JsonUtility.ToJson(new List<ScenarioRecord>());
        File.WriteAllText(filePath, emptyJsonData);
    }

    public void PauseGame()
    {
        if (!_gameIsActive) return;
        _gameIsActive = false;

        menuManager.OpenMenu(CheckIsMainMenuScene());
    }

    public void ResumeGame()
    {
        if (_gameIsActive) return;
        _gameIsActive = true;

        menuManager.CloseMenu();
    }
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

        if (_gameIsActive)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    private bool CheckIsMainMenuScene() => _gameIsActive = SceneManager.GetActiveScene().name == MAINMENUSCENE;
}
