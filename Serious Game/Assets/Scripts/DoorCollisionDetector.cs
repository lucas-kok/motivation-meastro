using UnityEngine;

public class DoorEntryTrigger : MonoBehaviour
{
    private AppLogger _logger;

    private void Start()
    {
        _logger = AppLogger.Instance;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        _logger.LogInfo(col.gameObject.name, this);
        _logger.LogInfo(gameObject.tag, this);
        if (col.gameObject.tag == "Player")
        {
            if (gameObject.tag == "Door1")
            {
                _logger.LogInfo("Player has entered the door 1", this);
            }
            else if (gameObject.tag == "Door2")
            {
                _logger.LogInfo("Player has entered the door 2", this);
            }
        }
    }
}
