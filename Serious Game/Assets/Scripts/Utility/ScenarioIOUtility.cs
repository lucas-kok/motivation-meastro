
using System.IO;
using System.Collections.Generic;


// Responsible for IO operations related to scenarios (and with that, decisions) 
public static class ScenarioIOUtility
{
    public static List<Scenario> LoadScenarios()
    {
        var filePath = "Assets/Data/ScenariosDecisions.csv";
        var lines = File.ReadAllLines(filePath);
        var scenariosBuffer = new List<Scenario>();

        for (int i = 1; i < lines.Length; i++)
        {
            var values = lines[i].Split(';');
            var scenario = new Scenario()
            {
                Title = values[0],
                Description = values[1],
                CorrectDecision = new Decision() { Title = values[2], Description = values[3] },
                IncorrectDecision = new Decision() { Title = values[4], Description = values[5] },
                IsCompleted = false,
                ReasonWhyPlayerChoseIncorrectly = values[6]
            };

            scenariosBuffer.Add(scenario);
        }
        return scenariosBuffer;
    }
}
