
using JetBrains.Annotations;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TempFinalSceneManager : MonoBehaviour
{
    public TextMeshProUGUI wrongDecisionExplaination;
    public TextMeshProUGUI scoreDisplay;

    public GameObject content;
    public GameObject template;
    public int scenarioXAxisPosition = -100;
    public int scenarioYAxisPosition = 395;
    public int scenarioYAxisGap = 216;

    private GameState _gameState;

    void Start()
    {
        template.SetActive(false);
        wrongDecisionExplaination.text = "";
        scoreDisplay.text = "";

        _gameState = GameState.Instance;
      

        var contentRectTransform = content.GetComponent<RectTransform>();
        var templateRectTransofrm = template.GetComponent<RectTransform>();
        var templateHeight = templateRectTransofrm.rect.height;
        contentRectTransform.sizeDelta = new Vector2(contentRectTransform.rect.width, templateHeight * (_gameState.Scenarios.Count + 1));

        _gameState = GameState.Instance;

        foreach(var scenario in _gameState.Scenarios)
        {
            if (!scenario.IsCompleted)
            {
                continue;
            }
            
            var duplicatedObject = Instantiate(template, new Vector3(scenarioXAxisPosition, scenarioYAxisPosition, 0), Quaternion.identity);

            var tmp = duplicatedObject.GetComponentInChildren<TextMeshProUGUI>();
            tmp.text = scenario.Title;

            var wrongDecisionIcon = duplicatedObject.transform.Find("WrongDecisionIcon");
            var rightDecisionIcon = duplicatedObject.transform.Find("RightDecisionIcon");

            var infoIcon = duplicatedObject.transform.Find("InfoIcon");

            if (infoIcon != null)
            {
                infoIcon.gameObject.SetActive(!scenario.ChoseCorrectly);
                if (!scenario.ChoseCorrectly)
                {
                    var hoverFunctionality = duplicatedObject.transform.Find("InfoHover");
                    hoverFunctionality.GetComponent<HoverFunctionallity>().infoIcon = infoIcon.gameObject;
                    hoverFunctionality.GetComponent<HoverFunctionallity>().stillInfoButton = infoIcon.GetComponent<Image>().sprite;

                    infoIcon.GetComponent<Button>().onClick.AddListener(() => ShowInfo(scenario));
                }
            }
            if (wrongDecisionIcon != null) wrongDecisionIcon.gameObject.SetActive(!scenario.ChoseCorrectly);
            if (rightDecisionIcon != null) rightDecisionIcon.gameObject.SetActive(scenario.ChoseCorrectly);

            duplicatedObject.transform.SetParent(content.transform, false);

            duplicatedObject.SetActive(true);

            scenarioYAxisPosition -= scenarioYAxisGap;

            
        }

        ShowScore();
    }
    private void ShowInfo(Scenario scenario)
    {
        wrongDecisionExplaination.text = scenario.Explanation;
    }

    private void ShowScore()
    {
        Statistics stats = _gameState.CalculateGameStats();
        scoreDisplay.text = $"Uw score is: {stats.AchievedAutonomyScore} / {stats.MaxAutonomyScore} op autonomie. \n" +
            $"Uw score is: {stats.AchievedCompetencyScore} / {stats.MaxCompetencyScore} op competentie. \n" +
            $"Uw score is: {stats.AchievedConnectednessScore} / {stats.MaxConnectednessScore} op verbondenheid.";
    }
}
