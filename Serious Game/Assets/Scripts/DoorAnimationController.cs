using UnityEngine;

public class DoorAnimationController : MonoBehaviour
{
    public Animator animator;
    private AppLogger _logger;

    private void Start()
    {
        _logger = AppLogger.Instance;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            animator.SetBool("OpenDoor", true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            animator.SetBool("OpenDoor", false);
        }
    }
}
