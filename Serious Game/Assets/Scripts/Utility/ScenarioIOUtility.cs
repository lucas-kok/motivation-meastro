
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


// Responsible for IO operations related to scenarios (and with that, decisions) 
public static class ScenarioIOUtility
{
    public static List<Scenario> LoadScenarios()
    {
        var filePath = Path.Combine(Application.streamingAssetsPath, "ScenariosDecisions.csv");

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

    // FOR WEBGL BUILDS!
    //public static IEnumerator LoadScenarios(System.Action<List<Scenario>> callback)
    //{
    //    string filePath = Path.Combine(Application.streamingAssetsPath, "ScenariosDecisions.csv");
    //    UnityWebRequest www = UnityWebRequest.Get(filePath);
    //    yield return www.SendWebRequest();

    //    if (www.result != UnityWebRequest.Result.Success)
    //    {
    //        Debug.LogError("Failed to load scenarios: " + www.error);
    //        callback?.Invoke(null);
    //        yield break;
    //    }

    //    string[] lines = www.downloadHandler.text.Split('\n');
    //    List<Scenario> scenariosBuffer = new List<Scenario>();

    //    for (int i = 1; i < lines.Length; i++)
    //    {
    //        string[] values = lines[i].Split(';');
    //        Scenario scenario = new Scenario()
    //        {
    //            Title = values[0],
    //            Description = values[1],
    //            CorrectDecision = new Decision() { Title = values[2], Description = values[3] },
    //            IncorrectDecision = new Decision() { Title = values[4], Description = values[5] },
    //            IsCompleted = false,
    //            ReasonWhyPlayerChoseIncorrectly = values[6]
    //        };

    //        scenariosBuffer.Add(scenario);
    //    }

    //    callback?.Invoke(scenariosBuffer);
    //}
}
