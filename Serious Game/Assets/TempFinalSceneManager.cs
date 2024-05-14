
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class TempFinalSceneManager : MonoBehaviour
{
    private GameState _gameState;
    private List<Scenario> scenarios;

    public GameObject content;
    public GameObject template;

    void Start()
    {
        _gameState = GameState.Instance;
        //scenarios = _gameState.Scenarios;
        scenarios = new List<Scenario>() { new Scenario(), new Scenario(), new Scenario(), new Scenario(), new Scenario() };

        foreach(var scenario in scenarios)
        {
            GameObject duplicatedObject = Instantiate(template, new Vector3(0, 0, 0), Quaternion.identity);

            var tmp = duplicatedObject.GetComponentInChildren<TextMeshProUGUI>();
            tmp.text = scenario.Title;

            // Set the parent of the duplicated object
            duplicatedObject.transform.SetParent(content.transform, false);


        }
    }

}
