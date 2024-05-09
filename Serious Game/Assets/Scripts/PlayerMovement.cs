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
    [SerializeField] public ParticleSystem dustParticle;
    [SerializeField] public float dashingPower = 0.075f;
    [SerializeField] public float dashingTime = 0.2f;
    [SerializeField] public float dashingCooldown = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
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
        rb.velocity = new Vector2(movementInput.x * speed * Time.deltaTime, movementInput.y * speed * Time.deltaTime);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        if (movementInput.x != Mathf.Epsilon && movementInput.y != Mathf.Epsilon)
        {
             rb.velocity = new Vector2(movementInput.x * dashingPower * 0.5f, movementInput.y * dashingPower * 0.5f);
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

    private void Pause()
    {
        // Pause the game
    }

    private void Submit()
    {
        // Submit the game
    }
}
