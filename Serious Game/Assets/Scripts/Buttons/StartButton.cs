using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : ButtonSetup
{
    protected override void DefineOnClick()
    {
        if (gameManager != null)
        {
            Debug.LogWarning("GameManager found in the scene.");
            button.onClick.AddListener(gameManager.StartGame);
        }
        else
        {
            Debug.LogWarning("GameManager not found in the scene.");
        }
    }
}
