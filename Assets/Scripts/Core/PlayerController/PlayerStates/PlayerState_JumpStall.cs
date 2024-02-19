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

        bool hasExitTime;
        public PlayerState_JumpStall(PlayerController parent) : base(parent)
        {
        }

        public override PlayerController.StateID GetID() => PlayerController.StateID.JumpStall;

        public override void Enter(StateMachine<PlayerController.StateID> machine)
        {
            PlayClip(stallClip);
        }

        public override void Exit(StateMachine<PlayerController.StateID> machine)
        {
        }

        protected override void Act(StateMachine<PlayerController.StateID> machine)
        {
            if (parent.Inputs.MoveInputValue.x > 0)
            {
                parent.transform.rotation = Quaternion.Euler(0, 0, 0);
                if (parent.Rb2D.velocity.x < _maxMovementVelocity)
                {
                    float speedDifference = Mathf.Abs(_maxMovementVelocity - parent.Rb2D.velocity.x);
                    parent.Rb2D.velocity += new Vector2(speedDifference, 0);
                }
            }
            else if (parent.Inputs.MoveInputValue.x < 0)
            {
                parent.transform.rotation = Quaternion.Euler(0, 180, 0);
                if (parent.Rb2D.velocity.x > -_maxMovementVelocity)
                {
                    float speedDifference = Mathf.Abs(_maxMovementVelocity + parent.Rb2D.velocity.x);
                    parent.Rb2D.velocity += new Vector2(-speedDifference, 0);
                }
            }
        }

        protected override void Decide(StateMachine<PlayerController.StateID> machine)
        {
            if (parent.Inputs.AttackSquareActionTriggered && parent.ReadyToAttack)
                machine.ChangeState(PlayerController.StateID.SquareAttack);
            if (parent.Inputs.AttackTriangleActionTriggered && parent.ReadyToAttack)
                machine.ChangeState(PlayerController.StateID.TriangleAttack);
            if (parent.Inputs.DashTriggered)
                machine.ChangeState(PlayerController.StateID.Dash);
            if (parent.IsGrounded())
                machine.ChangeState(PlayerController.StateID.Idle);
        }
        public override void InvokeState(StateMachine<PlayerController.StateID> machine)
        {
            if (machine.CompareState(GetID()) && !parent.IsNearGround())
                machine.ChangeState(PlayerController.StateID.Falling);
        }
    }
}
