using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public void SetInteractableBehaviour(IInteractableBehaviour interactableBehaviour)
    {
        if (playerMovement != null)
        {
            playerMovement.SetInteractableBehaviour(interactableBehaviour);
        }
    }
}
