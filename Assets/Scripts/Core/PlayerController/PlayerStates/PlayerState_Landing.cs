using UnityEngine;

public class PlayerState_Landing : PlayerState_Base
{
    const string landingClip = "JumpLand";
    public PlayerState_Landing(PlayerController parent) : base(parent)
    {
    }
    public PlayerController.StateID GetID() => PlayerController.StateID.Landing;

    public override void Enter(StateMachine<PlayerController.StateID> machine)
    {
        PlayClip(landingClip);
    }

    public override void Exit(StateMachine<PlayerController.StateID> machine)
    {

    }


    protected override void Act(StateMachine<PlayerController.StateID> machine)
    {
        if (!parent.PlayerInputs.IsHorizontalMoveInput)
        {
            parent.Rb.velocity = new Vector2();
        }
    }

    protected override void Decide(StateMachine<PlayerController.StateID> machine)
    {
        if (isAnimationFinished())
        {
            machine.ChangeState(PlayerController.StateID.Idle);
        }

        if (parent.PlayerInputs.IsJumpInput)
        {
            machine.ChangeState(PlayerController.StateID.Jump);
        }
        
        if (parent.PlayerInputs.IsHorizontalMoveInput)
        {
            machine.ChangeState(PlayerController.StateID.Move);
        }
    }
    
    private bool isAnimationFinished(float normalizedTime = 1)
    {
        return parent.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= normalizedTime;
    }
}