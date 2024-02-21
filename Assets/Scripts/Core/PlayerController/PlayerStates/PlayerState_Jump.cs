using System;
using UnityEngine;
using Platformer.Core.FSM;
using UnityEngine.Animations;

namespace Platformer.Core.CharacterController
{
    public class PlayerState_Jump : PlayerState_Base
    {
        const string jumpClip = "Jump";
        private const string fallClip = "JumpFall";
        private float _maxMovementVelocity = 5f;
        private float _jumpVelocity = 2f;
        private float jumpLoad = 3f;
        private float fallLoad = 4f;
        
        private float maxSpeed = 5f;
        private float minSpeed = 50f;
        
        private float _maxFallSpeed = -15f;

        private int _jumpCount;
        public PlayerState_Jump(PlayerController parent) : base(parent)
        {
        }

        public override PlayerController.StateID GetID() => PlayerController.StateID.Move;

        public override void Enter(StateMachine<PlayerController.StateID> machine)
        {
            parent.Rb2D.velocity += new Vector2(0,12);
            _jumpCount = 2;
            parent.Dust?.Emit(25);
        }

        public override void Exit(StateMachine<PlayerController.StateID> machine)
        {
            //Debug.Log("Max Speed: " + maxSpeed);
            //Debug.Log("Min Speed: " + minSpeed);
        }

        protected override void Act(StateMachine<PlayerController.StateID> machine)
        {
            if (parent.Inputs.JumpTriggered && _jumpCount > 0)
            {
                parent.Rb2D.velocity += new Vector2(0, _jumpVelocity);
                _jumpCount--;
            }
            HandleSpriteDirection(parent.Inputs.MoveInputValue.x);
            if (parent.Inputs.MoveInputValue.x > 0)
            {
                if (parent.Rb2D.velocity.x < _maxMovementVelocity)
                {
                    float speedDifference = Mathf.Abs(_maxMovementVelocity - parent.Rb2D.velocity.x);
                    parent.Rb2D.velocity += new Vector2(speedDifference, 0);
                }
            }
            else if (parent.Inputs.MoveInputValue.x < 0)
            {
                if (parent.Rb2D.velocity.x > -_maxMovementVelocity)
                {
                    float speedDifference = Mathf.Abs(_maxMovementVelocity + parent.Rb2D.velocity.x);
                    parent.Rb2D.velocity += new Vector2(-speedDifference, 0);
                }
            }

            if (parent.Rb2D.velocity.y <= 0)
            {
                parent.Rb2D.velocity += Vector2.up * (Physics2D.gravity.y * fallLoad * Time.deltaTime);
            }
        
            // // // if rising and space hold down
            // else if (parent.Rb2D.velocity.y > 0 && !Input.GetButton("Jump"))
            // {
            //     parent.Rb2D.velocity += Vector2.up * (Physics2D.gravity.y * fallLoad * 0.5f * Time.deltaTime);
            // }
        
            // if rising but space not hold down
            else if (parent.Rb2D.velocity.y > 0)
            {
                parent.Rb2D.velocity += Vector2.up * (float)(Physics2D.gravity.y * jumpLoad * Time.deltaTime);
            }

            
            if (parent.Rb2D.velocity.y > maxSpeed)
            {
                maxSpeed = parent.Rb2D.velocity.y;
            }
            if (parent.Rb2D.velocity.y < minSpeed)
            {
                minSpeed = parent.Rb2D.velocity.y;
            }
            
            //9.35
            //Debug.Log(parent.GetAirSprite(9));
            
            if (parent.Rb2D.velocity.y < -13)
                PlayClip(fallClip);
            else
            {
                PlayClip(jumpClip, parent.GetAirSprite(9), 9);
            }
            
            if (parent.Rb2D.velocity.y < _maxFallSpeed)
                parent.Rb2D.velocity += new Vector2(0, Mathf.Abs(parent.Rb2D.velocity.y - _maxFallSpeed));
        }

        protected override void Decide(StateMachine<PlayerController.StateID> machine)
        {  
            if (parent.Inputs.AttackSquareActionTriggered)
                machine.ChangeState(PlayerController.StateID.SquareAttack);
            else if (parent.Inputs.AttackTriangleActionTriggered)
                machine.ChangeState(PlayerController.StateID.TriangleAttack);
            else if (parent.Inputs.DashTriggered)
                machine.ChangeState(PlayerController.StateID.Dash);
            else if (parent.IsGrounded() && parent.Rb2D.velocity.y < 0)
                machine.ChangeState(PlayerController.StateID.Landing);
        }
    }
}
