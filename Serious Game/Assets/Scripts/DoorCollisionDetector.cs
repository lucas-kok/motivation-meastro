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
        if (col.gameObject.tag == "Player")
        {
            if (gameObject.CompareTag("ExitChallengeRoomDoor"))
            {
                GameManager.OnReachChallengeRoomExitDoor();
            }
            else if (gameObject.CompareTag("Door1"))
            {
                //DecisionManager?.ChooseLeftDecision();
                GameManager.OnReachDecisionRoomExitDoor();
            }
            else if (gameObject.CompareTag("Door2"))
            {
                //DecisionManager?.ChooseRightDecision();
                GameManager.OnReachDecisionRoomExitDoor();
            }

        }
    }
}
