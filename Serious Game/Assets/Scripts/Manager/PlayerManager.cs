using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerMovement playerMovement;
    public PlayerHealth playerHealth;
    public PlayerAnimationController playerAnimationController;
    
    public InputManager inputManager;

    public bool canMove
    {
        get => playerMovement.GetCanMove();
    }

    public void SetInteractableBehaviour(IInteractableBehaviour interactableBehaviour)
    {
        if (playerMovement != null)
        {
            playerMovement.SetInteractableBehaviour(interactableBehaviour);
        }
    }

    public void TakeDamage()
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage();
            playerAnimationController.PlayHurtAnimation();
            
            if (!playerHealth.IsAlive())
            {
                gameManager.RestartScene();
                SetCanMove(false);
            }
        }
    }

    public void Heal()
    {
        if (playerHealth != null)
        {
            playerHealth.Heal();
        }
    }

    public void SetCanMove(bool canMove)
    {
        if (playerMovement != null)
        {
            playerMovement.SetCanMove(canMove);
        }
    }

    public void SetPlayerAfk()
    {
        inputManager.ResetLatestPlayerMovement();
    }
}
