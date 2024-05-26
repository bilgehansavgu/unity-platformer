using UnityEngine;

public class PlayerState_Jump : PlayerState_Base
{
    const string jumpClip = "Jump";
    private const string fallClip = "JumpFall";
    private int jumpCount;

    private bool _prevJumpInput;
    public PlayerState_Jump(PlayerController parent) : base(parent)
    {
        
    }

    public PlayerController.StateID GetID() => PlayerController.StateID.Move;

    public override void Enter(StateMachine<PlayerController.StateID> machine)
    {
        parent.Rb.velocity += new Vector2(0,parent.config.JumpVelocity);
        jumpCount = parent.config.JumpCount; 
        _prevJumpInput = true;
    }

    public override void Exit(StateMachine<PlayerController.StateID> machine)
    {
    }

    protected override void Act(StateMachine<PlayerController.StateID> machine)
    {
        if (parent.PlayerInputs.JumpTriggered && _prevJumpInput == false && jumpCount > 0)
        {
            // yükselirken ayrı düşerken ayrı double jump forceları eklenecek
            parent.Rb.velocity += new Vector2(0, parent.config.JumpVelocity - parent.Rb.velocity.y);
            jumpCount--;
        }

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
            parent.Rb.velocity += Vector2.up * (Physics2D.gravity.y * parent.config.FallLoad * Time.deltaTime);
        }
        else if (parent.Rb.velocity.y > 0)
        {
            parent.Rb.velocity += Vector2.up * (float)(Physics2D.gravity.y * parent.config.JumpLoad * Time.deltaTime);
        }
        
        
        if (parent.Rb.velocity.y < -14)
            PlayClip(fallClip);
        else
        {
            PlayClip(jumpClip, parent.GetAirSprite(9), 9);
        }
        
        _prevJumpInput = parent.PlayerInputs.JumpTriggered;
    }
    
    protected override void Decide(StateMachine<PlayerController.StateID> machine)
    {  
        if (parent.PlayerInputs.AttackSquareActionTriggered)
            machine.ChangeState(PlayerController.StateID.SquareAttack);
        if (parent.PlayerInputs.AttackTriangleActionTriggered)
            machine.ChangeState(PlayerController.StateID.TriangleAttack);
        if (parent.PlayerInputs.DashTriggered)
            if (parent.PlayerInputs.MoveInputValue.x != 0 && parent.Rb.velocity.x != 0)
            {
                machine.ChangeState(PlayerController.StateID.Dash);
            }
        if (parent.IsGrounded() && parent.Rb.velocity.y is <= 0 and >= -1)
            machine.ChangeState(PlayerController.StateID.Landing);
    }
}
