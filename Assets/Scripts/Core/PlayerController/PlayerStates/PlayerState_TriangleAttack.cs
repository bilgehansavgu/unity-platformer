using UnityEngine;

public class PlayerState_TriangleAttack : PlayerState_Base
{
    const string attack = "CrossPunch";
    public PlayerState_TriangleAttack(PlayerController parent) : base(parent)
    {
    }
    public override PlayerController.StateID GetID() => PlayerController.StateID.TriangleAttack;

    public override void Enter(StateMachine<PlayerController.StateID> machine)
    {
        PlayClip(attack);
        parent.Rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public override void Exit(StateMachine<PlayerController.StateID> machine)
    {
        parent.Rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        parent.SetAttackCooldown(0.7f);
    }



    protected override void Act(StateMachine<PlayerController.StateID> machine)
    {
    }
    protected override void Decide(StateMachine<PlayerController.StateID> machine)
    {
    }

    public override void InvokeState(StateMachine<PlayerController.StateID> machine)
    {
        if (parent.IsGrounded())
            machine.ChangeState(PlayerController.StateID.Idle);
    }
}