using UnityEngine;

public class PlayerState_Idle : PlayerState_Base
{
    const string idleClip = "Idle";
    public PlayerState_Idle(PlayerController parent) : base(parent)
    {
    }

    public PlayerController.StateID GetID() => PlayerController.StateID.Idle;

    public override void Enter(StateMachine<PlayerController.StateID> machine) => PlayClip(idleClip);

    public override void Exit(StateMachine<PlayerController.StateID> machine)
    {
    }
    protected override void Act(StateMachine<PlayerController.StateID> machine)
    {
        if (parent.IsGrounded())
        parent.Rb.velocity = new Vector2();
    }

    protected override void Decide(StateMachine<PlayerController.StateID> machine)
    {
        if (parent.IsMoveInput)
            machine.ChangeState(PlayerController.StateID.Move);
        if (parent.PlayerInputs.JumpTriggered && parent.IsGrounded())
            machine.ChangeState(PlayerController.StateID.Jump);
        // if (parent.PlayerInputs.AttackSquareActionTriggered)
        //     machine.ChangeState(PlayerController.StateID.SquareAttack);
        // if (parent.PlayerInputs.AttackTriangleActionTriggered)
        //     machine.ChangeState(PlayerController.StateID.TriangleAttack);
    }
}