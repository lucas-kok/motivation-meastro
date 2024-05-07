using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movementInput;
    private bool dashInput;
    private bool pauseInput;
    private bool submitInput;

    private bool canDash = true;
    private bool isDashing;

    [SerializeField] public float speed;
    [SerializeField] public InputManager inputManager;
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

    void Update()
    {
        pauseInput = inputManager.CheckPauseInput();
        submitInput = inputManager.CheckSubmitInput();

        if (pauseInput) Pause();
        if (submitInput) Submit();
    }

    void FixedUpdate()
    {
        if (isDashing) return;

        movementInput = inputManager.CheckMovementInputs();
        dashInput = inputManager.CheckDashInput();

        Move();

        if (dashInput && canDash) StartCoroutine(Dash());
    }

    private void Move()
    {
        if (movementInput.x != 0 && movementInput.y != 0)
        {
            Debug.Log("hoitest1323~!");
            rb.velocity = new Vector2(movementInput.x * speed * Time.deltaTime * 0.707f, movementInput.y * speed * Time.deltaTime * 0.707f);
        } else
        {
            rb.velocity = new Vector2(movementInput.x * speed * Time.deltaTime, movementInput.y * speed * Time.deltaTime);
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        if (movementInput.x != 0 && movementInput.y != 0)
        {
            rb.velocity = new Vector2(movementInput.x * dashingPower * 0.707f, movementInput.y * dashingPower * 0.707f);
            Debug.Log(rb.velocity);
        } else
        {
            rb.velocity = new Vector2(movementInput.x * dashingPower, movementInput.y * dashingPower);
            Debug.Log(rb.velocity);
        }

       
        tr.emitting = true;
        
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void Pause()
    {
        // Pause the game
    }

    private void Submit()
    {
        // Submit the game
    }
}
