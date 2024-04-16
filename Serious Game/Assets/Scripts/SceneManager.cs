using UnityEngine;

public class SceneManager : GenericSingleton<SceneManager>
{
    public AppLogger _logger;

    void Start()
    {
        _logger = AppLogger.Instance;
        
        _logger.LogInfo("SceneManager started.", this);
        _logger.LogWarning("This is a warning.", this);
        _logger.LogError("This is an error message.", this);
    }

    void Update()
    {
        
    }
}
