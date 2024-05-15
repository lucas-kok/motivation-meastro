using UnityEngine;

public class Objectspawner : MonoBehaviour
{
    private GameState _gameState;
    
    public GameObject[] objects;
    public GameObject objectContainer;
    public PlayerManager playerManager;
    
    public float SpawnInterval = 2.0f;
    public float LaunchForce = 10.0f;

    private float _timer;

    void Start()
    {
        _gameState = GameState.Instance;
        _timer = SpawnInterval;

        SetSpawnerProperties();
    }

    void Update()
    {
        if (playerManager != null && !playerManager.CanMove)
        {
            return;
        }
        
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            SpawnAndLaunchObject();
            _timer = SpawnInterval;
        }
    }

    private void SetSpawnerProperties()
    {
        if (_gameState.CurrentGameDifficulty is GameDifficulty.EASY)
        {
            // Set easy properties
            SpawnInterval = 3.0f;
            LaunchForce = 5.0f;
        }
        else if (_gameState.CurrentGameDifficulty is GameDifficulty.MEDIUM)
        {
            // Set medium properties
            SpawnInterval = 2.0f;
            LaunchForce = 10.0f;
        }
        else if (_gameState.CurrentGameDifficulty is GameDifficulty.HARD)
        {
            // Set hard properties
            SpawnInterval = 1.0f;
            LaunchForce = 15.0f;
        }
    }

    void SpawnAndLaunchObject()
    {
        if (objects.Length == 0) return;

        var spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.value, 1.1f, 0));

        spawnPosition.z = 0;

        var selectedObject = objects[Random.Range(0, objects.Length)];
        var spawnedObject = Instantiate(selectedObject, spawnPosition, Quaternion.identity);
        spawnedObject.transform.SetParent(objectContainer.transform);

        var rb = spawnedObject.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = spawnedObject.AddComponent<Rigidbody2D>();
        }

        var launchDirection = new Vector2(Random.Range(-1f, 1f), -1).normalized;
        rb.AddForce(launchDirection * LaunchForce, ForceMode2D.Impulse);
    }
}
