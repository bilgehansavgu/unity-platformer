using UnityEngine;

public class PlayerState_TriangleAttack : PlayerState_Base
{
    const string attack = "CrossPunch";
    public PlayerState_TriangleAttack(PlayerController parent) : base(parent)
    {
    }
    public PlayerController.StateID GetID() => PlayerController.StateID.TriangleAttack;

    public override void Enter(StateMachine<PlayerController.StateID> machine)
    {
        PlayClip(attack);
        parent.Rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public override void Exit(StateMachine<PlayerController.StateID> machine)
    {
        parent.Rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }



    protected override void Act(StateMachine<PlayerController.StateID> machine)
    {
    }
    protected override void Decide(StateMachine<PlayerController.StateID> machine)
    {
    }
}