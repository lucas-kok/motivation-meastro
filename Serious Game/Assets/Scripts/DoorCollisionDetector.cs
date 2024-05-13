using UnityEngine;

public class DoorEntryTrigger : MonoBehaviour
{
    public DecisionManager decisionManager;
    public GameManager gameManager;
    private AppLogger _logger;

    private void Start()
    {
        _logger = AppLogger.Instance;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && decisionManager != null)
        {
            if (gameObject.tag == "Door1")
            {
                decisionManager.ChooseLeftDecision();
            }
            else if (gameObject.tag == "Door2")
            {
                decisionManager.ChooseRightDecision();
            }

            if (gameManager != null)
            {
                gameManager.StartNextScene();
            }
        }
    }
}
