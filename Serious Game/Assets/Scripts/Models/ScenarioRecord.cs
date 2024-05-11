[System.Serializable]
public class ScenarioRecord
{
    public string Title;
    public string Description;
    public Decision ChosenDecision;

    public bool PlayerHasChosenCorrectly = false;
    public string ReasonWhy;
}
