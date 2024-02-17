using UnityEngine;

public class TriangleAttackState : MonoBehaviour, IPlayerState
{
    private Animator animator;
    private Rigidbody2D rb;
    public PlayerStateInputs inputHandler;
    public PlayerStateMachine stateMachine;
    private string groundedAttackAnimation = "cross_punch_R_animation";
    private string aerialAttackAnimation = "light_jab_animation";

    [SerializeField] private float groundCheckDistance = 1f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask enemyLayer; // Layer mask for enemies
    
    [SerializeField] private float attackWidth = 4f;
    [SerializeField] private float attackHeight = 2f;
    [SerializeField] private Collider2D attackCollider;
    [SerializeField] private float knockbackForce = 10f;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        inputHandler = GetComponent<PlayerStateInputs>();
        stateMachine = GetComponent<PlayerStateMachine>();
    }

    public void EnterState()
    {
        Debug.Log("TriangleAttackState");

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
        CheckForEnemyCollision();
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
    private void CheckForEnemyCollision()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, new Vector2(attackWidth, attackHeight), 0, Vector2.zero, 0, enemyLayer);
        foreach (RaycastHit2D hit in hits)
        {
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(0);
            }
        }
    }
}