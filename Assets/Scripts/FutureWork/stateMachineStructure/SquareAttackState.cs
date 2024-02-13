using UnityEngine;

public class SquareAttackState : MonoBehaviour, IPlayerState
{
    private void Start()
    {
        // You can add any initialization logic for the SquareAttackState
    }

    public void EnterState()
    {
        // Trigger square attack action
        GetComponent<PlayerCombat>().PerformSquareAttack();
    }

    public void UpdateState()
    {
        // You can add any specific logic for the SquareAttackState update if needed
    }

    public void ExitState()
    {
        // You can add any cleanup logic for when the player exits the SquareAttackState
    }
}