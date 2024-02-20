using Core.StateMachine;
using UnityEngine;

namespace Core.CharacterController
{
    public class PlayerState_Landing : PlayerState_Base
    {
        const string landingClip = "JumpLand";
        public PlayerState_Landing(PlayerController parent) : base(parent)
        {
        }
        public override PlayerController.StateID GetID() => PlayerController.StateID.Landing;

        public override void Enter(StateMachine<PlayerController.StateID> machine)
        {
            PlayClip(landingClip);
        }

        public override void Exit(StateMachine<PlayerController.StateID> machine)
        {

        }


        protected override void Act(StateMachine<PlayerController.StateID> machine)
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
            if (parent.Inputs.JumpTriggered)
                machine.ChangeState(PlayerController.StateID.Jump);
        }

        public override void InvokeState(StateMachine<PlayerController.StateID> machine)
        {
            if (parent.IsGrounded())
                machine.ChangeState(PlayerController.StateID.Idle);
        }
        
        private void WalkRight()
        {
            if (parent.Rb2D.velocity.x < parent.config.LandingSpeed)
            {
                float speedDifference = Mathf.Abs(parent.config.LandingSpeed - parent.Rb2D.velocity.x);
                parent.Rb2D.velocity += new Vector2(parent.Inputs.MoveInputValue.x * speedDifference, 0);
            }
        }

        private void WalkLeft()
        {
            if (parent.Rb2D.velocity.x > -parent.config.LandingSpeed)
            {
                float speedDifference = Mathf.Abs(parent.config.LandingSpeed + parent.Rb2D.velocity.x);
                parent.Rb2D.velocity += new Vector2(-speedDifference, 0);
            }
        }
        
        
    }
    
}
