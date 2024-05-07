using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsController : MonoBehaviour
{
    public GameObject optionsPrefab;

    public void Start()
    {
        // Create the options UI, put it under the UI canvas 
        Instantiate(optionsPrefab, GameObject.Find("UI").transform);
    }

    public void DoSomething()
    {
        Debug.Log("Options button clicked!");
    }
}
