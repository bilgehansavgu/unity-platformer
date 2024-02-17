using UnityEngine;
using Core.StateMachine;

namespace Core.CharacterController
{
    public class PlayerState_JumpStall : PlayerState_Base
    {
        const string stallClip = "JumpStall";
        private float _maxMovementVelocity = 5f;
        private float jumpLoad = 3f;
        private float fallLoad = 4f;
        public PlayerState_JumpStall(PlayerController parent) : base(parent)
        {
        }

        public override PlayerController.StateID GetID() => PlayerController.StateID.Move;

        public override void Enter(StateMachine<PlayerController.StateID> machine)
        {
            PlayClip(stallClip);
        }

        public override void Exit(StateMachine<PlayerController.StateID> machine)
        {
        }

        protected override void Act(StateMachine<PlayerController.StateID> machine)
        {
      
        }

        protected override void Decide(StateMachine<PlayerController.StateID> machine)
        {  
            if (parent.IsGrounded())
                machine.ChangeState(PlayerController.StateID.Idle);
            if (parent.Inputs.attackSquareActionTriggered && parent.ReadyToAttack)
                machine.ChangeState(PlayerController.StateID.SquareAttack);
            if (parent.Rb2D.velocity.y < 0.2f)
                machine.ChangeState(PlayerController.StateID.Falling);
            // Switch to fall state when maxHeight reached
        }
    }
}
