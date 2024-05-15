
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


// Responsible for IO operations related to scenarios (and with that, decisions) 
public static class ScenarioIOUtility
{

    public static IEnumerator LoadScenarios(System.Action<List<Scenario>> callback)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "ScenariosDecisions.csv");

        string[] lines = null;

        // Check if we're running in WebGL
        if (filePath.Contains("://") || filePath.Contains(":///"))
        {
            UnityWebRequest www = UnityWebRequest.Get(filePath);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to load scenarios: " + www.error);
                callback?.Invoke(null);
                yield break;
            }

            lines = www.downloadHandler.text.Split(new string[] { "\r\n", "\n" }, System.StringSplitOptions.None);
        }
        else
        {
            Debug.Log("Reading scenarios from file: " + filePath);
            lines = File.ReadAllLines(filePath);
         
        }

        List<Scenario> scenariosBuffer = new List<Scenario>();
        for (int i = 1; i < lines.Length; i++) // Start from index 1 to skip the header line
        {
            // log line
            Debug.Log(lines[i]);
            string[] values = lines[i].Split(';');
            Debug.Log($"KIJK HIER: {values[7]} {values[8]} {values[9]}"); 
            Scenario scenario = new Scenario()
            {
                Title = values[0],
                Description = values[1],
                CorrectDecision = new Decision() { Title = values[2], Description = values[3] },
                IncorrectDecision = new Decision() { Title = values[4], Description = values[5] },
                IsCompleted = false,
                Explanation = values[6],
                AutonomyScore = double.Parse(values[7]) / 10,
                CompetencyScore = double.Parse(values[8]) / 10,
                ConnectednessScore = double.Parse(values[9]) / 10
            };
            scenariosBuffer.Add(scenario);
        }

        callback?.Invoke(scenariosBuffer);
    }
}
