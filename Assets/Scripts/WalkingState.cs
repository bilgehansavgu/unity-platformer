using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : IPlayerState
{
    public void EnterState(PlayerStateManager playerManager)
    {
        // Logic when entering
        Debug.Log("Entering Walking State");
    }

    public void UpdateState(PlayerStateManager playerManager)
    {
        // Logic to handle
        Debug.Log("Walking State Update");
    
        float horizontalInput = Input.GetAxis("Horizontal");
        //((PlayerParam)playerManager.GetComponent<PlayerParam>()).Move(horizontalInput);
    }


    public void ExitState(PlayerStateManager playerManager)
    {
        // Logic when exiting
        Debug.Log("Exiting Walking State");
    }
}
