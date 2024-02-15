using UnityEngine;

public class LandingState : MonoBehaviour, IPlayerState
{
    
    private Animator animator;
    private Rigidbody2D rb;
    public PlayerStateMachine stateMachine;
    public PlayerStateInputs inputHandler;
    
    [SerializeField] private float _maxMovementVelocity = 5f;
    [SerializeField] private float jumpForce = 2f;

    private bool isAnimationFinished = false;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stateMachine = GetComponent<PlayerStateMachine>();
        inputHandler = GetComponent<PlayerStateInputs>();
    }

    public void EnterState()
    {
        animator.Play("landing_animation");
    }

    public void UpdateState()
    {
        if (isAnimationFinished)
        {
            isAnimationFinished = false;
            if (inputHandler.MoveInputValue.x == 0)
            {
                stateMachine.SetState(GetComponent<IdleState>());
            }
            else
            {
                stateMachine.SetState(GetComponent<MovementState>());
            }
        }
        
        if (inputHandler.MoveInputValue.x > 0)
        {
            if (rb.velocity.x < _maxMovementVelocity)
            {
                float speedDifference = Mathf.Abs(_maxMovementVelocity - rb.velocity.x);
                rb.velocity += new Vector2(speedDifference, 0);
            }
        } else if (inputHandler.MoveInputValue.x < 0)
        {
            if (rb.velocity.x > -_maxMovementVelocity)
            {
                float speedDifference = Mathf.Abs(_maxMovementVelocity + rb.velocity.x);
                rb.velocity += new Vector2(-speedDifference, 0);
            }
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