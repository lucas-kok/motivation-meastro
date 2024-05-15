public class Scenario
{
    public string Title;
    public string Description;

    public Decision CorrectDecision;
    public Decision IncorrectDecision;

    public string Explanation;

    public bool ChoseCorrectly;
    public bool IsCompleted = false;

    public double CompetencyScore;
    public double ConnectednessScore;
    public double AutonomyScore;

    // Retrieve the decision the player made, determine wether it was correct or not 
    public void SetPlayerChosenDecision(Decision decision)
    {
        ChoseCorrectly = CorrectDecision == decision;
        IsCompleted = true;
    }
}
