using UnityEngine;

public class DoorEntryTrigger : MonoBehaviour
{
    public DecisionManager DecisionManager;
    public GameManager GameManager;
    private AppLogger _logger;

    private void Start()
    {
        _logger = AppLogger.Instance;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && DecisionManager != null)
        {
            if (gameObject.tag == "Door1")
            {
                DecisionManager.ChooseLeftDecision();
            }
            else if (gameObject.tag == "Door2")
            {
                DecisionManager.ChooseRightDecision();
            }

            if (GameManager != null)
            {
                GameManager.StartNextScene();
            }
        }
    }
}
