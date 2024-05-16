using System.Linq;
using TMPro;
using UnityEngine;
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
        var scenarios = _gameState.Scenarios;

        var contentRectTransform = content.GetComponent<RectTransform>();
        var templateRectTransofrm = template.GetComponent<RectTransform>();
        var templateHeight = templateRectTransofrm.rect.height;

        var numberOfCompletedScenarios = scenarios.Where(s => s.IsCompleted).Count();
        contentRectTransform.sizeDelta = new Vector2(contentRectTransform.rect.width, templateHeight * (numberOfCompletedScenarios + 1));

        foreach (var scenario in scenarios)
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
                    infoIcon.GetComponent<Button>().onClick.AddListener(() => DisplayExplaination(scenario));
                }
            }
            if (wrongDecisionIcon != null) wrongDecisionIcon.gameObject.SetActive(!scenario.ChoseCorrectly);
            if (rightDecisionIcon != null) rightDecisionIcon.gameObject.SetActive(scenario.ChoseCorrectly);

            duplicatedObject.transform.SetParent(content.transform, false);

            duplicatedObject.SetActive(true);

            scenarioYAxisPosition -= scenarioYAxisGap;
        }

        DisplayScore();
    }

    private void DisplayExplaination(Scenario scenario)
    {
        wrongDecisionExplaination.text = scenario.Explanation;
    }

    private void DisplayScore()
    {
        Statistics stats = _gameState.CalculateGameStats();
        scoreDisplay.text = $"Uw score is: {stats.AchievedAutonomyScore} / {stats.MaxAutonomyScore} op autonomie. \n" +
            $"Uw score is: {stats.AchievedCompetencyScore} / {stats.MaxCompetencyScore} op competentie. \n" +
            $"Uw score is: {stats.AchievedConnectednessScore} / {stats.MaxConnectednessScore} op verbondenheid.";

    }
}
