using Platformer.Core.FSM;
using UnityEngine;

namespace Platformer.Core.CharacterController
{
    public class PlayerState_Idle : PlayerState_Base
    {
        const string idleClip = "Idle";
        public PlayerState_Idle(PlayerController parent) : base(parent)
        {
        }

        public override PlayerController.StateID GetID() => PlayerController.StateID.Idle;

        public override void Enter(StateMachine<PlayerController.StateID> machine)
        {
            PlayClip(idleClip);
        }

        public override void Exit(StateMachine<PlayerController.StateID> machine)
        {
        }
        protected override void Act(StateMachine<PlayerController.StateID> machine)
        {
            // Friction
            if (Mathf.Abs(parent.Rb2D.velocity.x) > 0)
            {
                parent.Rb2D.velocity += -parent.config.GroundedFriction * Time.deltaTime * new Vector2(parent.Rb2D.velocity.x, 0);
            }
            // if there is too little movement stop completely
            if (Mathf.Abs(parent.Rb2D.velocity.x) < 0.1)
            {
                parent.Rb2D.velocity = Vector2.zero;
            }
        }

        protected override void Decide(StateMachine<PlayerController.StateID> machine)
        {
            if (parent.IsMoving)
                machine.ChangeState(PlayerController.StateID.Move);
            else if (!parent.IsGrounded())
                machine.ChangeState(PlayerController.StateID.Falling);
            else if (parent.Inputs.JumpTriggered && parent.IsGrounded())
                machine.ChangeState(PlayerController.StateID.Jump);
            else if (parent.Inputs.AttackSquareActionTriggered)
                machine.ChangeState(PlayerController.StateID.SquareAttack);
            else if (parent.Inputs.AttackTriangleActionTriggered)
                machine.ChangeState(PlayerController.StateID.TriangleAttack);
        }
    }
}
