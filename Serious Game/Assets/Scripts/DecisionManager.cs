using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DecisionManager : GenericSingleton<DecisionManager>, IInteractableBehaviour
{
    // Data
    private List<Scenario> _scenarios;

    private Scenario _currentScenario;
    private Decision _leftDecision;
    private Decision _rightDecision;

    // UI
    public GameObject EnterKeypressHintUI;
    public GameObject DecisionsPanelsUI;
    public GameObject ScenarioPanelUI;
    public GameObject FirstDecisionPanel;
    public GameObject SecondDecisionPanel;

    public PlayerManager playerManager;

    private bool _canMakeDecision = false;
    private bool _isReadingDecisions = false;
    private AppLogger _logger;

    void Start()
    {
        if (EnterKeypressHintUI != null) EnterKeypressHintUI.SetActive(false);
        if (DecisionsPanelsUI != null) DecisionsPanelsUI.SetActive(false);

        _logger = AppLogger.Instance;

        _scenarios = LoadScenariosAndDecisions();
        SetNewScenarioAndDecisions();
    }

    public void ShowPressEnterButtonUI()
    {
        if (_isReadingDecisions)
        {
            return;
        }

        EnterKeypressHintUI.SetActive(true);
        playerManager.SetInteractableBehaviour(this);
        _canMakeDecision = true;
    }

    public void HidePressEnterButtonUI()
    {
        EnterKeypressHintUI.SetActive(false);
        _canMakeDecision = false;
    }

    public void ShowScenarioAndDecisions()
    {
        DecisionsPanelsUI.SetActive(true);
        _isReadingDecisions = true;
        HidePressEnterButtonUI();
    }

    public void HideScenarioAndDecisions()
    {
        DecisionsPanelsUI.SetActive(false);
        _isReadingDecisions = false;
    }

    public void Interact()
    {
        if (!_canMakeDecision)
        {
            return;
        }

        ShowScenarioAndDecisions();
    }

    public void ChooseLeftDecision()
    {
        RecordDecision(_leftDecision);
    }

    public void ChooseRightDecision()
    {
        RecordDecision(_rightDecision);
    }

    private void RecordDecision(Decision decision)
    {
        var filePath = "Assets/Data/PlayerScenarios.json";
        var scenarioRecordsWrapper = new ScenarioRecordsWrapper();

        if (File.Exists(filePath))
        {
            var jsonData = File.ReadAllText(filePath);
            scenarioRecordsWrapper = JsonUtility.FromJson<ScenarioRecordsWrapper>(jsonData);
            if (scenarioRecordsWrapper == null)
            {
                scenarioRecordsWrapper = new ScenarioRecordsWrapper();
            }
        }

        var isCorrectDecision = _currentScenario.CorrectDecision == decision;
        scenarioRecordsWrapper.ScenarioRecords.Add(new ScenarioRecord
        {
            Title = _currentScenario.Title,
            Description = _currentScenario.Description,
            ChosenDecision = decision,
            PlayerHasChosenCorrectly = isCorrectDecision,
            ReasonWhy = isCorrectDecision ? _currentScenario.ReasonWhyPlayerChoseCorrectly : _currentScenario.ReasonWhyPlayerChoseIncorrectly
        });

        var newJsonData = JsonUtility.ToJson(scenarioRecordsWrapper);
        File.WriteAllText(filePath, newJsonData);
    }

    private List<ScenarioRecord> GetPlayerScenarioRecords()
    {
        var decisionsFilePath = "Assets/Data/PlayerScenarios.json";

        if (File.Exists(decisionsFilePath))
        {
            var jsonData = File.ReadAllText(decisionsFilePath);
            var scenarioRecords = JsonUtility.FromJson<ScenarioRecordsWrapper>(jsonData) ?? new ScenarioRecordsWrapper();

            return scenarioRecords.ScenarioRecords;
        }

        return new List<ScenarioRecord>();
    }

    private List<Scenario> LoadScenariosAndDecisions()
    {
        var completedTitles = new HashSet<string>();
        var scenarioRecords = GetPlayerScenarioRecords();
        foreach (var record in scenarioRecords)
        {
            completedTitles.Add(record.Title);
        }

        var filePath = "Assets/Data/ScenariosDecisions.csv";
        var lines = File.ReadAllLines(filePath);
        var scenariosBuffer = new List<Scenario>();

        for (int i = 1; i < lines.Length; i++)
        {
            var values = lines[i].Split(';');
            var scenario = new Scenario()
            {
                Title = values[0],
                Description = values[1],
                CorrectDecision = new Decision() { Title = values[2], Description = values[3] },
                IncorrectDecision = new Decision() { Title = values[4], Description = values[5] },
                IsCompleted = completedTitles.Contains(values[0])
            };

            scenariosBuffer.Add(scenario);
        }

        return scenariosBuffer;
    }

    private void SetNewScenarioAndDecisions()
    {
        if (ScenarioPanelUI == null || FirstDecisionPanel == null || SecondDecisionPanel == null)
        {
            return;
        }
        
        List<Scenario> availableScenarios = _scenarios.Where(scenario => !scenario.IsCompleted).ToList();

        if (availableScenarios.Count == 0)
        {
            // Todo: End game
            _logger.LogError("No more scenarios to show", this);
            return;
        }

        int randomIndex = Random.Range(0, availableScenarios.Count);
        _currentScenario = availableScenarios[randomIndex];

        bool leftDecisionIsRight = Random.Range(0, 2) == 0;
        _leftDecision = leftDecisionIsRight ? _currentScenario.CorrectDecision : _currentScenario.IncorrectDecision;
        _rightDecision = leftDecisionIsRight ? _currentScenario.IncorrectDecision : _currentScenario.CorrectDecision;

        ScenarioPanelUI.transform.Find("Scenario Title").GetComponent<TMPro.TextMeshProUGUI>().text = _currentScenario.Title;
        ScenarioPanelUI.transform.Find("Scenario Description").GetComponent<TMPro.TextMeshProUGUI>().text = _currentScenario.Description;

        FirstDecisionPanel.transform.Find("Decision Title").GetComponent<TMPro.TextMeshProUGUI>().text = _leftDecision.Title;
        FirstDecisionPanel.transform.Find("Decision Description").GetComponent<TMPro.TextMeshProUGUI>().text = _leftDecision.Description;

        SecondDecisionPanel.transform.Find("Decision Title").GetComponent<TMPro.TextMeshProUGUI>().text = _rightDecision.Title;
        SecondDecisionPanel.transform.Find("Decision Description").GetComponent<TMPro.TextMeshProUGUI>().text = _rightDecision.Description;
    }

}
