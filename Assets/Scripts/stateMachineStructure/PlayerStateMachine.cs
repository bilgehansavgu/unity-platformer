using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private IPlayerState currentState;

    public void SetState(IPlayerState state)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }

        currentState = state;
        currentState.EnterState();
    }

    private void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.UpdateState();
        }
    }
}