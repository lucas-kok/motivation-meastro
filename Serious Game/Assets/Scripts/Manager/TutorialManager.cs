using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerManager playerManager;
    public InputManager inputManager;
   
    public GameObject exitDoorArrow;
    public GameObject[] tutorialStepsUI;
    public KeyCode[] tutorialKeys;

    private AppLogger _logger;
    private int _currentStep = 0;

    private void Start()
    {
        _logger = AppLogger.Instance;
        exitDoorArrow.SetActive(false);
        gameManager.LockAllDoors();

        if (tutorialStepsUI.Length != tutorialKeys.Length)
        {
            _logger.LogError("Tutorial steps and keys are not equal in length.", this);
            return;
        }
        
        if (tutorialStepsUI.Length > 0)
        {
            tutorialStepsUI[_currentStep].SetActive(true);
            for (int i = 1; i < tutorialStepsUI.Length; i++)
            {
                tutorialStepsUI[i].SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        inputManager.OnKeyPress += HandleKeyPress;
    }

    private void OnDisable()
    {
        inputManager.OnKeyPress -= HandleKeyPress;
    }

    private void HandleKeyPress(KeyCode key)
    {
        if (!playerManager.CanMove)
        {
            return;
        }
        
        if (key.ToString() == tutorialKeys[_currentStep].ToString())
        {
            HideCurrentStep();
            ShowNextStep();
        }
    }

    private void HideCurrentStep()
    {
        if (_currentStep < tutorialStepsUI.Length)
        {
            tutorialStepsUI[_currentStep].SetActive(false);
        }
    }

    private void ShowNextStep()
    {
        if (_currentStep < tutorialStepsUI.Length - 1)
        {
            _currentStep++;
            tutorialStepsUI[_currentStep].SetActive(true);
        }

        if (_currentStep == tutorialStepsUI.Length - 1)
        {
            exitDoorArrow.SetActive(true);
            gameManager.UnlockAllDoors();
        }
    }
}
