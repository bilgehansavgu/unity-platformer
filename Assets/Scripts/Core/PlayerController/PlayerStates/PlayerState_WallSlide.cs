using UnityEngine;
using Core.StateMachine;

namespace Core.CharacterController
{
    public class PlayerState_WallSlide : PlayerState_Base
    {
        const string wallSlideClip = "WallHangFall";

        private bool isWallSliding;
        private float wallSlidingSpeed = 2f;
        private float _maxMovementVelocity = 5f;

        public PlayerState_WallSlide(PlayerController parent) : base(parent)
        {
        }

        public override PlayerController.StateID GetID() => PlayerController.StateID.WallSlide;

        public override void Enter(StateMachine<PlayerController.StateID> machine)
        {
            PlayClip(wallSlideClip);
        }

        public override void Exit(StateMachine<PlayerController.StateID> machine)
        {
        }

        protected override void Act(StateMachine<PlayerController.StateID> machine)
        {
            parent.Rb2D.velocity += Vector2.up * (float)(-Physics2D.gravity.y/8  * Time.deltaTime);

        }

        protected override void Decide(StateMachine<PlayerController.StateID> machine)
        {  
            if (!parent.IsNearGround())
                machine.ChangeState(PlayerController.StateID.Landing);
            if (parent.IsGrounded())
                machine.ChangeState(PlayerController.StateID.Idle);
            if (parent.Inputs.JumpTriggered)
                machine.ChangeState(PlayerController.StateID.Jump);
        }
    }
}