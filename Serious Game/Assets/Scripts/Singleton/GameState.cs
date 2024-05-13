using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameState : GenericSingleton<GameState>
{
    // Game rules
    private static readonly int REQUIRED_DECISIONS = 3;
    private static readonly int REQUIRED_DECISIONS_FOR_FINAL_LEVEL = 9;
    private static readonly int REQUIRED_CHALLENGES_FOR_FINAL_LEVEL = 3;

    // General Game state
    public bool GameIsActive { get; set; } = false;

    // Progression state
    public int PlayedDecisionRoomsCount { get; private set; }
    public int PlayedChallengeRoomsCount { get; private set; }

    // Data 
    public List<Scenario> Scenarios { get; private set; }

    public void Initialize()
    {
        GameIsActive = true;

        PlayedDecisionRoomsCount = 0;
        PlayedChallengeRoomsCount = 0;

        Scenarios = ScenarioIOUtility.LoadScenarios();
    }

    public void IncrementPlayedDecisionRoomCount() => PlayedDecisionRoomsCount++;
    public void IncrementPlayedChallengeRoomCount() => PlayedChallengeRoomsCount++;


    /// <summary>
    /// Apply Game rule: After a certain number of played decision rooms in a row (see "// Game rules"), a challenge room should be played 
    /// </summary>
    public bool NextRoomShouldBeChallengeRoom() => PlayedDecisionRoomsCount % REQUIRED_DECISIONS == 0;

    /// <summary>
    /// APPLY Game rule: a final room should be played after a certain number of played challenge and decisions rooms in total (see "// Game rules")
    /// </summary>
    public bool NextRoomShouldBeFinalRoom() => PlayedChallengeRoomsCount == REQUIRED_CHALLENGES_FOR_FINAL_LEVEL && PlayedDecisionRoomsCount == REQUIRED_DECISIONS_FOR_FINAL_LEVEL;

    public void Pause() => GameIsActive = false;

    public void Resume() => GameIsActive = true;

    // ASSUMPTION: what is defined under // Game rules in terms of required played decision rooms count, is always the same as the amount of scenarios in "ScenariosDecisions.csv
    public Scenario GetRandomAvailableScenario()
    {
        var availableScenarios = Scenarios.Where(s => !s.IsCompleted).ToList();
        int randomIndex = Random.Range(0, availableScenarios.Count);

        return availableScenarios[randomIndex];
    }

    // TEMPORARY tostring for fun in the TEMPORARY "mock" final room scene
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
