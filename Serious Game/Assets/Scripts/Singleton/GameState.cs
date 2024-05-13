using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameState : GenericSingleton<GameState>
{
    public bool GameIsActive { get; set; } = false;
    public int PlayedDecisionRoomsCount { get; private set; }
    public int PlayedChallengeRoomsCount { get; private set; }

    public List<Scenario> Scenarios { get; private set; }

    // Game rules
    private static readonly int REQUIRED_DECISIONS = 3;
    private static readonly int REQUIRED_DECISIONS_FOR_FINAL_LEVEL = 9;
    private static readonly int REQUIRED_CHALLENGES_FOR_FINAL_LEVEL = 3;

    public void Initialize()
    {
        GameIsActive = true;
        PlayedDecisionRoomsCount = 0;
        Scenarios = ScenarioIOUtility.LoadScenarios(); 
    }

    public void IncrementPlayedDecisionRoomCount() => PlayedDecisionRoomsCount++; 
    public void IncrementPlayedChallengeRoomCount() => PlayedChallengeRoomsCount++;

 
    // GAME RULE
    // Challenge room should be the next room if 3 decisions were made in a row
    public bool NextRoomShouldBeChallengeRoom() => PlayedDecisionRoomsCount % REQUIRED_DECISIONS == 0;

    // GAME RULE
    public bool NextRoomShouldBeFinalRoom() => PlayedChallengeRoomsCount == REQUIRED_CHALLENGES_FOR_FINAL_LEVEL && PlayedDecisionRoomsCount == REQUIRED_DECISIONS_FOR_FINAL_LEVEL;

    public void Pause() => GameIsActive = false;

    public void Resume() => GameIsActive = true;

    // TODO: no more available scenarios? 
    public Scenario GetRandomAvailableScenario()
    {
        var availableScenarios = Scenarios.Where(s => !s.IsCompleted).ToList();
        int randomIndex = Random.Range(0, availableScenarios.Count);

        return availableScenarios[randomIndex];
    }

    // Temp tostring for fun
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < Scenarios.Count; i++)
        {
            sb.Append($"Scenario {i + 1}: {Scenarios[i].Title} - Correct Decision Made: {Scenarios[i].ChoseCorrectly}\n");
        }
        return sb.ToString();
    }
}
