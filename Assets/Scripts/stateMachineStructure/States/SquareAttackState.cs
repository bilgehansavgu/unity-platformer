using UnityEngine;

public class SquareAttackState : MonoBehaviour, IPlayerState
{
    // Reference to player's movement script to check if grounded

    private Animator animator;
    private Rigidbody2D rb;


    // References to grounded and aerial attack animations
    private string groundedAttackAnimation = "chain_punch_R_animation";
    private string aerialAttackAnimation = "jump_and_ground_slam_R_animation";
    
    [SerializeField] private float groundCheckDistance = 1f; // Distance to check for ground
    [SerializeField] private LayerMask groundLayer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    public void EnterState()
    {
        if (IsGrounded())
        {
            // Perform grounded attack action
            Debug.Log("Performing grounded square attack");
            animator.Play(groundedAttackAnimation);
        }
        else
        {
            // Perform aerial attack action
            Debug.Log("Performing aerial square attack");
            animator.Play(aerialAttackAnimation);
            rb.AddForce(Vector2.down * 8, ForceMode2D.Impulse);

        }

    }


    public void UpdateState()
    {
   
    }

    public void ExitState()
    {
        // Cleanup state if needed
    }
    
    public bool IsGrounded()
    {
        // Perform a raycast downward to check for ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        
        // If the raycast hit something and it's not a trigger collider, consider the player grounded
        return hit.collider != null && !hit.collider.isTrigger;
    }
}