using UnityEngine;

public class ChallengeRoomManager : MonoBehaviour
{
    public GameObject layoutContainer;
    public GameObject DifficultyBarEasy;
    public GameObject DifficultyBarMedium;
    public GameObject DifficultyBarHard;
    private GameState _gameState;

    private void Start()
    {
        _gameState = GameState.Instance;

        SetDifficultyBar(_gameState.CurrentGameDifficulty);

        if (layoutContainer == null || layoutContainer.transform.childCount <= 0)
        {
            return;
        }

        var layoutsCount = layoutContainer.transform.childCount;
        for (int i = 0; i < layoutsCount; i++)
        {
            layoutContainer.transform.GetChild(i).gameObject.SetActive(false);
        }
        
        var randomIndex = Random.Range(0, layoutsCount);
        layoutContainer.transform.GetChild(randomIndex).gameObject.SetActive(true);

    }

    private void SetDifficultyBar(GameDifficulty diff)
    {
        switch (diff)
        {
            case GameDifficulty.EASY:
                DifficultyBarEasy.SetActive(true);
                break;
            case GameDifficulty.MEDIUM:
                DifficultyBarMedium.SetActive(true);
                break;
            case GameDifficulty.HARD:
                DifficultyBarHard.SetActive(true);
                break;
        }
    }
}
