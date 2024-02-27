﻿using UnityEngine;

public class PlayerState_Falling : PlayerState_Base
{
    const string fallClip = "JumpFall";
    private float _maxMovementVelocity = 5f;
    private float fallLoad = 4f;
    public PlayerState_Falling(PlayerController parent) : base(parent)
    {
    }

    public override PlayerController.StateID GetID() => PlayerController.StateID.Falling;

    public override void Enter(StateMachine<PlayerController.StateID> machine)
    {
        PlayClip(fallClip);
    }

    public override void Exit(StateMachine<PlayerController.StateID> machine)
    {
    }


    protected override void Act(StateMachine<PlayerController.StateID> machine)
    {
        if (parent.Rb.velocity.y <= 0)
        {
            parent.Rb.velocity += Vector2.up * (Physics2D.gravity.y * fallLoad * Time.deltaTime);
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
    }

    protected override void Decide(StateMachine<PlayerController.StateID> machine)
    {
        if (parent.Inputs.AttackSquareActionTriggered && parent.ReadyToAttack)
            machine.ChangeState(PlayerController.StateID.SquareAttack);
        if (parent.Inputs.AttackTriangleActionTriggered && parent.ReadyToAttack)
            machine.ChangeState(PlayerController.StateID.TriangleAttack);
        if (parent.IsGrounded())
            machine.ChangeState(PlayerController.StateID.Move);
        if (parent.Inputs.DashTriggered)
            machine.ChangeState(PlayerController.StateID.Dash);
    }
}