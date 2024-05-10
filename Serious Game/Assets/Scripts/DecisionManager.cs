using UnityEngine;

public class DecisionManager : MonoBehaviour, IInteractableBehaviour
{
    public GameObject enterKeypressHintUI;
    public GameObject decisionsUI;
    public PlayerManager playerManager;

    private bool _canMakeDecision = false;
    private bool _isReadingDecisions = false;
    private AppLogger _logger;

    void Start()
    {
        enterKeypressHintUI.SetActive(false);
        decisionsUI.SetActive(false);

        _logger = AppLogger.Instance;        
    }

    private void Update()
    {
        
    }

    public void ShowPressEnterButtonUI()
    {
        if (_isReadingDecisions)
        {
            return;
        }

        enterKeypressHintUI.SetActive(true);
        _logger.LogInfo("Showing press enter", this);
        playerManager.SetInteractableBehaviour(this);
        _canMakeDecision = true;
    }

    public void HidePressEnterButtonUI()
    {
        enterKeypressHintUI.SetActive(false);
        _logger.LogInfo("Hiding press enter", this);
        _canMakeDecision = false;
    }

    public void ShowDecisions()
    {
        decisionsUI.SetActive(true);
        _isReadingDecisions = true;
        HidePressEnterButtonUI();
    }

    public void HideDecisions()
    {
        decisionsUI.SetActive(false);
        _isReadingDecisions = false;
    }

    public void Interact()
    {
        if (!_canMakeDecision)
        {
            return;
        }

        ShowDecisions();
    }
}
