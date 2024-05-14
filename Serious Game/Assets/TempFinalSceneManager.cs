
using TMPro;
using UnityEngine;

public class TempFinalSceneManager : MonoBehaviour
{
    private GameState _gameState; 

    public TextMeshProUGUI statsText;

    void Start()
    {
        _gameState = GameState.Instance;
        
        statsText.SetText(_gameState.ToString());   
    }

}
