using UnityEngine;
using Core.StateMachine;

namespace Core.CharacterController
{
    public class PlayerState_Dash : PlayerState_Base
    {
        const string dashClip = "Dash";
        private Vector2 dashDirection;
        public float dashSpeed = 5f; // Dash speed
        public float dashDistance = 10f; // Dash distance

        private float dashDistanceTraveled = 0f; // Track the distance traveled during dash

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
                parent.Rb2D.AddForce(Vector2.right * 14 , ForceMode2D.Impulse);
            }
            else if (parent.Inputs.MoveInputValue.x < 0)
            {
                parent.transform.rotation = Quaternion.Euler(0, 180, 0);
                parent.Rb2D.AddForce(Vector2.left * 14, ForceMode2D.Impulse);
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
            // Check if the player is still grounded
            if (parent.IsGrounded())
                machine.ChangeState(PlayerController.StateID.Falling);
        }
    }
}