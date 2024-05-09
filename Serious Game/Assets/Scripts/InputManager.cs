using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using static UnityEngine.Rendering.DebugUI;

public class InputManager : MonoBehaviour
{
    // Action controllers, managers et cetera
    public PlayerMovement playerMovement;

    // Input types
    public enum InputType { Keyboard, Controller }
    public InputType inputType = InputType.Keyboard;

    // Input keys
    [Header("Keyboard Controls")]
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode up = KeyCode.W;
    public KeyCode down = KeyCode.S;

    public KeyCode dash = KeyCode.LeftShift;
    public KeyCode pause = KeyCode.Escape;
    public KeyCode interact = KeyCode.Return;

    // Events
    public UnityEvent OnPauseGameEvent;

    private void Update()
    {
        // TODO: consider refactor: does a "PlayerMovement" really interact? 
        if (CheckInteractInput())
        {
            playerMovement.Interact();
        }
    }

    void FixedUpdate()
    {
        // Current movements 
        Vector2 movementInputs = GetMovementInputs();

        // Delegate Moving 
        playerMovement.Move(movementInputs);

        // Delegate Dashing
        if (CheckDashInput())
        {
            StartCoroutine(playerMovement.Dash(movementInputs));
        }
    }

    private Vector2 GetMovementInputs()
    {
        //movement input
        float x = 0f;
        float y = 0f;

        if (Input.GetKey(left)) x = -500f;
        if (Input.GetKey(right)) x = 500f;
        if (Input.GetKey(up)) y = 500f;
        if (Input.GetKey(down)) y = -500f;

        return new Vector2(x, y);
    }

    private bool CheckDashInput()
    {
        return Input.GetKey(dash);
    }

    public bool CheckInteractInput()
    {
        return Input.GetKeyDown(interact);
    }
    
}
