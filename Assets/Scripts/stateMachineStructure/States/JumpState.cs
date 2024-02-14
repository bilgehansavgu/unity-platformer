using UnityEngine;

public class JumpState : MonoBehaviour, IPlayerState
{
    private Animator animator;
    private Rigidbody2D rb;
    public PlayerStateMachine stateMachine;
    public PlayerStateInputs inputHandler;
    
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    
    [SerializeField] private float groundCheckDistance = 1f;
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
        //animator.Play("");
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void UpdateState()
    {
        if (IsGrounded())
        {
            if (rb.velocity.x == 0)
            {
                stateMachine.SetState(GetComponent<IdleState>());
            }
            else
            {
                stateMachine.SetState(GetComponent<MovementState>());
            }
        }
        rb.velocity = new Vector2(inputHandler.MoveInputValue.x * moveSpeed, rb.velocity.y);
    }
    
    public void ExitState()
    {

    }
    
    public bool IsGrounded()
    {
        // Perform a raycast downward to check for ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        
        // If the raycast hit something and it's not a trigger collider, consider the player grounded
        return hit.collider != null && !hit.collider.isTrigger;
    }
}