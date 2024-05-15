public record Statistics
{
    public double MaxCompetencyScore { get; set; }
    public double MaxConnectednessScore { get; set; }
    public double MaxAutonomyScore { get; set; }

    public double AchievedCompetencyScore { get; set; }
    public double AchievedConnectednessScore { get; set; }
    public double AchievedAutonomyScore { get; set; }

    // create a cool tostring
    public override string ToString()
    {
        return $"Max Competency Score: {MaxCompetencyScore}\n" +
               $"Max Connectedness Score: {MaxConnectednessScore}\n" +
               $"Max Autonomy Score: {MaxAutonomyScore}\n" +
               $"Achieved Competency Score: {AchievedCompetencyScore}\n" +
               $"Achieved Connectedness Score: {AchievedConnectednessScore}\n" +
               $"Achieved Autonomy Score: {AchievedAutonomyScore}";
    }

}
