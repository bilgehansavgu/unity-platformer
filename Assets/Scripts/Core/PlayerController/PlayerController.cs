using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.StateMachine;

namespace Core.CharacterController
{
    [RequireComponent(typeof(CapsuleCollider2D), typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour, IHittable
    {
        public PlayerConfig config;
        [Header("References & Setup")]
        public Animator Animator;
        public Rigidbody2D Rb2D;

        public enum StateID
        {
            Idle,
            Move,
            JumpRise,
            JumpStall,
            Falling,
            Landing,
            SquareAttack,
            TriangleAttack,
            Dash,
            GetHit,
            GetHitAirbourne
        }

        [SerializeField, Space] StateMachine<StateID> fsm;
        private void SetupFSM()
        {
            Dictionary<StateID, IState<StateID>> states = new Dictionary<StateID, IState<StateID>>
            {
                { StateID.Idle, new PlayerState_Idle(this) },
                { StateID.Move, new PlayerState_Move(this) },
                { StateID.JumpRise, new PlayerState_JumpRise(this) },
                { StateID.JumpStall, new PlayerState_JumpStall(this) },
                { StateID.Falling, new PlayerState_Falling(this) },
                { StateID.Landing, new PlayerState_Landing(this) },
                { StateID.SquareAttack, new PlayerState_SquareAttack(this) },
                { StateID.TriangleAttack, new PlayerState_TriangleAttack(this)},
                { StateID.Dash, new PlayerState_Dash(this)}
            };
            fsm = new StateMachine<StateID>(states, StateID.Idle);
        }

        public IInputProvider Inputs;
        private void Awake()
        {
            if (config == null)
            {
                Debug.Log("Player config is missing. PlayerController is disabled");
                this.enabled = false;
            }
            GetReferences();
            SetupFSM();
        }

        private void GetReferences()
        {
            if (Animator == null)
                Animator = GetComponent<Animator>();
            if (Rb2D == null)
                Rb2D = GetComponent<Rigidbody2D>();
            if (Inputs == null)
                Inputs = GetComponent<IInputProvider>();
        }

        private void Update()
        {
            fsm.Tick();
            CountAttackCooldown();
        }

        public bool IsMoving => Inputs.MoveInputValue.x != 0;
        public bool ReadyToAttack => attackCooldown <= 0;

        #region Attack Cooldown
        float attackCooldown = 0;
        private void CountAttackCooldown()
        {
            if (attackCooldown > 0)
                attackCooldown -= Time.deltaTime;
            else
            {
                attackCooldown = 0;
            }
        }
        public void SetAttackCooldown(float cooldown)
        {
            attackCooldown = cooldown;
        }
        #endregion
      
        public bool IsGrounded()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, config.GroundCheckDistance, config.WhatIsGround);
            return hit.collider != null && !hit.collider.isTrigger;
        }
        public bool IsNearGround()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, config.FallCheckDistance, config.WhatIsGround);
            return hit.collider == null;
        }
        public void InvokeState(StateID stateToTrigger)
        {
            fsm.GetState(stateToTrigger).InvokeState(fsm);
        }

        public bool IsInvincible;
        public void TakeHit(float hitForce)
        {
            if (IsInvincible)
                return;

            if (IsGrounded())
                fsm.ChangeState(StateID.GetHit);
            else
                fsm.ChangeState(StateID.GetHitAirbourne);
        }
    }
}
