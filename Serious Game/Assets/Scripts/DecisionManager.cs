using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DecisionManager : GenericSingleton<DecisionManager>, IInteractableBehaviour
{
    // Data
    public List<Scenario> scenarios;
    private int _currentScenario = 0; 

    // UI
    public GameObject enterKeypressHintUI;
    public GameObject decisionsPanelsUI;
    public GameObject firstDecisionPanel;
    public GameObject secondDecisionPanel;

    public PlayerManager playerManager;

    private bool _canMakeDecision = false;
    private bool _isReadingDecisions = false;
    private AppLogger _logger;

    void Start()
    {
        enterKeypressHintUI.SetActive(false);
        decisionsPanelsUI.SetActive(false);

        _logger = AppLogger.Instance;

        scenarios = LoadScenariosAndDecisions();
    }

    public void ShowPressEnterButtonUI()
    {
        if (_isReadingDecisions)
        {
            return;
        }

        enterKeypressHintUI.SetActive(true);
        playerManager.SetInteractableBehaviour(this);
        _canMakeDecision = true;
    }

    public void HidePressEnterButtonUI()
    {
        enterKeypressHintUI.SetActive(false);
        _canMakeDecision = false;
    }

    public void ShowDecisions()
    {
        decisionsPanelsUI.SetActive(true);
        _isReadingDecisions = true;
        HidePressEnterButtonUI();

        // temp 
        ShowNextScenario();
    }

    public void HideDecisions()
    {
        decisionsPanelsUI.SetActive(false);
        _isReadingDecisions = false;
    }

    public void ChooseFirstDecision()
    {

    }

    public void ChooseSecondDecision()
    {

    }

    public void Interact()
    {
        if (!_canMakeDecision)
        {
            return;
        }

        ShowDecisions();
    }

    private void SaveScenarioWithDecision()
    {

    }

    private List<Scenario> LoadScenariosAndDecisions()
    {
        string filePath = "Assets/Data/ScenariosDecisions.csv";
        string[] lines = File.ReadAllLines(filePath);

        var scenariosBuffer = new List<Scenario>();

        //Parse SCV, load scenarios and decisions
        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(';');
            var scenario = new Scenario() { title = values[0], description = values[1] };
            scenario.CorrectDecision = new Decision() { title = values[2], description = values[3] };
            scenario.IncorrectDecision = new Decision() { title = values[4], description = values[5] };

            scenariosBuffer.Add(scenario);

        }

        return scenariosBuffer;
    }

    private void ShowNextScenario()
    {
        if (_currentScenario >= scenarios.Count)
        {
            _logger.LogError("No more scenarios to show", this);
            return;
        }
        
        var scenario = scenarios[_currentScenario];

        bool leftDecisionIsRight = Random.Range(0, 2) == 0;

        var firstDecisionPanelResult = leftDecisionIsRight ? scenario.CorrectDecision : scenario.IncorrectDecision;
        var secondDecisionPanelResult = leftDecisionIsRight ? scenario.IncorrectDecision : scenario.CorrectDecision;

        // Set Panel object (view) data
        firstDecisionPanel.transform.Find("Decision Title").GetComponent<TMPro.TextMeshProUGUI>().text = firstDecisionPanelResult.title;
        firstDecisionPanel.transform.Find("Decision Description").GetComponent<TMPro.TextMeshProUGUI>().text = firstDecisionPanelResult.description;

        secondDecisionPanel.transform.Find("Decision Title").GetComponent<TMPro.TextMeshProUGUI>().text = secondDecisionPanelResult.title;
        secondDecisionPanel.transform.Find("Decision Description").GetComponent<TMPro.TextMeshProUGUI>().text = secondDecisionPanelResult.description;

        _currentScenario++;
    }
}
