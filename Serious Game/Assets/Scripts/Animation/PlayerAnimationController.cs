using System.Collections;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public PlayerManager playerManager;
    private AppLogger _logger;
    private Animator _animator;

    public int secondsUntilAfk = 5;
    private bool _isAfk = false;
    private bool _isIdleCoroutineRunning = false;
    private bool _isDashing = false;

    // Coroutine reference
    private Coroutine idleTimerCoroutine;

    void Start()
    {
        _logger = AppLogger.Instance;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_isDashing)
        {
            return;
        }

        if (playerManager != null && !playerManager.canMove)
        {
            _animator.SetBool("IsIdle", true);
            return;
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(moveX, moveY);

        if (movement != Vector2.zero)
        {
            _isAfk = false;
            if (_isIdleCoroutineRunning)
            {
                // Properly stop the running coroutine
                if (idleTimerCoroutine != null)
                {
                    StopCoroutine(idleTimerCoroutine);
                    idleTimerCoroutine = null;
                }

                _isIdleCoroutineRunning = false;
            }

            _animator.SetFloat("VelocityX", moveX);
            _animator.SetFloat("VelocityY", moveY);
            _animator.SetBool("IsAfk", false);
            _animator.SetBool("IsIdle", false);
        }
        else
        {
            _animator.SetBool("IsIdle", true);

            if (!_isIdleCoroutineRunning && !_isAfk)
            {
                idleTimerCoroutine = StartCoroutine(IdleTimerCoroutine());
            }
        }
    }

    private IEnumerator IdleTimerCoroutine()
    {
        _isIdleCoroutineRunning = true;

        yield return new WaitForSeconds(secondsUntilAfk);
        PlayAfkAnimation();

        playerManager.SetPlayerAfk();

        _isAfk = true;
        _isIdleCoroutineRunning = false;
        idleTimerCoroutine = null;
    }

    public void PlayAfkAnimation()
    {
        _animator.SetBool("IsAfk", true);
        _animator.SetFloat("VelocityX", 0f);
        _animator.SetFloat("VelocityY", -1f);
    }

    public void PlayHurtAnimation()
    {
        _animator.Play("Hurt");
    }

    public void PlayHitAnimation()
    {
        _animator.Play("Hit");
    }

    public void SetDashing(bool isDashing)
    {
        _isDashing = isDashing;
        _animator.SetBool("IsIdle", !isDashing);
        _isAfk = !isDashing ? false : _isAfk;
        _animator.SetBool("IsAfk", _isAfk);
    }
}
