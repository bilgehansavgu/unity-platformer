using System;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private IPlayerState currentState;

    public void Start()
    {
        
    }

    public void SetState(IPlayerState state)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }

        currentState = state;
        currentState.EnterState();
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState();
        }
        else
        {
            SetState(GetComponent<IdleState>());
        }
    }
}