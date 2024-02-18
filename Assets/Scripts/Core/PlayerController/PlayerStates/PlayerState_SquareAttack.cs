using UnityEngine;
using Core.StateMachine;

namespace Core.CharacterController
{
    public class PlayerState_SquareAttack : PlayerState_Base
    {
        const string attack = "ChainPunch";
        public PlayerState_SquareAttack(PlayerController parent) : base(parent)
        {
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
            parent.SetAttackCooldown(0.7f);
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
        }
    }
}
