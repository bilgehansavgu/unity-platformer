using UnityEngine;
using Core.FSM;

namespace Core.CharacterController
{
    public class PlayerState_Dash : PlayerState_Base
    {
        const string dashClip = "Dash";

        public PlayerState_Dash(PlayerController parent) : base(parent)
        {
            SetCooldown(parent.config.DashCooldown);
        }

        public override PlayerController.StateID GetID() => PlayerController.StateID.Dash;

        public override void Enter(StateMachine<PlayerController.StateID> machine)
        {
            // Rigidbody Condition
            parent.IsInvincible = true;
            parent.Rb2D.constraints = RigidbodyConstraints2D.FreezePositionY;
            parent.Rb2D.freezeRotation = true;
            // FX
            PlayClip(dashClip);
            parent.Dust?.Emit(20);
            parent.Dust?.Play();

            // Dashing
            Vector2 dashVector;

            if (parent.SpriteRenderer.flipX)
                dashVector = Vector2.left;
            else
                dashVector = Vector2.right;

            parent.Rb2D.AddForce(dashVector * parent.config.DashForce, ForceMode2D.Impulse);
        }

        public override void Exit(StateMachine<PlayerController.StateID> machine)
        {
            // Rigidbody Condition
            parent.IsInvincible = false;
            parent.Rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            // FX
            parent.Dust?.Stop();
        }

        protected override void Act(StateMachine<PlayerController.StateID> machine)
        {
            
        }

        protected override void Decide(StateMachine<PlayerController.StateID> machine)
        {
            
        }
        public override void InvokeStateTrigger(StateMachine<PlayerController.StateID> machine)
        {
            if (parent.IsGrounded())
                machine.ChangeState(PlayerController.StateID.Idle);
            else
                machine.ChangeState(PlayerController.StateID.Falling);
        }
    }
}