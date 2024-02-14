using UnityEngine;
using UnityEngine.InputSystem;

public class MovementState : MonoBehaviour, IPlayerState
{

    private Animator animator;
    private Rigidbody2D rb;
    private bool facingRight = true;

    [SerializeField] private float moveSpeed = 5f;
    public PlayerStateInputs inputHandler;
    public PlayerStateMachine stateMachine;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inputHandler = GetComponent<PlayerStateInputs>();
        animator = GetComponent<Animator>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        
    }

    public void EnterState()
    {
        Debug.Log("MMMMEnterState");
        // Initialize state
        animator.Play("walk_R_animation");

    }

    public void UpdateState()
    {
        Debug.Log("MMMMupdateState");
        rb.velocity = new Vector2(inputHandler.MoveInputValue.x * moveSpeed, rb.velocity.y);
        
        if (inputHandler.MoveInputValue.x > 0 && !facingRight)
        {
            FlipPlayer();
        }
        else if (inputHandler.MoveInputValue.x < 0 && facingRight)
        {
            FlipPlayer();
        }
        else if (inputHandler.MoveInputValue.x == 0)
        {
            stateMachine.SetState(GetComponent<IdleState>());
        }
    }
    public void ExitState()
    {
        Debug.Log("MMMMExitState");

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
}