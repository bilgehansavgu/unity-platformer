using UnityEngine;

public class MovementState : MonoBehaviour, IPlayerState
{
    private PlayerMovement playerMovement;
    private Animator animator;
    private Rigidbody2D rb;
    private bool facingRight = true;

    [SerializeField] private float moveSpeed = 5f;
    public PlayerStateInputs inputHandler;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inputHandler = GetComponent<PlayerStateInputs>();
        animator = GetComponent<Animator>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void EnterState()
    {
        // Initialize state
        animator.Play("walk_R_animation");
    }

    public void UpdateState()
    {
        if (inputHandler == null)
        {
            Debug.LogError("PlayerInputHandler not found!");
            return;
        }
        Vector2 moveInput = inputHandler.MoveInputValue;
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        
        if (moveInput.x > 0 && !facingRight)
        {
            FlipPlayer();
        }
        else if (moveInput.x < 0 && facingRight)
        {
            FlipPlayer();
        }
        else if (moveInput.x == 0)
        {
            animator.Play("idle");
        }
    }
    public void OnAnimationFinished()
    {
        animator.Play("idle");
    }
    private void FlipPlayer()
    {
        facingRight = !facingRight; // Toggle facing direction

        // Calculate the new rotation
        Vector3 newRotation = transform.eulerAngles;

        // Flip around the Y-axis (180 degrees)
        newRotation.y += 180f;

        // Apply the new rotation
        transform.eulerAngles = newRotation;
    }
    public void ExitState()
    {
        // Clean up state
    }
}