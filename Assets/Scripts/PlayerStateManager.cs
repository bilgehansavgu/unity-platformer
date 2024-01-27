using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStateManager : MonoBehaviour
{
    private IPlayerState currentState;
    
    void Start()
    {
        ChangeState(new IdleState());
    }
    
    void Update()
    {
        currentState.UpdateState(this);

        switch (currentState)
        {
            case IdleState:

                break;
            
            case WalkingState:

                break;
            
            case JumpingState:

                break;
            
            case DoubleJumpingState:

                break;
        }
    }

    public void ChangeState(IPlayerState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState(this);
        }

        newState.EnterState(this);
        currentState = newState;
    }
}