using UnityEngine;

public class SquareAttackState : MonoBehaviour, IPlayerState
{
    // Reference to player's movement script to check if grounded
    private PlayerMovement playerMovement;
    private Animator animator;
    private Rigidbody2D rb;


    // References to grounded and aerial attack animations
    private string groundedAttackAnimation = "chain_punch_R_animation";
    private string aerialAttackAnimation = "jump_and_ground_slam_R_animation";

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    public void EnterState()
    {
        if (playerMovement.IsGrounded())
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

    public void OnAnimationFinished()
    {
        animator.Play("idle");
    }


    public void UpdateState()
    {
   
    }

    public void ExitState()
    {
        // Cleanup state if needed
    }
}