using UnityEngine;

public class TriangleAttackState : MonoBehaviour, IPlayerState
{
    private PlayerMovement playerMovement;
    private Animator animator;
    private Rigidbody2D rb;
    public PlayerStateInputs inputHandler;



    // References to grounded and aerial attack animations
    private string groundedAttackAnimation = "cross_punch_R_animation";
    private string aerialAttackAnimation = "light_jab_animation";

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        inputHandler = GetComponent<PlayerStateInputs>();
    }

    public void EnterState()
    {
        inputHandler.SetAttackState(true);
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
        inputHandler.SetAttackState(false);
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