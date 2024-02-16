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
        [SerializeField] private float groundCheckDistance = 1f;
        [SerializeField] private LayerMask groundLayer;

        public enum StateID
        {
            Idle,
            Move,
            Jump,
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
                { StateID.Jump, new PlayerState_Jump(this) },
                { StateID.SquareAttack, new PlayerState_SquareAttack(this) }
            };
            fsm = new StateMachine<StateID>(states, new PlayerState_Any(fsm), StateID.Idle);
        }
        public bool IsMoving => Inputs.MoveInputValue.x != 0;
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
            GetInputs();
            fsm.Tick();
        }

        private void GetInputs()
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
            fsm.GetState(stateToTrigger).Exit(fsm);
        }
    }
    public class PlayerState_Idle : PlayerState_Base
    {
        const string idleClip = "idle";
        public PlayerState_Idle(PlayerController parent) : base(parent)
        {
        }

        public override PlayerController.StateID GetID() => PlayerController.StateID.Idle;

        public override void Enter(StateMachine<PlayerController.StateID> machine) => PlayClip(idleClip);

        public override void Exit(StateMachine<PlayerController.StateID> machine)
        {
        }
        protected override void Act(StateMachine<PlayerController.StateID> machine)
        {
        }

        protected override void Decide(StateMachine<PlayerController.StateID> machine)
        {
            if (parent.IsMoving)
                machine.ChangeState(PlayerController.StateID.Move);
            if (parent.Inputs.jumpTriggered)
                machine.ChangeState(PlayerController.StateID.Jump);
            if (parent.Inputs.attackSquareActionTriggered)
                machine.ChangeState(PlayerController.StateID.SquareAttack);
        }
    }
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
        }
    }
    public class PlayerState_Jump : PlayerState_Base
    {
        const string jumpClip = "jump_animation";
        private float _maxMovementVelocity = 5f;
        private float jumpLoad = 3f;
        private float fallLoad = 4f;
        public PlayerState_Jump(PlayerController parent) : base(parent)
        {
        }

        public override PlayerController.StateID GetID() => PlayerController.StateID.Move;

        public override void Enter(StateMachine<PlayerController.StateID> machine)
        {
            PlayClip(jumpClip);
            parent.Rb2D.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
        }

        public override void Exit(StateMachine<PlayerController.StateID> machine)
        {
        }

        protected override void Act(StateMachine<PlayerController.StateID> machine)
        {
            if (parent.Rb2D.velocity.y <= 0)
            {
                parent.Rb2D.velocity += Vector2.up * (Physics2D.gravity.y * fallLoad * Time.deltaTime);
            }

            // // if rising and space hold down
            // else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            // {
            //     rb.velocity += Vector2.up * (Physics2D.gravity.y * _jumpRiseVelDec * Time.deltaTime);
            // }

            // if rising but space not hold down
            else if (parent.Rb2D.velocity.y > 0)
            {
                parent.Rb2D.velocity += Vector2.up * (float)(Physics2D.gravity.y * jumpLoad * Time.deltaTime);
            }

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
            if (parent.IsGrounded())
                machine.ChangeState(PlayerController.StateID.Landing);
        }
    }
    public class PlayerState_Landing : PlayerState_Base
    {
        const string landingClip = "landing_animation";
        public PlayerState_Landing(PlayerController parent) : base(parent)
        {
        }
        public override PlayerController.StateID GetID() => PlayerController.StateID.Landing;

        public override void Enter(StateMachine<PlayerController.StateID> machine) => PlayClip(landingClip);

        public override void Exit(StateMachine<PlayerController.StateID> machine)
        {
            if (parent.IsMoving)
                machine.ChangeStateImmediate(PlayerController.StateID.Move);
            else
                machine.ChangeStateImmediate(PlayerController.StateID.Idle);
        }


        protected override void Act(StateMachine<PlayerController.StateID> machine)
        {
        }
        float counter;
        protected override void Decide(StateMachine<PlayerController.StateID> machine)
        {
        }
    }
    public class PlayerState_SquareAttack : PlayerState_Base
    {
        const string attack = "chain_punch_R_animation";
        public PlayerState_SquareAttack(PlayerController parent) : base(parent)
        {
        }
        public override PlayerController.StateID GetID() => PlayerController.StateID.SquareAttack;

        public override void Enter(StateMachine<PlayerController.StateID> machine) => PlayClip(attack);

        public override void Exit(StateMachine<PlayerController.StateID> machine)
        {
        }



        protected override void Act(StateMachine<PlayerController.StateID> machine)
        {
        }

        protected override void Decide(StateMachine<PlayerController.StateID> machine)
        {
        }
    }
}
