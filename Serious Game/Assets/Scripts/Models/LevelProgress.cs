public class LevelProgress
{
    public int CurrentChallengeRoom { get; private set; } = 1;
    public int ChallengeRoomsVisited { get; private set; } = 0;

    public void WentToNextChallengeRoom()
    {
        CurrentChallengeRoom++;
    }
}
