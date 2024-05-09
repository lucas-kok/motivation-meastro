using UnityEngine;

public class DoorAnimationController : MonoBehaviour
{
    public Animator Animator;
    private AppLogger _logger;

    private void Start()
    {
        _logger = AppLogger.Instance;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Animator.SetBool("OpenDoor", true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Animator.SetBool("OpenDoor", false);
        }
    }
}
