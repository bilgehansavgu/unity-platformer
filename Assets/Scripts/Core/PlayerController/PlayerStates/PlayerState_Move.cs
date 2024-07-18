using UnityEngine;

public class PlayerState_Move : PlayerState_Base
{
    const string walkClip = "Run";
    public PlayerState_Move(PlayerController parent) : base(parent)
    {
    }

    public PlayerController.StateID GetID() => PlayerController.StateID.Move;

    public override void Enter(StateMachine<PlayerController.StateID> machine) => PlayClip(walkClip);


    public override void Exit(StateMachine<PlayerController.StateID> machine)
    {
    }

    protected override void Act(StateMachine<PlayerController.StateID> machine)
    {
        Move();
    }

    private void Move()
    {
        if (parent.PlayerInputs.MoveInputValue.x > 0)
        {
            parent.transform.rotation = Quaternion.Euler(0, 0, 0);
            WalkRight();
        }
        if (parent.PlayerInputs.MoveInputValue.x < 0)
        {
            parent.transform.rotation = Quaternion.Euler(0, 180, 0);
            WalkLeft();
        }
        if (parent.PlayerInputs.MoveInputValue.x == 0)
        {
            parent.Rb.velocity = new Vector2(0, parent.Rb.velocity.y);
        }
    }

    protected override void Decide(StateMachine<PlayerController.StateID> machine)
    {
        if (!parent.PlayerInputs.IsHorizontalMoveInput)
            machine.ChangeState(PlayerController.StateID.Idle);
        
        if (parent.PlayerInputs.IsJumpInput)
            machine.ChangeState(PlayerController.StateID.Jump);
        
        if (parent.PlayerInputs.AttackSquareActionTriggered)
            machine.ChangeState(PlayerController.StateID.SquareAttack);
        
        if (parent.PlayerInputs.AttackTriangleActionTriggered)
            machine.ChangeState(PlayerController.StateID.TriangleAttack);
        
        if (parent.Rb.velocity.y < -1)
            machine.ChangeState(PlayerController.StateID.Falling);
    }

    private void WalkRight()
    {
        if (parent.Rb.velocity.x < parent.config.MovementSpeed)
        {
            float speedDifference = Mathf.Abs(parent.config.MovementSpeed - parent.Rb.velocity.x);
            parent.Rb.velocity += new Vector2(parent.PlayerInputs.MoveInputValue.x * speedDifference, 0);
        }
    }

    private void WalkLeft()
    {
        if (parent.Rb.velocity.x > -parent.config.MovementSpeed)
        {
            float speedDifference = Mathf.Abs(parent.config.MovementSpeed + parent.Rb.velocity.x);
            parent.Rb.velocity += new Vector2(-speedDifference, 0);
        }
    }
}
