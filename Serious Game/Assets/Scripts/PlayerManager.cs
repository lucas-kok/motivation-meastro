using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameManager GameManager;
    public PlayerMovement PlayerMovement;
    public PlayerHealth PlayerHealth;
    public PlayerAnimationController PlayerAnimationController;

    public bool CanMove
    {
        get => PlayerMovement.GetCanMove();
    }

    public void SetInteractableBehaviour(IInteractableBehaviour interactableBehaviour)
    {
        if (PlayerMovement != null)
        {
            PlayerMovement.SetInteractableBehaviour(interactableBehaviour);
        }
    }

    public void TakeDamage()
    {
        if (PlayerHealth != null)
        {
            PlayerHealth.TakeDamage();
            PlayerAnimationController.PlayHurtAnimation();
            
            if (!PlayerHealth.IsAlive())
            {
                GameManager.RestartScene();
                SetCanMove(false);
            }
        }
    }

    public void Heal()
    {
        if (PlayerHealth != null)
        {
            PlayerHealth.Heal();
        }
    }

    public void SetCanMove(bool canMove)
    {
        if (PlayerMovement != null)
        {
            PlayerMovement.SetCanMove(canMove);
        }
    }
}
