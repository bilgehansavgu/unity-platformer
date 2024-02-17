﻿using UnityEngine;
using Core.StateMachine;

namespace Core.CharacterController
{
    public class PlayerState_Jump : PlayerState_Base
    {
        const string jumpClip = "jump_animation";
        private float _maxMovementVelocity = 5f;
        private float jumpLoad = 3f;
        private float fallLoad = 4f;
        public PlayerState_Jump(PlayerController parent) : base(parent)
        {
        }

        public override PlayerController.StateID GetID() => PlayerController.StateID.Move;

        public override void Enter(StateMachine<PlayerController.StateID> machine)
        {
            PlayClip(jumpClip);
            parent.Rb2D.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
        }

        public override void Exit(StateMachine<PlayerController.StateID> machine)
        {
        }

        protected override void Act(StateMachine<PlayerController.StateID> machine)
        {
            if (parent.Rb2D.velocity.y <= 0)
            {
                parent.Rb2D.velocity += Vector2.up * (Physics2D.gravity.y * fallLoad * Time.deltaTime);
            }

            // // if rising and space hold down
            // else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            // {
            //     rb.velocity += Vector2.up * (Physics2D.gravity.y * _jumpRiseVelDec * Time.deltaTime);
            // }

            // if rising but space not hold down
            else if (parent.Rb2D.velocity.y > 0)
            {
                parent.Rb2D.velocity += Vector2.up * (float)(Physics2D.gravity.y * jumpLoad * Time.deltaTime);
            }

            if (parent.Inputs.MoveInputValue.x > 0)
            {
                parent.transform.rotation = Quaternion.Euler(0, 0, 0);
                if (parent.Rb2D.velocity.x < _maxMovementVelocity)
                {
                    float speedDifference = Mathf.Abs(_maxMovementVelocity - parent.Rb2D.velocity.x);
                    parent.Rb2D.velocity += new Vector2(speedDifference, 0);
                }
            }
            else if (parent.Inputs.MoveInputValue.x < 0)
            {
                parent.transform.rotation = Quaternion.Euler(0, 180, 0);
                if (parent.Rb2D.velocity.x > -_maxMovementVelocity)
                {
                    float speedDifference = Mathf.Abs(_maxMovementVelocity + parent.Rb2D.velocity.x);
                    parent.Rb2D.velocity += new Vector2(-speedDifference, 0);
                }
            }
        }

        protected override void Decide(StateMachine<PlayerController.StateID> machine)
        {
            if (parent.IsGrounded())
                machine.ChangeState(PlayerController.StateID.Landing);
            if (parent.Inputs.attackSquareActionTriggered && parent.ReadyToAttack)
                machine.ChangeState(PlayerController.StateID.SquareAttack);
            // Switch to fall state when maxHeight reached
        }
    }
}