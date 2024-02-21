using UnityEngine;
using Core.FSM;

namespace Core.CharacterController
{
    public class PlayerState_SquareAttack : PlayerState_Base
    {
        const string attack = "ChainPunch";
        public PlayerState_SquareAttack(PlayerController parent) : base(parent)
        {
            SetCooldown(parent.config.AttackCooldown);
        }
        public override PlayerController.StateID GetID() => PlayerController.StateID.SquareAttack;

        public override void Enter(StateMachine<PlayerController.StateID> machine)
        {
            PlayClip(attack);
            parent.Rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        public override void Exit(StateMachine<PlayerController.StateID> machine)
        {
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
