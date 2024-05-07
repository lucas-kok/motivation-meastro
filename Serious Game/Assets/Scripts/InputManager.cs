using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;

public class InputManager : MonoBehaviour
{
    public enum InputType { Keyboard, Controller }
    public InputType inputType = InputType.Keyboard;

    [Header("Keyboard Controls")]
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode up = KeyCode.W;
    public KeyCode down = KeyCode.S;

    public KeyCode dash = KeyCode.LeftShift;
    public KeyCode pause = KeyCode.Escape;
    public KeyCode submit = KeyCode.Return;

    public Vector2 CheckMovementInputs()
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

    public bool CheckDashInput()
    {
        return Input.GetKey(dash);
    }

    public bool CheckPauseInput()
    {
        return Input.GetKeyDown(pause);
    }

    public bool CheckSubmitInput()
    {
        return Input.GetKeyDown(submit);
    }
    
}
