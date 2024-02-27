

public class PlayerState_Idle : PlayerState_Base
{
    const string idleClip = "Idle";
    public PlayerState_Idle(PlayerController parent) : base(parent)
    {
    }

    public override PlayerController.StateID GetID() => PlayerController.StateID.Idle;

    public override void Enter(StateMachine<PlayerController.StateID> machine) => PlayClip(idleClip);

    public override void Exit(StateMachine<PlayerController.StateID> machine)
    {
    }
    protected override void Act(StateMachine<PlayerController.StateID> machine)
    {
    }

    protected override void Decide(StateMachine<PlayerController.StateID> machine)
    {
        if (parent.IsMoving)
            machine.ChangeState(PlayerController.StateID.Move);
        if (parent.Inputs.JumpTriggered && parent.IsGrounded())
            machine.ChangeState(PlayerController.StateID.Jump);
        if (parent.Inputs.AttackSquareActionTriggered && parent.ReadyToAttack)
            machine.ChangeState(PlayerController.StateID.SquareAttack);
        if (parent.Inputs.AttackTriangleActionTriggered && parent.ReadyToAttack)
            machine.ChangeState(PlayerController.StateID.TriangleAttack);
    }
}