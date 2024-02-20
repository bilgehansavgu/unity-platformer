using UnityEngine;
using Core.StateMachine;

namespace Core.CharacterController
{
    public class PlayerState_Dash : PlayerState_Base
    {
        const string dashClip = "Dash";

        public PlayerState_Dash(PlayerController parent) : base(parent)
        {
        }

        public override PlayerController.StateID GetID() => PlayerController.StateID.Dash;

        public override void Enter(StateMachine<PlayerController.StateID> machine)
        {
            parent.IsInvincible = true;
            PlayClip(dashClip);
    
            if (parent.Inputs.MoveInputValue.x >= 0)
            {
                parent.transform.rotation = Quaternion.Euler(0, 0, 0);
                parent.Rb2D.AddForce(Vector2.right * parent.config.DashForce , ForceMode2D.Impulse);
            }
            else if (parent.Inputs.MoveInputValue.x < 0)
            {
                parent.transform.rotation = Quaternion.Euler(0, 180, 0);
                parent.Rb2D.AddForce(Vector2.left * parent.config.DashForce, ForceMode2D.Impulse);
            }
        }

        public override void Exit(StateMachine<PlayerController.StateID> machine)
        {
            parent.IsInvincible = false;
        }

        protected override void Act(StateMachine<PlayerController.StateID> machine)
        {
     
            
        }

        protected override void Decide(StateMachine<PlayerController.StateID> machine)
        {  
 
        }
        public override void InvokeState(StateMachine<PlayerController.StateID> machine)
        {
            if (parent.IsGrounded())
                machine.ChangeState(PlayerController.StateID.Idle);
            else
                machine.ChangeState(PlayerController.StateID.Falling);
        }
    }
}