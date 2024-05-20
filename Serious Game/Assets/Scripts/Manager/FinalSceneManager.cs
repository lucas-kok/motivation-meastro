
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinalSceneManager : MonoBehaviour
{
    public TextMeshProUGUI wrongDecisionExplaination;

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

        _gameState = GameState.Instance;
        var scenarios = _gameState.Scenarios;

        var contentRectTransform = content.GetComponent<RectTransform>();
        var templateRectTransofrm = template.GetComponent<RectTransform>();
        var templateHeight = templateRectTransofrm.rect.height;

        var numberOfCompletedScenarios = scenarios.Where(s => s.IsCompleted).Count();
        contentRectTransform.sizeDelta = new Vector2(contentRectTransform.rect.width, templateHeight * (numberOfCompletedScenarios + 1));

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
        double autonomyPercentage = stats.MaxAutonomyScore == 0 ? -1 : (double)stats.AchievedAutonomyScore / stats.MaxAutonomyScore * 100;
        double competencyPercentage = stats.MaxCompetencyScore == 0 ? -1 : (double)stats.AchievedCompetencyScore / stats.MaxCompetencyScore * 100;
        double connectednessPercentage = stats.MaxConnectednessScore == -0 ? -1 : (double)stats.AchievedConnectednessScore / stats.MaxConnectednessScore * 100;

        SetScore("Autonomy", autonomyPercentage);
        SetScore("Competence", competencyPercentage);
        SetScore("Connectedness", connectednessPercentage);
    }

    private void SetScore(string aspect, double score)
    {
        var student = GameObject.Find(aspect);
        var studentHappy = student.transform.GetChild(0).gameObject;
        var studentNeutral = student.transform.GetChild(1).gameObject;
        var studentAngry = student.transform.GetChild(2).gameObject;
        var scoreDisplay = student.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();

        if (score == -1)
        {
            studentNeutral.gameObject.SetActive(true);
            studentNeutral.transform.GetChild(1).gameObject.SetActive(false);
            scoreDisplay.text += "\nN.v.t.";
            
            return;
        }
        else if (score < 50)
        {
            studentAngry.gameObject.SetActive(true);
        }
        else if (score >= 50 && score < 70)
        {
            studentNeutral.gameObject.SetActive(true);
        }
        else
        {
            studentHappy.gameObject.SetActive(true);
        }

        scoreDisplay.text += "\n" + score.ToString("0.0") + "%";
    }
}
