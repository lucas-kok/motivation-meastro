
using JetBrains.Annotations;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TempFinalSceneManager : MonoBehaviour
{
    public TextMeshProUGUI wrongDecisionExplaination;

    public GameObject content;
    public GameObject template;
    public int scenarioXAxisPosition = -100;
    public int scenarioYAxisPosition = 395;
    public int scenarioYAxisGap = 216;

    private GameState _gameState;
    private List<Scenario> _scenarios;

    void Start()
    {
        template.SetActive(false);
        wrongDecisionExplaination.text = "";

        _gameState = GameState.Instance;
        _scenarios = _gameState.Scenarios;
        var scenarios = new List<Scenario>() {
            new Scenario()
            {
                Title = "Scenario 1",
                Description = "Description 1",
                CorrectDecision = new Decision() { Title = "Correct Decision 1", Description = "Correct Decision Description 1" },
                IncorrectDecision = new Decision() { Title = "Incorrect Decision 1", Description = "Incorrect Decision Description 1" },
                ReasonWhyPlayerChoseCorrectly = "Reason Why Player Chose Correctly 1",
                ReasonWhyPlayerChoseIncorrectly = "Reason Why Player Chose Incorrectly 1",
                ChoseCorrectly = true,
                IsCompleted = true
            },
            new Scenario()
            {
                Title = "Scenario 2",
                Description = "Description 2",
                CorrectDecision = new Decision() { Title = "Correct Decision 2", Description = "Correct Decision Description 2" },
                IncorrectDecision = new Decision() { Title = "Incorrect Decision 2", Description = "Incorrect Decision Description 2" },
                ReasonWhyPlayerChoseCorrectly = "Reason Why Player Chose Correctly 2",
                ReasonWhyPlayerChoseIncorrectly = "Reason Why Player Chose Incorrectly 2",
                ChoseCorrectly = false,
                IsCompleted = true
            },
            new Scenario()
            {
                Title = "Scenario 3",
                Description = "Description 3",
                CorrectDecision = new Decision() { Title = "Correct Decision 3", Description = "Correct Decision Description 3" },
                IncorrectDecision = new Decision() { Title = "Incorrect Decision 3", Description = "Incorrect Decision Description 3" },
                ReasonWhyPlayerChoseCorrectly = "Reason Why Player Chose Correctly 3",
                ReasonWhyPlayerChoseIncorrectly = "Reason Why Player Chose Incorrectly 3",
                ChoseCorrectly = true,
                IsCompleted = true
            },
            new Scenario()
            {
                Title = "Scenario 4",
                Description = "Description 4",
                CorrectDecision = new Decision() { Title = "Correct Decision 4", Description = "Correct Decision Description 4" },
                IncorrectDecision = new Decision() { Title = "Incorrect Decision 4", Description = "Incorrect Decision Description 4" },
                ReasonWhyPlayerChoseCorrectly = "Reason Why Player Chose Correctly 4",
                ReasonWhyPlayerChoseIncorrectly = "Reason Why Player Chose Incorrectly 4",
                ChoseCorrectly = false,
                IsCompleted = true
            },
            new Scenario()
            {
                Title = "Scenario 5",
                Description = "Description 5",
                CorrectDecision = new Decision() { Title = "Correct Decision 5", Description = "Correct Decision Description 5" },
                IncorrectDecision = new Decision() { Title = "Incorrect Decision 5", Description = "Incorrect Decision Description 5" },
                ReasonWhyPlayerChoseCorrectly = "Reason Why Player Chose Correctly 5",
                ReasonWhyPlayerChoseIncorrectly = "Reason Why Player Chose Incorrectly 5",
                ChoseCorrectly = true,
                IsCompleted = true
            },
            new Scenario()
            {
                Title = "Scenario 6",
                Description = "Description 6",
                CorrectDecision = new Decision() { Title = "Correct Decision 6", Description = "Correct Decision Description 6" },
                IncorrectDecision = new Decision() { Title = "Incorrect Decision 6", Description = "Incorrect Decision Description 6" },
                ReasonWhyPlayerChoseCorrectly = "Reason Why Player Chose Correctly 6",
                ReasonWhyPlayerChoseIncorrectly = "Reason Why Player Chose Incorrectly 6",
                ChoseCorrectly = false,
                IsCompleted = true
            },
        };

        var contentRectTransform = content.GetComponent<RectTransform>();
        var templateRectTransofrm = template.GetComponent<RectTransform>();
        var templateHeight = templateRectTransofrm.rect.height;
        contentRectTransform.sizeDelta = new Vector2(contentRectTransform.rect.width, templateHeight * (_scenarios.Count + 1));

        _gameState = GameState.Instance;

        foreach(var scenario in _scenarios)
        {
            if (!scenario.IsCompleted)
            {
                continue;
            }
            
            var duplicatedObject = Instantiate(template, new Vector3(scenarioXAxisPosition, scenarioYAxisPosition, 0), Quaternion.identity);

            var tmp = duplicatedObject.GetComponentInChildren<TextMeshProUGUI>();
            tmp.text = scenario.Title;

            var infoIcon = duplicatedObject.transform.Find("InfoIcon");
            var wrongDecisionIcon = duplicatedObject.transform.Find("WrongDecisionIcon");
            var rightDecisionIcon = duplicatedObject.transform.Find("RightDecisionIcon");

            if (infoIcon != null)
            {
                infoIcon.gameObject.SetActive(!scenario.ChoseCorrectly);
                if (!scenario.ChoseCorrectly)
                {
                    infoIcon.GetComponent<Button>().onClick.AddListener(() => ShowInfo(scenario));
                }
            }
            if (wrongDecisionIcon != null) wrongDecisionIcon.gameObject.SetActive(!scenario.ChoseCorrectly);
            if (rightDecisionIcon != null) rightDecisionIcon.gameObject.SetActive(scenario.ChoseCorrectly);

            duplicatedObject.transform.SetParent(content.transform, false);

            duplicatedObject.SetActive(true);

            scenarioYAxisPosition -= scenarioYAxisGap;
        }
    }

    private void ShowInfo(Scenario scenario)
    {
        wrongDecisionExplaination.text = scenario.ReasonWhyPlayerChoseIncorrectly;
    }
}
