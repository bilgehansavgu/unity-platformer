using UnityEngine;
using UnityEngine.Serialization;

public class LandingState : MonoBehaviour, IPlayerState
{
    
    private Animator animator;
    private Rigidbody2D rb;
    public PlayerStateMachine stateMachine;
    [FormerlySerializedAs("inputHandler")] public PlayerStateInputs_old inputOldHandler;
    
    [SerializeField] private float _maxMovementVelocity = 5f;
    [SerializeField] private float jumpForce = 2f;

    private bool isAnimationFinished = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stateMachine = GetComponent<PlayerStateMachine>();
        inputOldHandler = GetComponent<PlayerStateInputs_old>();
    }

    public void EnterState()
    {
        animator.Play("JumpLand");
    }

    public void UpdateState()
    {
        if (isAnimationFinished)
        {
            isAnimationFinished = false;
            if (inputOldHandler.MoveInputValue.x == 0)
            {
                stateMachine.SetState(GetComponent<IdleState>());
            }
            else
            {
                stateMachine.SetState(GetComponent<MovementState>());
            }
        }
        
        if (inputOldHandler.MoveInputValue.x > 0)
        {
            if (rb.velocity.x < _maxMovementVelocity)
            {
                float speedDifference = Mathf.Abs(_maxMovementVelocity - rb.velocity.x);
                rb.velocity += new Vector2(speedDifference, 0);
            }
        } else if (inputOldHandler.MoveInputValue.x < 0)
        {
            if (rb.velocity.x > -_maxMovementVelocity)
            {
                float speedDifference = Mathf.Abs(_maxMovementVelocity + rb.velocity.x);
                rb.velocity += new Vector2(-speedDifference, 0);
            }
        }
        if (inputOldHandler.attackSquareActionTriggered)
        {
            stateMachine.SetState(GetComponent<SquareAttackState>());
        }
        if (inputOldHandler.attackTriangleActionTriggered)
        {
            stateMachine.SetState(GetComponent<TriangleAttackState>());
        }
    }

    public void ExitState()
    {

    }

    public void onAnimationFinished()
    {
        isAnimationFinished = true;
    }
}