using System.Linq;
using UnityEngine;

public class StatusSceneManager : MonoBehaviour
{
    private GameState _gameState;
    private Scenario _scenario;

    public GameObject leftChoiseIcon;
    public GameObject rightChoiseIcon;

    public GameObject AutonomyHappy;
    public GameObject AutonomyAngry;
    public GameObject AutonomyNeutral;

    public GameObject ConnectednessHappy;
    public GameObject ConnectednessAngry;
    public GameObject ConnectednessNeutral;

    public GameObject CompetenceHappy;
    public GameObject CompetenceAngry;
    public GameObject CompetenceNeutral;

    public GameManager gameManager;
    public InputManager inputManager;

    void Start()
    {
        _gameState = GameState.Instance;
        _scenario = _gameState.CurrentScenario;

        SetChoiceIcon();
        SetAutonomy();
        SetCompetence();
        SetConnectedness();
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
        if (key == KeyCode.Return)
        {
            gameManager.OnExitStatusScene();
        }
    }

    private void SetChoiceIcon()
    {
        var scenarioAndDoor = _gameState.ScenariosAndChosenDoorIndex.Where(s => s.scenario.Equals(_scenario)).FirstOrDefault();

        var chosenDoorIndex = scenarioAndDoor.doorIndex; // 0 when no scenario could be found

        leftChoiseIcon.SetActive(chosenDoorIndex == 0);
        rightChoiseIcon.SetActive(chosenDoorIndex == 1);
    }

    private void SetAutonomy()
    {
        AutonomyAngry.SetActive(_scenario.AutonomyScore == 0 || !_scenario.ChoseCorrectly);
        AutonomyHappy.SetActive(_scenario.AutonomyScore == 1 && _scenario.ChoseCorrectly);
        AutonomyNeutral.SetActive(_scenario.AutonomyScore == 0.5 && _scenario.ChoseCorrectly);
    }
    private void SetCompetence()
    {
        CompetenceAngry.SetActive(_scenario.CompetencyScore == 0 || !_scenario.ChoseCorrectly);
        CompetenceHappy.SetActive(_scenario.CompetencyScore == 1 && _scenario.ChoseCorrectly);
        CompetenceNeutral.SetActive(_scenario.CompetencyScore == 0.5 && _scenario.ChoseCorrectly);
    }
    private void SetConnectedness()
    {
        ConnectednessAngry.SetActive(_scenario.ConnectednessScore == 0 || !_scenario.ChoseCorrectly);
        ConnectednessHappy.SetActive(_scenario.ConnectednessScore == 1 && _scenario.ChoseCorrectly);
        ConnectednessNeutral.SetActive(_scenario.ConnectednessScore == 0.5 && _scenario.ChoseCorrectly);
    }
}
