using UnityEngine;

public class TriangleAttackState : MonoBehaviour, IPlayerState
{
    private void Start()
    {
        // You can add any initialization logic for the TriangleAttackState
    }

    public void EnterState()
    {
        // Trigger triangle attack action
        GetComponent<PlayerCombat>().PerformTriangleAttack();
    }

    public void UpdateState()
    {
        // You can add any specific logic for the TriangleAttackState update if needed
    }

    public void ExitState()
    {
        // You can add any cleanup logic for when the player exits the TriangleAttackState
    }
}