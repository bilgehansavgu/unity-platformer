﻿using UnityEngine;
using Core.StateMachine;

namespace Core.CharacterController
{
    public class PlayerState_Move : PlayerState_Base
    {
        const string walkClip = "walk_R_animation";
        public PlayerState_Move(PlayerController parent) : base(parent)
        {
        }

        public override PlayerController.StateID GetID() => PlayerController.StateID.Move;

        public override void Enter(StateMachine<PlayerController.StateID> machine) => PlayClip(walkClip);


        public override void Exit(StateMachine<PlayerController.StateID> machine)
        {
        }

        protected override void Act(StateMachine<PlayerController.StateID> machine)
        {
            Move();
            HandleSpriteDirection();
        }

        private void Move()
        {
            parent.Rb2D.velocity = new Vector2(parent.Inputs.MoveInputValue.x * parent.MovementSpeed, parent.Rb2D.velocity.y);
        }

        protected override void Decide(StateMachine<PlayerController.StateID> machine)
        {
            if (!parent.IsMoving)
                machine.ChangeState(PlayerController.StateID.Idle);
            if (parent.Inputs.jumpTriggered)
                machine.ChangeState(PlayerController.StateID.Jump);
            if (parent.Inputs.attackSquareActionTriggered && parent.ReadyToAttack)
                machine.ChangeState(PlayerController.StateID.SquareAttack);
            if (!parent.IsGrounded() && !parent.Inputs.jumpTriggered)
                machine.ChangeState(PlayerController.StateID.Falling);
        }
    }
}