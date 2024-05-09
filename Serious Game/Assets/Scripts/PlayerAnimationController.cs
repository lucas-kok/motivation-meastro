using System.Collections;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private AppLogger _logger;
    private Animator animator;

    public int secondsUntilAfk = 5;
    public bool isAfk = false;
    public bool isIdleCoroutineRunning = false;

    // Coroutine reference
    private Coroutine idleTimerCoroutine;

    void Start()
    {
        _logger = AppLogger.Instance;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(moveX, moveY);

        if (movement != Vector2.zero)
        {
            isAfk = false;
            if (isIdleCoroutineRunning)
            {
                // Properly stop the running coroutine
                if (idleTimerCoroutine != null)
                {
                    StopCoroutine(idleTimerCoroutine);
                    idleTimerCoroutine = null;
                }
                
                isIdleCoroutineRunning = false;
            }

            animator.SetFloat("VelocityX", moveX);
            animator.SetFloat("VelocityY", moveY);
            animator.SetBool("IsAfk", false);
            animator.SetBool("IsIdle", false);
        }
        else
        {
            animator.SetBool("IsIdle", true);

            if (!isIdleCoroutineRunning && !isAfk)
            {
                idleTimerCoroutine = StartCoroutine(IdleTimerCoroutine());
            }
        }
    }

    private IEnumerator IdleTimerCoroutine()
    {
        isIdleCoroutineRunning = true;

        yield return new WaitForSeconds(secondsUntilAfk);
        PlayAfkAnimation();

        isAfk = true;
        isIdleCoroutineRunning = false;
        idleTimerCoroutine = null;
    }

    public void PlayAfkAnimation()
    {
        animator.SetBool("IsAfk", true);
    }

    public void PlayHurtAnimation()
    {
        animator.Play("Hurt");
    }

    public void PlayHitAnimation()
    {
        animator.Play("Hit");
    }
}
