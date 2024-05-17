
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
        double autonomyPercentage = -1;
        double competencyPercentage = -1;
        double connectednessPercentage = -1;

        if (stats.AchievedAutonomyScore != 0 && stats.MaxAutonomyScore != 0)
        {
            autonomyPercentage = (double)stats.AchievedAutonomyScore / stats.MaxAutonomyScore * 100;
        }

        if (stats.AchievedCompetencyScore != 0 && stats.MaxCompetencyScore != 0)
        {
            competencyPercentage = (double)stats.AchievedCompetencyScore / stats.MaxCompetencyScore * 100;
        }

        if (stats.AchievedConnectednessScore != 0 && stats.MaxConnectednessScore != 0)
        {
            connectednessPercentage = (double)stats.AchievedConnectednessScore / stats.MaxConnectednessScore * 100;
        }

        SetAutomyScore(autonomyPercentage);
        SetCompetencyScore(competencyPercentage);
        SetConnectednessScore(connectednessPercentage);
    }

    private void SetAutomyScore(double score)
    {
        var autonomy = GameObject.Find("Autonomy");
        var autonomyHappy = autonomy.transform.GetChild(0).gameObject;
        var autonomyNeutral = autonomy.transform.GetChild(1).gameObject;
        var autonomyAngry = autonomy.transform.GetChild(2).gameObject;
        var scoreDisplay = GameObject.Find("AutonomyText (TMP)").GetComponent<TextMeshProUGUI>();


        if (score == -1)
        {
            autonomyNeutral.gameObject.SetActive(true);
            scoreDisplay.text += "\nN.v.t.";
            return;
        } 
        else if(score < 50)
        {
            autonomyAngry.gameObject.SetActive(true);
        } 
        else if(score >= 50 && score < 70)
        {
            autonomyNeutral.gameObject.SetActive(true);
        } 
        else
        {
            autonomyHappy.gameObject.SetActive(true);
        }

        scoreDisplay.text += "\n" + score.ToString("0.0") + "%";
    }

    private void SetCompetencyScore(double score)
    {
        var competency = GameObject.Find("Competence");
        var competencyHappy = competency.transform.GetChild(0).gameObject;
        var competencyNeutral = competency.transform.GetChild(1).gameObject;
        var competencyAngry = competency.transform.GetChild(2).gameObject;
        var scoreDisplay = GameObject.Find("CompetenceText (TMP)").GetComponent<TextMeshProUGUI>();

        if (score == -1)
        {
            competencyNeutral.gameObject.SetActive(true);
            scoreDisplay.text += "\nN.v.t.";
            return;
        }
        else if (score < 50)
        {
            competencyAngry.gameObject.SetActive(true);
        }
        else if (score >= 50 && score < 70)
        {
            competencyNeutral.gameObject.SetActive(true);
        }
        else
        {
            competencyHappy.gameObject.SetActive(true);
        }

        scoreDisplay.text += "\n" + score.ToString("0.0") + "%";
    }

    private void SetConnectednessScore(double score)
    {
        var connectedness = GameObject.Find("Connectedness");
        var connectednessHappy = connectedness.transform.GetChild(0).gameObject;
        var connectednessNeutral = connectedness.transform.GetChild(1).gameObject;
        var connectednessAngry = connectedness.transform.GetChild(2).gameObject;
        var scoreDisplay = GameObject.Find("ConnectednessText (TMP)").GetComponent<TextMeshProUGUI>();

        

        if (score == -1)
        {
            connectednessNeutral.gameObject.SetActive(true);
            scoreDisplay.text += "\nN.v.t.";
            return;
        }
        else if (score < 50)
        {
            connectednessAngry.gameObject.SetActive(true);
        }
        else if (score >= 50 && score < 70)
        {
            connectednessNeutral.gameObject.SetActive(true);
        }
        else
        {
            connectednessHappy.gameObject.SetActive(true);
        }

        scoreDisplay.text += "\n" + score.ToString("0.0") + "%";
    }
}
