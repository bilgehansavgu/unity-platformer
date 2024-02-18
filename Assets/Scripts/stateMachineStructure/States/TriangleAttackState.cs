using UnityEngine;
using UnityEngine.Serialization;

public class TriangleAttackState : MonoBehaviour, IPlayerState
{

    private Animator animator;
    private Rigidbody2D rb;
    [FormerlySerializedAs("inputHandler")] public PlayerStateInputs_old inputOldHandler;
    public PlayerStateMachine stateMachine;
    
    [SerializeField] private float groundCheckDistance = 1f; // Distance to check for ground
    [SerializeField] private LayerMask groundLayer;
    
    // References to grounded and aerial attack animations
    private string groundedAttackAnimation = "cross_punch_R_animation";
    private string aerialAttackAnimation = "light_jab_animation";

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        inputOldHandler = GetComponent<PlayerStateInputs_old>();
        stateMachine = GetComponent<PlayerStateMachine>();
    }

    public void EnterState()
    {
        Debug.Log("TriangleAttackState");

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
        
    }
    public void OnAnimationFinished()
    {
        Debug.Log("Square Attack finised.");
        stateMachine.SetState(GetComponent<IdleState>());
    }
    public bool IsGrounded()
    {
        // Perform a raycast downward to check for ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        
        // If the raycast hit something and it's not a trigger collider, consider the player grounded
        return hit.collider != null && !hit.collider.isTrigger;
    }
}