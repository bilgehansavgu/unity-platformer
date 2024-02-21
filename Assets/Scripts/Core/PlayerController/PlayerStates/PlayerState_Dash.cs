using UnityEngine;
using Platformer.Core.FSM;

namespace Platformer.Core.CharacterController
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
            parent.IsInvincible = true;
            PlayClip(dashClip);
            parent.Dust?.Emit(20);
            parent.Dust?.Play();
            parent.Rb2D.gravityScale = 0f;
            HandleSpriteDirection(parent.Rb2D.velocity.x);
            parent.Rb2D.constraints = RigidbodyConstraints2D.FreezePositionY;
            parent.Rb2D.freezeRotation = true;

            if (parent.transform.localScale.x > 0)
            {
                parent.Rb2D.AddForce(Vector2.right * parent.config.DashForce , ForceMode2D.Impulse);
            }
            else if (parent.transform.localScale.x < 0)
            {
                parent.Rb2D.AddForce(Vector2.left * parent.config.DashForce, ForceMode2D.Impulse);
            }
        }

        public override void Exit(StateMachine<PlayerController.StateID> machine)
        {
            parent.Dust?.Stop();
            parent.Rb2D.gravityScale = 1f;
            parent.IsInvincible = false;
            parent.Rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
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