using UnityEngine;
using UnityEngine.Serialization;

public class SquareAttackState : MonoBehaviour, IPlayerState
{
    private Animator animator;
    private Rigidbody2D rb;
    public PlayerStateMachine stateMachine;
    [FormerlySerializedAs("inputHandler")] public PlayerStateInputs_old inputOldHandler;

    private string groundedAttackAnimation = "ChainPunch";
    private string aerialAttackAnimation = "SlamPunch";

    [SerializeField] private float groundCheckDistance = 1f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask enemyLayer; // Layer mask for enemies
    
    [SerializeField] private float attackWidth = 4f;
    [SerializeField] private float attackHeight = 2f;
    [SerializeField] private Collider2D attackCollider;
    [SerializeField] private float knockbackForce = 10f;

    


    private bool hasDealtDamage = false; // Flag to track if damage has been dealt

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stateMachine = GetComponent<PlayerStateMachine>();
        inputOldHandler = GetComponent<PlayerStateInputs_old>();
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
            Rigidbody2D enemyRb = other.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                enemyRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }
    public void OnAnimationFinished()
    {
        Debug.Log("Square Attack finished.");
        attackCollider.enabled = false;
        stateMachine.SetState(GetComponent<IdleState>());
    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        return hit.collider != null && !hit.collider.isTrigger;
    }
}