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
            decisionManager.ShowPressEnterButtonUI();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            decisionManager.HidePressEnterButtonUI();
            decisionManager.HideDecisions();
        }
    }
}
