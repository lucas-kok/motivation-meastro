using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    // Define a delegate that represents the signature of the methods that can handle the input event
    public delegate void KeyAction(KeyCode key);

    // Define an event based on that delegate
    public event KeyAction OnKeyPress;
    
    // Action controllers, managers et cetera
    public PlayerMovement playerMovement;

    // Events
    public UnityEvent OnToggleMenuKeyPressed; 

    // Input types
    public enum InputType { Keyboard, Controller }
    public InputType inputType = InputType.Keyboard;

    // Input keys
    [Header("Keyboard Controls")]
    public KeyCode[] left = { KeyCode.A, KeyCode.LeftArrow };
    public KeyCode[] right = { KeyCode.D , KeyCode.RightArrow };
    public KeyCode[] up = { KeyCode.W, KeyCode.UpArrow };
    public KeyCode[] down = { KeyCode.S, KeyCode.DownArrow };

    public KeyCode dash = KeyCode.LeftShift;
    public KeyCode pauseOrResume = KeyCode.Escape;
    public KeyCode interact = KeyCode.Return;

    private Vector2 _latestPlayerMovement;

    private void Start()
    {
        _latestPlayerMovement = new Vector2(0f, -500f);
    }

    private void Update()
    {
        EmitKeyPresses();
        
        if (CheckToggleMenuInput())
        {
            OnToggleMenuKeyPressed.Invoke();
        }

        if (CheckInteractInput())
        {
            if (playerMovement != null) playerMovement.Interact();
        }
    }

    void FixedUpdate()
    {
        if (playerMovement == null) return;

        // Current movements 
        Vector2 movementInputs = GetMovementInputs();

        // Delegate Moving 
        playerMovement.Move(movementInputs);

        // Delegate Dashing
        if (CheckDashInput())
        {
            StartCoroutine(
                playerMovement.Dash(
                    (movementInputs.y == 0  && movementInputs.x == 0) ? _latestPlayerMovement : movementInputs
                )
            );
        }

        if (movementInputs.y != 0 || movementInputs.x != 0) _latestPlayerMovement = movementInputs;
    }

    private Vector2 GetMovementInputs()
    {
        //movement input
        float x = 0f;
        float y = 0f;

        if (Input.GetKey(left[0]) || Input.GetKey(left[1])) x = -500f;
        if (Input.GetKey(right[0]) || Input.GetKey(right[1])) x = 500f;
        if (Input.GetKey(up[0]) || Input.GetKey(up[1])) y = 500f;
        if (Input.GetKey(down[0]) || Input.GetKey(down[1])) y = -500f;

        return new Vector2(x, y);
    }

    private bool CheckToggleMenuInput()
    {
        return Input.GetKeyDown(pauseOrResume);
    }

    private bool CheckDashInput()
    {
        return Input.GetKey(dash);
    }

    private bool CheckInteractInput()
    {
        return Input.GetKeyDown(interact);
    }

    // Emits pressed key
    private void EmitKeyPresses()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                {
                    OnKeyPress?.Invoke(kcode);
                }
            }
        }
    }
}
