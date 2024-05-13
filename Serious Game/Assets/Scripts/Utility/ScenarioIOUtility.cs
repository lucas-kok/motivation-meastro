
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public static class ScenarioIOUtility
{
    //public static List<ScenarioRecord> GetPlayerScenarioRecords()
    //{
    //    var decisionsFilePath = "Assets/Data/PlayerScenarios.json";

    //    if (File.Exists(decisionsFilePath))
    //    {
    //        var jsonData = File.ReadAllText(decisionsFilePath);
    //        var scenarioRecords = JsonUtility.FromJson<ScenarioRecordsWrapper>(jsonData) ?? new ScenarioRecordsWrapper();

    //        return scenarioRecords.ScenarioRecords;
    //    }

    //    return new List<ScenarioRecord>();
    //}

    public static List<Scenario> LoadScenarios()
    {
        //var completedTitles = new HashSet<string>();
        //var scenarioRecords = GetPlayerScenarioRecords();
        //foreach (var record in scenarioRecords)
        //{
        //    completedTitles.Add(record.Title);
        //}

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
                //IsCompleted = completedTitles.Contains(values[0]),
                IsCompleted = false,
                ReasonWhyPlayerChoseIncorrectly = values[6]
            };

            scenariosBuffer.Add(scenario);
        }

        return scenariosBuffer;
    }



}
