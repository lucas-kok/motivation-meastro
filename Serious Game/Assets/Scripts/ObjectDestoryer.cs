using UnityEngine;

public class ObjectDestoryer : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Object")
        {
            Destroy(collision.gameObject);
        }
    }
}
