using UnityEngine;
using Core.StateMachine;

namespace Core.CharacterController
{
    public class PlayerState_Move : PlayerState_Base
    {
        const string walkClip = "Run";
        public PlayerState_Move(PlayerController parent) : base(parent)
        {
        }

        public override PlayerController.StateID GetID() => PlayerController.StateID.Move;

        public override void Enter(StateMachine<PlayerController.StateID> machine) => PlayClip(walkClip);


        public override void Exit(StateMachine<PlayerController.StateID> machine)
        {
        }

        protected override void Act(StateMachine<PlayerController.StateID> machine)
        {
            Move();
            HandleSpriteDirection();
        }

        private void Move()
        {
            if (parent.Inputs.MoveInputValue.x > 0)
            {
                WalkRight();
            }
            if (parent.Inputs.MoveInputValue.x < 0)
            {
                WalkLeft();
            }
        }

        protected override void Decide(StateMachine<PlayerController.StateID> machine)
        {
            if (!parent.IsMoving)
                machine.ChangeState(PlayerController.StateID.Idle);
            if (parent.Inputs.JumpTriggered)
                machine.ChangeState(PlayerController.StateID.Jump);
            if (parent.Inputs.AttackSquareActionTriggered && parent.ReadyToAttack)
                machine.ChangeState(PlayerController.StateID.SquareAttack);
            if (parent.Inputs.AttackTriangleActionTriggered && parent.ReadyToAttack)
                machine.ChangeState(PlayerController.StateID.TriangleAttack);
        }

        private void WalkRight()
        {
            if (parent.Rb2D.velocity.x < parent.config.MovementSpeed)
            {
                float speedDifference = Mathf.Abs(parent.config.MovementSpeed - parent.Rb2D.velocity.x);
                parent.Rb2D.velocity += new Vector2(parent.Inputs.MoveInputValue.x * speedDifference, 0);
            }
        }

        private void WalkLeft()
        {
            if (parent.Rb2D.velocity.x > -parent.config.MovementSpeed)
            {
                float speedDifference = Mathf.Abs(parent.config.MovementSpeed + parent.Rb2D.velocity.x);
                parent.Rb2D.velocity += new Vector2(-speedDifference, 0);
            }
        }
    }
}
