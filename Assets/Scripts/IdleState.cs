using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IPlayerState
{
    public void EnterState(PlayerStateManager playerManager)
    {
        // Logic when entering the idle state
        Debug.Log("Entering Idle State");
    }

    public void UpdateState(PlayerStateManager playerManager)
    {
        // Logic to handle idle state
        Debug.Log("Idle State Update");
    }

    public void ExitState(PlayerStateManager playerManager)
    {
        // Logic when exiting the idle state
        Debug.Log("Exiting Idle State");
    }
}
