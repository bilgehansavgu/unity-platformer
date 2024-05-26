using UnityEngine;

public class PlayerState_Dash : PlayerState_Base
{
    const string dashClip = "Dash";

    public PlayerState_Dash(PlayerController parent) : base(parent)
    {
    }

    public PlayerController.StateID GetID() => PlayerController.StateID.Dash;

    public override void Enter(StateMachine<PlayerController.StateID> machine)
    {
        PlayClip(dashClip);

        if (parent.PlayerInputs.MoveInputValue.x > 0)
        {
            parent.transform.rotation = Quaternion.Euler(0, 0, 0);
            //parent.Rb.AddForce(Vector2.right * parent.config.DashForce , ForceMode2D.Impulse);
            parent.Rb.velocity = new Vector2(parent.config.DashForce, 0);
        }
        else if (parent.PlayerInputs.MoveInputValue.x < 0)
        {
            parent.transform.rotation = Quaternion.Euler(0, 180, 0);
            //parent.Rb.AddForce(Vector2.left * parent.config.DashForce, ForceMode2D.Impulse);
            parent.Rb.velocity = new Vector2(-parent.config.DashForce, 0);
        }
    }

    public override void Exit(StateMachine<PlayerController.StateID> machine)
    {

    }

    protected override void Act(StateMachine<PlayerController.StateID> machine)
    {
        if (isAnimationHalfwayFinished() && parent.Rb.velocity.y <= 0)
        {
            //parent.Rb.velocity += new Vector2(0, -parent.Rb.velocity.y);
        }
    }

    protected override void Decide(StateMachine<PlayerController.StateID> machine)
    {  
        if (isAnimationFinished())
        {
            machine.ChangeState(PlayerController.StateID.Falling);
        }
    }
    
    private bool isAnimationFinished()
    {
        return parent.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;
    }
    
    private bool isAnimationHalfwayFinished()
    {
        return parent.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5;
    }
}