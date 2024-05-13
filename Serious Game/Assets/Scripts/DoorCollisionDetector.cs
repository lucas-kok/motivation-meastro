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
        if (col.gameObject.tag == "Player")
        {
            if (gameObject.CompareTag("ExitChallengeRoomDoor"))
            {
                gameManager.OnReachChallengeRoomExitDoor();
            }
            else if (gameObject.CompareTag("Door1"))
            {
                decisionManager.ChooseLeftDecision();
                gameManager.OnReachDecisionRoomExitDoor();

            }
            else if (gameObject.CompareTag("Door2"))
            {
                decisionManager.ChooseRightDecision();
                gameManager.OnReachDecisionRoomExitDoor();
            }
        }
    }
}
