using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    // Dash states
    private bool canDash = true;
    private bool isDashing = false;

    [SerializeField] public float speed;
    [SerializeField] public InputManager inputManager;
    [SerializeField] public ParticleSystem dustParticle;
    [SerializeField] public float dashingPower = 0.075f;
    [SerializeField] public float dashingTime = 0.2f;
    [SerializeField] public float dashingCooldown = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    public void Move(Vector2 movementInput)
    {
        if (isDashing)
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
        if (isDashing || !canDash)
        {
            yield break;
        }

        canDash = false;
        isDashing = true;

        if (movementInput.x != 0 && movementInput.y != 0)
        {
            rb.velocity = new Vector2(movementInput.x * dashingPower * 0.707f, movementInput.y * dashingPower * 0.707f);
        } else
        {
            rb.velocity = new Vector2(movementInput.x * dashingPower, movementInput.y * dashingPower);
        }

        dustParticle.Play();

        yield return new WaitForSeconds(dashingTime);
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    public void Interact()
    {
        // Let the player interact 
    }
}
