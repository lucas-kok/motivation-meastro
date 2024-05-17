using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    // Dash states
    private bool canDash = true;
    private bool isDashing = false;
    private IInteractableBehaviour _interactableBehaviour;
    private AppLogger _logger;

    private bool _canMove = true;

    public Slider dashCooldownSlider;
    private Color _dashDisabledColor = new Color(1f, 0.2770096f, 0.2122642f, 1f);
    private Color _dashEnabledColor = new Color(0.2667724f, 1f, 0.2117647f, 1f);

    [SerializeField] public float speed;
    [SerializeField] public InputManager inputManager;
    [SerializeField] public PlayerAnimationController playerAnimationController;
    [SerializeField] public ParticleSystem dustParticle;
    [SerializeField] public float dashingPower = 0.075f;
    [SerializeField] public float dashingTime = 0.2f;
    [SerializeField] public float dashingCooldown = 1f;

    private void Start()
    {
        _logger = AppLogger.Instance;

        if (dashCooldownSlider != null) {
            dashCooldownSlider.value = 1;
            SetDashCooldownSliderColor(_dashEnabledColor);
        }

        rb = GetComponent<Rigidbody2D>();
        playerAnimationController = GetComponent<PlayerAnimationController>();
        rb.gravityScale = 0f;
    }

    public void Move(Vector2 movementInput)
    {
        if (!_canMove || isDashing)
        {
            return;
        }
        
        if (movementInput.x != 0 && movementInput.y != 0)
        {
            rb.velocity = new Vector2(movementInput.x * speed * Time.deltaTime * 0.707f, movementInput.y * speed * Time.deltaTime * 0.707f);
        } else
        {
            rb.velocity = new Vector2(movementInput.x * speed * Time.deltaTime, movementInput.y * speed * Time.deltaTime);
        }
    }

    public IEnumerator Dash(Vector2 movementInput)
    {
        // Check on dashing state
        if (!_canMove || isDashing || !canDash)
        {
            yield break;
        }

        StopCoroutine("PlayDashCooldownAnimation");

        canDash = false;
        isDashing = true;

        playerAnimationController.SetDashing(true);

        if (movementInput.x != 0 && movementInput.y != 0)
        {
            rb.velocity = new Vector2(movementInput.x * dashingPower * 0.707f, movementInput.y * dashingPower * 0.707f);
        } else
        {
            rb.velocity = new Vector2(movementInput.x * dashingPower, movementInput.y * dashingPower);
        }

        dustParticle.Play();
        SetDashCooldownSliderColor(_dashDisabledColor);
        dashCooldownSlider.value = 0;

        yield return new WaitForSeconds(dashingTime);
        isDashing = false;

        playerAnimationController.SetDashing(false);

        StartCoroutine("PlayDashCooldownAnimation");

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    public IEnumerator PlayDashCooldownAnimation()
    {
        StopCoroutine("PlayExitLevelAnimation");
        if (dashCooldownSlider == null) yield break;

        while (dashCooldownSlider.value < 1f)
        {
            dashCooldownSlider.value += Time.deltaTime / dashingCooldown;
            yield return null;
        }

        SetDashCooldownSliderColor(_dashEnabledColor);
    }

    private void SetDashCooldownSliderColor(Color color)
    {
        if (dashCooldownSlider == null)
        {
            return;
        }

        var imageComponent = dashCooldownSlider.transform.Find("Fill Area").transform.Find("Fill").GetComponent<Image>();
        if (imageComponent == null)
        {
            return;
        }

        imageComponent.color = color;
    }

    public void SetInteractableBehaviour(IInteractableBehaviour interactableBehaviour)
    {
        _interactableBehaviour = interactableBehaviour;
        _logger.LogInfo("Setting Interactable Behaviour", this);
    }
    
    public void Interact()
    {
        if (_interactableBehaviour != null)
        {
            _interactableBehaviour.Interact();
        }
    }

    public void SetCanMove(bool canMove)
    {
        if (!canMove && rb != null)
        {
            rb.velocity = Vector2.zero;
        }

        _canMove = canMove;
    }

    public bool GetCanMove()
    {
        return _canMove;
    }
}
