using UnityEngine;
using Platformer.Core.FSM;

namespace Platformer.Core.CharacterController
{
    public class PlayerState_Move : PlayerState_Base
    {
        const string walkClip = "Walk";
        public PlayerState_Move(PlayerController parent) : base(parent)
        {
        }

        public override PlayerController.StateID GetID() => PlayerController.StateID.Move;

        public override void Enter(StateMachine<PlayerController.StateID> machine)
        {
            PlayClip(walkClip);
            parent.Dust?.Play();
        }

        public override void Exit(StateMachine<PlayerController.StateID> machine)
        {
            parent.Dust?.Stop();
        }

        protected override void Act(StateMachine<PlayerController.StateID> machine)
        {
            HandleMovement(parent.config.GroundModifier);
            HandleSpriteDirection(parent.Inputs.MoveInputValue.x);
        }


        protected override void Decide(StateMachine<PlayerController.StateID> machine)
        {
            if (!parent.IsMoving)
                machine.ChangeState(PlayerController.StateID.Idle);
            else if (parent.Inputs.JumpTriggered)
                machine.ChangeState(PlayerController.StateID.Jump);
            else if (parent.Inputs.DashTriggered)
                machine.ChangeState(PlayerController.StateID.Dash);
            else if (!parent.IsGrounded())
                machine.ChangeState(PlayerController.StateID.Falling);
            else if (parent.Inputs.AttackSquareActionTriggered)
                machine.ChangeState(PlayerController.StateID.SquareAttack);
            else if (parent.Inputs.AttackTriangleActionTriggered)
                machine.ChangeState(PlayerController.StateID.TriangleAttack);
        }

        protected void HandleMovement(float movementModifier)
        {
            if (parent.Inputs.MoveInputValue.x == 0)
                return;

            float maxMoveSpeed = movementModifier * parent.config.MovementSpeed;
            float absoluteRigidbodyVelocityX = Mathf.Abs(parent.Rb2D.velocity.x);

            float difference = maxMoveSpeed - absoluteRigidbodyVelocityX;
            Vector2 direction = new Vector2(parent.Inputs.MoveInputValue.x, 0).normalized * difference;
            if (absoluteRigidbodyVelocityX < maxMoveSpeed)
            {
                parent.Rb2D.velocity += direction;
            }

            // if velocity is bigger then velocity this doesn't work
        }

        //private void Move()
        //{
        //    if (parent.Inputs.MoveInputValue.x > 0)
        //    {
        //        WalkRight();
        //    }
        //    if (parent.Inputs.MoveInputValue.x < 0)
        //    {
        //        WalkLeft();
        //    }
        //}
        //private void WalkRight()
        //{
        //    if (parent.Rb2D.velocity.x < parent.config.MovementSpeed)
        //    {
        //        float speedDifference = Mathf.Abs(parent.config.MovementSpeed - parent.Rb2D.velocity.x);
        //        parent.Rb2D.velocity += new Vector2(parent.Inputs.MoveInputValue.x * speedDifference, 0);
        //    }
        //}
        //private void WalkLeft()
        //{
        //    if (parent.Rb2D.velocity.x > -parent.config.MovementSpeed)
        //    {
        //        float speedDifference = Mathf.Abs(parent.config.MovementSpeed + parent.Rb2D.velocity.x);
        //        parent.Rb2D.velocity += new Vector2(-speedDifference, 0);
        //    }
        //}

    }
}
