using Platformer.Core.FSM;

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
            if (parent.Rb2D.velocity.x != 0)
            {
                
            }
        }

        protected override void Decide(StateMachine<PlayerController.StateID> machine)
        {
            if (parent.IsMoving)
                machine.ChangeState(PlayerController.StateID.Move);
            else if (parent.Inputs.JumpTriggered && parent.IsGrounded())
                machine.ChangeState(PlayerController.StateID.Jump);
            else if (parent.Inputs.AttackSquareActionTriggered)
                machine.ChangeState(PlayerController.StateID.SquareAttack);
            else if (parent.Inputs.AttackTriangleActionTriggered)
                machine.ChangeState(PlayerController.StateID.TriangleAttack);
        }
    }
}
