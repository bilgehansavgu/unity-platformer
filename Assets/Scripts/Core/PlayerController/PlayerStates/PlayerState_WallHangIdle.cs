using UnityEngine;

    public class PlayerState_WallHangIdle : PlayerState_Base
    {
        const string wallHangClip = "WallHangIdle";
        public PlayerState_WallHangIdle(PlayerController parent) : base(parent)
        {
        }

        public override PlayerController.StateID GetID() => PlayerController.StateID.WallHangIdle;

        public override void Enter(StateMachine<PlayerController.StateID> machine)
        {
            PlayClip(wallHangClip);
        }

        public override void Exit(StateMachine<PlayerController.StateID> machine)
        {
        }

        protected override void Act(StateMachine<PlayerController.StateID> machine)
        {
            if (parent.Rb.velocity.y < 0)
            {
                parent.Rb.velocity += Vector2.up * (float)(-Physics2D.gravity.y  * Time.deltaTime);

            }
        }

        protected override void Decide(StateMachine<PlayerController.StateID> machine)
        {
            if (parent.IsGrounded())
                machine.ChangeState(PlayerController.StateID.Idle);
            if (parent.Inputs.DashTriggered)
                machine.ChangeState(PlayerController.StateID.WallSlide);
            if (parent.Inputs.JumpTriggered)
                machine.ChangeState(PlayerController.StateID.WallJump);
        }
        public override void InvokeState(StateMachine<PlayerController.StateID> machine)
        {


        }
    }