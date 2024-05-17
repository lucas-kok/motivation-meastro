
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

// Responsible for IO operations related to scenarios (and with that, decisions) 
public static class ScenarioIOUtility
{

    public static IEnumerator LoadScenarios(Action<List<Scenario>> callback)
    {
        var filePath = Path.Combine(Application.streamingAssetsPath, "ScenariosDecisions.csv");
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

            lines = www.downloadHandler.text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
        }
        else
        {
            Debug.Log("Reading scenarios from file: " + filePath);
            lines = File.ReadAllLines(filePath);
        }

        List<Scenario> scenariosBuffer = new List<Scenario>();
        for (int i = 1; i < lines.Length; i++) // Start from index 1 to skip the header line
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
            {
                continue;
            }

            var line = lines[i].Trim('"');
            var values = line.Split(';');

            if (values.Length != 10)
            {
                Debug.LogError($"Line {i + 1} is invalid. Expected 10 values but got {values.Length}. Line content: {lines[i]}");
                continue;
            }

            try
            {
                var scenario = new Scenario()
                {
                    Title = values[0].Trim('"'),
                    Description = values[1].Trim('"'),
                    CorrectDecision = new Decision() { Title = values[2].Trim('"'), Description = values[3].Trim('"') },
                    IncorrectDecision = new Decision() { Title = values[4].Trim('"'), Description = values[5].Trim('"') },
                    IsCompleted = false,
                    Explanation = values[6].Trim('"'),
                    AutonomyScore = double.Parse(values[7].Trim('"')) / 10,
                    CompetencyScore = double.Parse(values[8].Trim('"')) / 10,
                    ConnectednessScore = double.Parse(values[9].Trim('"')) / 10
                };

                scenariosBuffer.Add(scenario);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error parsing line {i + 1}: {ex.Message}. Line content: {lines[i]}");
            }
        }

        callback?.Invoke(scenariosBuffer);
    }
}
