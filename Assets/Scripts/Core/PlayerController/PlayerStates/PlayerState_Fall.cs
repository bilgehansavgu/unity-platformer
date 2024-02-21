using UnityEngine;
using Core.FSM;

namespace Core.CharacterController
{
    public class PlayerState_Fall : PlayerState_Base
    {
        const string fallClip = "JumpFall";
        private float _maxMovementVelocity = 5f;
        private float fallLoad = 4f;
        public PlayerState_Fall(PlayerController parent) : base(parent)
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
            if (parent.Rb2D.velocity.y <= 0)
            {
                parent.Rb2D.velocity += Vector2.up * (Physics2D.gravity.y * fallLoad * Time.deltaTime);
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
        }

        protected override void Decide(StateMachine<PlayerController.StateID> machine)
        {
            if (parent.IsNearGround())
                machine.ChangeState(PlayerController.StateID.Landing);
            else if (parent.Inputs.AttackSquareActionTriggered)
                machine.ChangeState(PlayerController.StateID.SquareAttack);
            else if (parent.Inputs.AttackTriangleActionTriggered)
                machine.ChangeState(PlayerController.StateID.TriangleAttack);
            else if (parent.Inputs.DashTriggered)
                machine.ChangeState(PlayerController.StateID.Dash);
        }
    }
}
