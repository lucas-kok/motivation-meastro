public class Scenario
{
    public string Title;
    public string Description;

    public Decision CorrectDecision;
    public Decision IncorrectDecision;

    public string ReasonWhyPlayerChoseCorrectly;
    public string ReasonWhyPlayerChoseIncorrectly;

    public bool ChoseCorrectly { get; private set; }
    public bool IsCompleted = false; 

    // Retrieve the decision the player made, determine wether it was correct or not 
    public void SetPlayerChosenDecision(Decision decision)
    {
        ChoseCorrectly = CorrectDecision == decision;
        IsCompleted = true;
    }
}