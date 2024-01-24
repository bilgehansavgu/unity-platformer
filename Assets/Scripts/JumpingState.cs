using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : IPlayerState
{
    public void EnterState(PlayerStateManager playerManager)
    {
        // Logic when entering
        Debug.Log("Entering Jumping State");
        
        ((PlayerParam)playerManager.GetComponent<PlayerParam>()).Jump();
    }

    public void UpdateState(PlayerStateManager playerManager)
    {
        // Logic to handle
        Debug.Log("Jumping State Update");
        

    }

    public void ExitState(PlayerStateManager playerManager)
    {
        // Logic when exiting
        Debug.Log("Exiting Jumping State");
    }
}
