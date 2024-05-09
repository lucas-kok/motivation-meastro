using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    // Dash states
    private bool canDash = true;
    private bool isDashing = false;

    [SerializeField] public float speed;
    [SerializeField] public TrailRenderer tr;
    [SerializeField] public float dashingPower = 0.075f;
    [SerializeField] public float dashingTime = 0.2f;
    [SerializeField] public float dashingCooldown = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        tr.emitting = false;
    }

    public void Move(Vector2 movementInput)
    {
        if (isDashing) return;
        rb.velocity = new Vector2(movementInput.x * speed * Time.deltaTime, movementInput.y * speed * Time.deltaTime);
    }

    public IEnumerator Dash(Vector2 movementInput)
    {
        // Check on dashing state
        if (isDashing) yield break;
        if (!canDash) yield break;
        canDash = false;
        isDashing = true;

        // Apply movement 
        if (movementInput.x != Mathf.Epsilon && movementInput.y != Mathf.Epsilon)
        {
             rb.velocity = new Vector2(movementInput.x * dashingPower * 0.5f, movementInput.y * dashingPower * 0.5f);
        } else
        {
            rb.velocity = new Vector2(movementInput.x * dashingPower, movementInput.y * dashingPower);
        }

        tr.emitting = true;
        
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    public void Interact()
    {
        // Let the player interact 
    }
}
