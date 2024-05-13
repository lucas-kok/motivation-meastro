using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Define a delegate that represents the signature of the methods that can handle the input event
    public delegate void KeyAction(KeyCode key);

    // Define an event based on that delegate
    public event KeyAction OnKeyPress;
    
    // Action controllers, managers et cetera
    public PlayerMovement playerMovement;
    private GameManager gameManagerInstance;

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
    public KeyCode pauseOrResume = KeyCode.Escape;
    public KeyCode interact = KeyCode.Return;

    private void Start()
    {
        // TODO: bug: singleton doesn't work, new instances are made if gamemanager prefab is dragged into a scene
        var gameManagers = FindObjectsOfType<GameManager>();
        gameManagerInstance = gameManagers[gameManagers.Length - 1];
    }

    private void Update()
    {
        EmitKeyPresses();
        
        if (CheckToggleMenuInput())
        {
            gameManagerInstance.ToggleMenu();
        }

        if (CheckInteractInput())
        {
            if (playerMovement != null) playerMovement.Interact();
        }

        if (Input.GetKeyDown(pauseOrResume))
        {
            Application.Quit();
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
