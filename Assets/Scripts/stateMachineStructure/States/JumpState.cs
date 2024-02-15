using UnityEngine;

public class JumpState : MonoBehaviour, IPlayerState
{
    private Animator animator;
    private Rigidbody2D rb;
    public PlayerStateMachine stateMachine;
    public PlayerStateInputs inputHandler;
    
    [SerializeField] private float _maxMovementVelocity = 5f;
    [SerializeField] private float jumpForce = 2f;
    
    [SerializeField] private float groundCheckDistance = 0.0001f;
    [SerializeField] private LayerMask groundLayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stateMachine = GetComponent<PlayerStateMachine>();
        inputHandler = GetComponent<PlayerStateInputs>();
    }

    public void EnterState()
    {
        animator.Play("jump_animation");
        rb.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
    }

    public void UpdateState()
    {
        if (IsGrounded())
        {
            stateMachine.SetState(GetComponent<LandingState>());
        }
        
        if (rb.velocity.y <= 0)
        {
            rb.velocity += Vector2.up * (Physics2D.gravity.y * 4 * Time.deltaTime);
        }
        
        // // if rising and space hold down
        // else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        // {
        //     rb.velocity += Vector2.up * (Physics2D.gravity.y * _jumpRiseVelDec * Time.deltaTime);
        // }
        
        // if rising but space not hold down
        else if (rb.velocity.y > 0)
        {
            rb.velocity += Vector2.up * (float)(Physics2D.gravity.y * 3 * Time.deltaTime);
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
    
    public bool IsGrounded()
    {
        // Perform a raycast downward to check for ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        
        // If the raycast hit something and it's not a trigger collider, consider the player grounded
        return hit.collider != null && !hit.collider.isTrigger && rb.velocity.y < 0;
    }
}