using System.Collections;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private AppLogger _logger;
    private Animator animator;
    
    public int secondsUntilAfk = 5;
    private bool isAfk = false;
    private bool isIdleCoroutineRunning = false;

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

        // Check for movement
        if (movement != Vector2.zero)
        {
            StopCoroutine(IdleTimerCoroutine());
            isIdleCoroutineRunning = false;
            isAfk = false;

            animator.SetFloat("VelocityX", moveX);
            animator.SetFloat("VelocityY", moveY);

            animator.ResetTrigger("OpenBook");
            animator.SetBool("IsIdle", false);
        }
        else
        {
            animator.SetBool("IsIdle", true);

            if (!isIdleCoroutineRunning && !isAfk)
            {
                StartCoroutine(IdleTimerCoroutine());
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
    }

    public void PlayAfkAnimation()
    {
        animator.SetTrigger("OpenBook");
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
