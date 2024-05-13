using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : GenericSingleton<GameState>
{
    public int CurrentChallengeRoom { get; private set; } = 1;

    public void RestartGameLoop()
    {
        CurrentChallengeRoom = 1; 
    }

    public void WentToNextChallengeRoom()
    {
        CurrentChallengeRoom++;
    }

    public void WentToNextDecisionRoom()
    {

    }
}
