using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DecisionManager : MonoBehaviour, IInteractableBehaviour
{
    // Managers
    public PlayerManager playerManager;

    // Singletons
    private AppLogger _logger;
    private GameState _gameState;

    // Data 
    private Scenario _scenario;

    // UI View data 
    private Decision _leftDecision;
    private Decision _rightDecision;

    // UI objects
    public GameObject enterKeypressHintUI;
    public GameObject decisionsPanelsUI;
    public GameObject scenarioPanelUI;
    public GameObject firstDecisionPanel;
    public GameObject secondDecisionPanel;

    // UI control state
    private bool _canMakeDecision = false;
    private bool _isReadingDecisions = false;

    void Start()
    {
        if (enterKeypressHintUI != null) enterKeypressHintUI.SetActive(false);
        if (decisionsPanelsUI != null) decisionsPanelsUI.SetActive(false);

        _logger = AppLogger.Instance;
        _gameState = GameState.Instance;

        // Get a Scenario to "manage" from the GameState
        _scenario = _gameState.GetRandomAvailableScenario();

        // Populate UI 
        PopulateDecisionsUI(); 
    }

    // Populate scenario and decision panels
    private void PopulateDecisionsUI()
    {
        if (scenarioPanelUI == null || firstDecisionPanel == null || secondDecisionPanel == null)
        {
            return;
        }

        bool leftDecisionShouldBeCorrect = Random.Range(0, 2) == 0;
        _leftDecision = leftDecisionShouldBeCorrect ? _scenario.CorrectDecision : _scenario.IncorrectDecision;
        _rightDecision = leftDecisionShouldBeCorrect ? _scenario.IncorrectDecision : _scenario.CorrectDecision;

        scenarioPanelUI.transform.Find("Scenario Title").GetComponent<TMPro.TextMeshProUGUI>().text = _scenario.Title;
        scenarioPanelUI.transform.Find("Scenario Description").GetComponent<TMPro.TextMeshProUGUI>().text = _scenario.Description;

        firstDecisionPanel.transform.Find("Decision Title").GetComponent<TMPro.TextMeshProUGUI>().text = _leftDecision.Title;
        firstDecisionPanel.transform.Find("Decision Description").GetComponent<TMPro.TextMeshProUGUI>().text = _leftDecision.Description;

        secondDecisionPanel.transform.Find("Decision Title").GetComponent<TMPro.TextMeshProUGUI>().text = _rightDecision.Title;
        secondDecisionPanel.transform.Find("Decision Description").GetComponent<TMPro.TextMeshProUGUI>().text = _rightDecision.Description;
    }


    // Hide and show UI methods
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

    public void ShowScenarioAndDecisions()
    {
        decisionsPanelsUI.SetActive(true);
        _isReadingDecisions = true;
        HidePressEnterButtonUI();
    }

    public void HideScenarioAndDecisions()
    {
        decisionsPanelsUI.SetActive(false);
        _isReadingDecisions = false;
    }

    // IInteractableBehaviour implementation. Showing the scenario in this case
    public void Interact()
    {
        if (!_canMakeDecision)
        {
            return;
        }

        ShowScenarioAndDecisions();
    }

    // Methods when a door is chosen
    public void ChooseLeftDecision()
    {
        RecordDecision(_leftDecision);
    }

    public void ChooseRightDecision()
    {
        RecordDecision(_rightDecision);
    }

    // Record the decision the player made and let the GameState process this
    private void RecordDecision(Decision decision) => _scenario.SetPlayerChosenDecision(decision);
}
