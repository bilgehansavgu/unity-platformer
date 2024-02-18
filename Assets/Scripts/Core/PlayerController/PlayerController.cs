using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.StateMachine;

namespace Core.CharacterController
{
    [RequireComponent(typeof(CapsuleCollider2D), typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        public PlayerStateInputs inputHandler;

        [Header("References & Setup")]
        public Animator Animator;
        public Rigidbody2D Rb2D;
        public float MovementSpeed;
        [Header("Ground Check")]
        [SerializeField] private float groundCheckDistance = 0.55f;
        [SerializeField] private float fallCheckDistance = 7f;

        [SerializeField] private LayerMask groundLayer;

        public enum StateID
        {
            Idle,
            Move,
            AnticipateJump,
            JumpRise,
            JumpStall,
            Falling,
            Landing,
            SquareAttack,
            TriangleAttack
        }

        [SerializeField, Space] StateMachine<StateID> fsm;
        private void SetupFSM()
        {
            Dictionary<StateID, IState<StateID>> states = new Dictionary<StateID, IState<StateID>>
            {
                { StateID.Idle, new PlayerState_Idle(this) },
                { StateID.Move, new PlayerState_Move(this) },
                { StateID.AnticipateJump, new PlayerState_AnticipateJump(this) },
                { StateID.JumpRise, new PlayerState_JumpRise(this) },
                { StateID.JumpStall, new PlayerState_JumpStall(this) },
                { StateID.Falling, new PlayerState_Falling(this) },
                { StateID.Landing, new PlayerState_Landing(this) },
                { StateID.SquareAttack, new PlayerState_SquareAttack(this) },
                { StateID.TriangleAttack, new PlayerState_TriangleAttack(this)}
            };
            fsm = new StateMachine<StateID>(states, StateID.Idle);


            // This is an alternative way to initialize fsm. It looks cleaner but it's easier to make a mistake.
            //fsm = new StateMachine<StateID>();
            //fsm.RegisterState(new PlayerState_Idle(this));
            //fsm.RegisterState(new PlayerState_Move(this));
            //fsm.RegisterState(new PlayerState_AnticipateJump(this));
            //fsm.RegisterState(new PlayerState_Jump(this));
            //fsm.RegisterState(new PlayerState_Falling(this));
            //fsm.RegisterState(new PlayerState_Landing(this));
            //fsm.RegisterState(new PlayerState_SquareAttack(this));
            //fsm.ChangeState(StateID.Idle);
        }

        public PlayerInputData Inputs => inputs;
        [SerializeField] PlayerInputData inputs;
        private void Awake()
        {
            if (Animator == null)
                Animator = GetComponent<Animator>();
            if (Rb2D == null)
                Rb2D = GetComponent<Rigidbody2D>();
            if (inputHandler == null)
                inputHandler = GetComponent<PlayerStateInputs>();
            SetupFSM();
        }

        private void Update()
        {
            inputs.MoveInputValue = inputHandler.MoveInputValue;
            inputs.jumpTriggered = inputHandler.jumpTriggered;
            inputs.attackSquareActionTriggered = inputHandler.attackSquareActionTriggered;
            inputs.attackTriangleActionTriggered = inputHandler.attackTriangleActionTriggered;

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

        /// <summary>
        /// This method is temporary
        /// </summary>
      
        public bool IsGrounded()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
            return hit.collider != null && !hit.collider.isTrigger;
        }
        public bool IsNearGround()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, fallCheckDistance, groundLayer);
            return hit.collider != null;
        }
        public void OnAnimationFinished(StateID stateToTrigger)
        {
            fsm.GetState(stateToTrigger).InvokeState(fsm);
        }
    }
}
