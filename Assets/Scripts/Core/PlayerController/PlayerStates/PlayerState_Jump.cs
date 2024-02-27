﻿using UnityEngine;

public class PlayerState_Jump : PlayerState_Base
{
    const string jumpClip = "Jump";
    private const string fallClip = "JumpFall";
    private float _maxMovementVelocity = 5f;
    private float _jumpVelocity = 15f;
    private float jumpLoad = 3f;
    private float fallLoad = 4f;
    
    private float maxSpeed = 5f;
    private float minSpeed = 50f;
    
    private float _maxFallSpeed = -15f;

    private int _jumpCount;
    private bool _prevJumpInput;
    public PlayerState_Jump(PlayerController parent) : base(parent)
    {
        
    }

    public override PlayerController.StateID GetID() => PlayerController.StateID.Move;

    public override void Enter(StateMachine<PlayerController.StateID> machine)
    {
        parent.Rb.velocity += new Vector2(0,_jumpVelocity);
        _jumpCount = 1;
        _prevJumpInput = true;
    }

    public override void Exit(StateMachine<PlayerController.StateID> machine)
    {
        Debug.Log("Max Speed: " + maxSpeed);
        Debug.Log("Min Speed: " + minSpeed);
    }

    protected override void Act(StateMachine<PlayerController.StateID> machine)
    {
        //
        if (parent.Inputs.JumpTriggered && _prevJumpInput == false && _jumpCount > 0)
        {
            // yükselirken ayrı düşerken ayrı double jump forceları eklenecek
            parent.Rb.velocity += new Vector2(0, _jumpVelocity - parent.Rb.velocity.y);
            _jumpCount--;
        }

        if (parent.Inputs.MoveInputValue.x > 0)
        {
            parent.transform.rotation = Quaternion.Euler(0, 0, 0);
            if (parent.Rb.velocity.x < _maxMovementVelocity)
            {
                float speedDifference = Mathf.Abs(_maxMovementVelocity - parent.Rb.velocity.x);
                parent.Rb.velocity += new Vector2(speedDifference, 0);
            }
        }
        else if (parent.Inputs.MoveInputValue.x < 0)
        {
            parent.transform.rotation = Quaternion.Euler(0, 180, 0);
            if (parent.Rb.velocity.x > -_maxMovementVelocity)
            {
                float speedDifference = Mathf.Abs(_maxMovementVelocity + parent.Rb.velocity.x);
                parent.Rb.velocity += new Vector2(-speedDifference, 0);
            }
        }
        
        
        // if rising and no space input

        if (parent.Rb.velocity.y <= 0)
        {
            parent.Rb.velocity += Vector2.up * (Physics2D.gravity.y * fallLoad * Time.deltaTime);
        }
        // if rising but space hold down

        else if (parent.Rb.velocity.y <= 0 && parent.Inputs.JumpTriggered)
        {
            parent.Rb.velocity += Vector2.up * (Physics2D.gravity.y * fallLoad * 0.5f * Time.deltaTime);
        }
        // if rising but space not hold down
        else if (parent.Rb.velocity.y > 0)
        {
            parent.Rb.velocity += Vector2.up * (float)(Physics2D.gravity.y * jumpLoad * Time.deltaTime);
        }
        
        if (parent.Rb.velocity.y < -13)
            PlayClip(fallClip);
        else
        {
            PlayClip(jumpClip, parent.GetAirSprite(9), 9);
        }
        
        if (parent.Rb.velocity.y < _maxFallSpeed)
            parent.Rb.velocity += new Vector2(0, Mathf.Abs(parent.Rb.velocity.y - _maxFallSpeed));
        
        _prevJumpInput = parent.Inputs.JumpTriggered;
    }
    
    protected override void Decide(StateMachine<PlayerController.StateID> machine)
    {  
        if (parent.IsWalled())
            machine.ChangeState(PlayerController.StateID.WallHangIdle);
        if (parent.Inputs.AttackSquareActionTriggered && parent.ReadyToAttack)
            machine.ChangeState(PlayerController.StateID.SquareAttack);
        if (parent.Inputs.AttackTriangleActionTriggered && parent.ReadyToAttack)
            machine.ChangeState(PlayerController.StateID.TriangleAttack);
        if (parent.Inputs.DashTriggered)
            machine.ChangeState(PlayerController.StateID.Dash);
        if (parent.IsGrounded() && parent.Rb.velocity.y <= 0)
            machine.ChangeState(PlayerController.StateID.Landing);
    }
}
