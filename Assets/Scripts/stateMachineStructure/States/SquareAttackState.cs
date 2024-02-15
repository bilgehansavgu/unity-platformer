using UnityEngine;

public class SquareAttackState : MonoBehaviour, IPlayerState
{


    private Animator animator;
    private Rigidbody2D rb;
    public PlayerStateMachine stateMachine;
    public PlayerStateInputs inputHandler;

    
    private string groundedAttackAnimation = "chain_punch_R_animation";
    private string aerialAttackAnimation = "jump_and_ground_slam_R_animation";
    
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
        Debug.Log("SquareAttackState");

        if (IsGrounded())
        {
            animator.Play(groundedAttackAnimation);
        }
        else
        {
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        return hit.collider != null && !hit.collider.isTrigger;
    }
}