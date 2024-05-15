using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

public class GameState : GenericSingleton<GameState>
{
    // Game rules
    private static readonly int REQUIRED_DECISIONS_FOR_CHALLENGE_ROOM = 3;
    private static readonly int REQUIRED_DECISIONS_FOR_FINAL_ROOM = 3;
    private static readonly int REQUIRED_CHALLENGES_FOR_FINAL_ROOM = 1;

    // General Game state
    public bool GameIsActive { get; set; } = false;

    // Progression state
    public int PlayedDecisionRoomsCount { get; private set; }
    public int PlayedChallengeRoomsCount { get; private set; }

    // Data 
    public List<Scenario> Scenarios { get; private set; } = new List<Scenario>();

    /// <summary>
    /// This method accounts for the situation where the game loop is executed multiple times in a session (e.g. by going back to main menu and starting the game again)
    /// This means that:
    /// -Score is reset
    /// -All scenarios are marked as not completed
    /// </summary>
    public void Initialize()
    {
        GameIsActive = true;

        PlayedDecisionRoomsCount = 0;
        PlayedChallengeRoomsCount = 0;

        foreach (var scenario in Scenarios)
        {
            scenario.IsCompleted = false;
        }
    }

    // When the singleton is created, we're gonna read the scenarios from the filesystem once
    public override void Awake()
    {
        base.Awake();
        LoadScenarios();
    }

    private void LoadScenarios()
    {
        StartCoroutine(ScenarioIOUtility.LoadScenarios(scenarios =>
        {
            Scenarios = scenarios;
        }));
    }

    public void IncrementPlayedDecisionRoomCount() => PlayedDecisionRoomsCount++;
    public void IncrementPlayedChallengeRoomCount() => PlayedChallengeRoomsCount++;


    /// <summary>
    /// Apply Game rule: After a certain number of played decision rooms in a row (see "// Game rules"), a challenge room should be played 
    /// </summary>
    public bool NextRoomShouldBeChallengeRoom() => PlayedDecisionRoomsCount % REQUIRED_DECISIONS_FOR_CHALLENGE_ROOM == 0;

    /// <summary>
    /// APPLY Game rule: a final room should be played after a certain number of played challenge and decisions rooms in total (see "// Game rules")
    /// </summary>
    public bool NextRoomShouldBeFinalRoom() => PlayedChallengeRoomsCount == REQUIRED_CHALLENGES_FOR_FINAL_ROOM && PlayedDecisionRoomsCount == REQUIRED_DECISIONS_FOR_FINAL_ROOM;

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
