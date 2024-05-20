using UnityEngine;

public class DoorEntryTrigger : MonoBehaviour
{
    private AppLogger _logger;
    private AudioState _audioState;

    public DecisionManager decisionManager;
    public GameManager gameManager;
    private DoorBehaviour _doorBehaviour;

    private void Start()
    {
        _logger = AppLogger.Instance;
        _audioState = AudioState.Instance;

        _doorBehaviour = GetComponentInParent<DoorBehaviour>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && !_doorBehaviour.isLocked)
        {
            _audioState.Play("open-door");

            if (gameObject.CompareTag("ExitChallengeRoomDoor"))
            {
                gameManager.OnReachChallengeRoomExitDoor();
            }
            else if (gameObject.CompareTag("ExitTutorialRoomDoor"))
            {
                gameManager.OnReachTutorialRoomExitDoor();
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
        } else
        {
            _audioState.Play("locked-door");
        }
    }
}
