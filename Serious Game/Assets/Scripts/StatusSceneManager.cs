using UnityEngine;
using UnityEngine.SceneManagement;

public class StatusSceneManager : MonoBehaviour
{
    private GameState _gameState;
    private Scenario _scenario;

    public GameObject AutonomyHappy;
    public GameObject AutonomyAngry;
    public GameObject AutonomyNeutral;

    public GameObject ConnectednessHappy;
    public GameObject ConnectednessAngry;
    public GameObject ConnectednessNeutral;

    public GameObject CompetenceHappy;
    public GameObject CompetenceAngry;
    public GameObject CompetenceNeutral;

    void Start()
    {
        _gameState = GameState.Instance;
        _scenario = _gameState.GetCurrentScenario();

        SetAutonomy();
        SetCompetence();
        SetConnectedness();
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
