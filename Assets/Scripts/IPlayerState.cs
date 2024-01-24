using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
    void EnterState(PlayerStateManager playerManager);
    void UpdateState(PlayerStateManager playerManager);
    void ExitState(PlayerStateManager playerManager);
}