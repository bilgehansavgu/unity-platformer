using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.StateMachine;

namespace Core.CharacterController
{
    [RequireComponent(typeof(CapsuleCollider2D), typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [Header("References & Setup")]
        public Animator Animator;
        public Rigidbody2D Rb2D;
        public float MovementSpeed;
        [Header("Ground Check")]
        [SerializeField] private float groundCheckDistance = 1f;
        [SerializeField] private LayerMask groundLayer;

        public enum StateID
        {
            Idle,
            Move,
            AnticipateJump,
            Jump,
            Falling,
            Landing,
            SquareAttack
        }

        [SerializeField, Space] StateMachine<StateID> fsm;
        private void SetupFSM()
        {
            Dictionary<StateID, IState<StateID>> states = new Dictionary<StateID, IState<StateID>>
            {
                { StateID.Idle, new PlayerState_Idle(this) },
                { StateID.Move, new PlayerState_Move(this) },
                { StateID.AnticipateJump, new PlayerState_AnticipateJump(this) },
                { StateID.Jump, new PlayerState_Jump(this) },
                { StateID.Falling, new PlayerState_Falling(this) },
                { StateID.Landing, new PlayerState_Landing(this) },
                { StateID.SquareAttack, new PlayerState_SquareAttack(this) }
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
            SetupFSM();
        }

        private void Update()
        {
            CacheInputs();
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
        private void CacheInputs()
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                inputs.MoveInputValue.x = Input.GetAxis("Horizontal");
            else
                inputs.MoveInputValue.x = 0;

            inputs.jumpTriggered = Input.GetKey(KeyCode.Space);
            inputs.attackSquareActionTriggered = Input.GetKey(KeyCode.C);
        }
        public bool IsGrounded()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
            return hit.collider != null && !hit.collider.isTrigger;
        }
        public void OnAnimationFinished(StateID stateToTrigger)
        {
            fsm.GetState(stateToTrigger).InvokeState(fsm);
        }
    }
}
