using UnityEngine;

public class PlayerState_Falling : PlayerState_Base
{
    const string fallClip = "JumpFall";
    
    public PlayerState_Falling(PlayerController parent) : base(parent)
    {
    }

    public PlayerController.StateID GetID() => PlayerController.StateID.Falling;

    public override void Enter(StateMachine<PlayerController.StateID> machine)
    {
        PlayClip(fallClip);
    }

    public override void Exit(StateMachine<PlayerController.StateID> machine)
    {
    }


    protected override void Act(StateMachine<PlayerController.StateID> machine)
    {
        if (parent.PlayerInputs.MoveInputValue.x > 0)
        {
            parent.transform.rotation = Quaternion.Euler(0, 0, 0);
            if (parent.Rb.velocity.x < parent.config.AirbourneMoveSpeed)
            {
                float speedDifference = Mathf.Abs(parent.config.AirbourneMoveSpeed - parent.Rb.velocity.x);
                parent.Rb.velocity += new Vector2(speedDifference, 0);
            }
        }
        else if (parent.PlayerInputs.MoveInputValue.x < 0)
        {
            parent.transform.rotation = Quaternion.Euler(0, 180, 0);
            if (parent.Rb.velocity.x > -parent.config.AirbourneMoveSpeed)
            {
                float speedDifference = Mathf.Abs(parent.config.AirbourneMoveSpeed + parent.Rb.velocity.x);
                parent.Rb.velocity += new Vector2(-speedDifference, 0);
            }
        }

        if (parent.Rb.velocity.y <= 0)
        {
            parent.Rb.velocity += Vector2.up * (Physics2D.gravity.y * parent.config.FallLoad * 0.5f * Time.deltaTime);
        }
    }

    protected override void Decide(StateMachine<PlayerController.StateID> machine)
    {
        // if (parent.PlayerInputs.AttackSquareActionTriggered)
        //     machine.ChangeState(PlayerController.StateID.SquareAttack);
        // if (parent.PlayerInputs.AttackTriangleActionTriggered)
        //     machine.ChangeState(PlayerController.StateID.TriangleAttack);
        if (parent.IsGrounded() && parent.IsMoveInput)
            machine.ChangeState(PlayerController.StateID.Move);
        if (parent.IsGrounded() && !parent.IsMoveInput)
            machine.ChangeState(PlayerController.StateID.Landing);
        if (parent.PlayerInputs.DashTriggered)
            machine.ChangeState(PlayerController.StateID.Dash);
        if (parent.PlayerInputs.JumpTriggered)
            machine.ChangeState(PlayerController.StateID.Jump);
    }
}