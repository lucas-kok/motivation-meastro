using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsButton : ButtonSetup
{

    protected override void DefineOnClick()
    {
        button.onClick.AddListener(gameManager.GoToOptions);
    }
}