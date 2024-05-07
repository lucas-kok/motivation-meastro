using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonSetup : MonoBehaviour
{
    protected Button button;
    protected GameManager gameManager;

    private void Start()
    {
        button = GetComponent<Button>();
        gameManager = FindObjectOfType<GameManager>();
        DefineOnClick();
    }

    protected abstract void DefineOnClick();
}
