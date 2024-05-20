using UnityEngine;

public class ObjectCollisionDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            var playerManager = other.gameObject.GetComponent<PlayerManager>();
            if (playerManager != null && playerManager.canMove)
            {
                playerManager.TakeDamage();
            }
        }
    }
}
