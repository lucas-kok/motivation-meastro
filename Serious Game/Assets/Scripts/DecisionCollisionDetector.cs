using UnityEngine;

public class DecisionColliderDetector : MonoBehaviour
{
    public DecisionManager decisionManager;
    private AppLogger _logger;

    private void Start()
    {
        _logger = AppLogger.Instance;
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            _logger.LogInfo("Showing press enter", this);
            decisionManager.ShowPressEnterButtonUI();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            _logger.LogInfo("Hiding press enter", this);
            decisionManager.HidePressEnterButtonUI();
            decisionManager.HideDecisions();
        }
    }
}
