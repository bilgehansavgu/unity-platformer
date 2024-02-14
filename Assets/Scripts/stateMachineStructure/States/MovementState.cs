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
        animator.Play("walk_R_animation");
    }

    public void UpdateState()
    {
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

        if (inputHandler.jumpTriggered)
        {
            stateMachine.SetState(GetComponent<JumpState>());
        }
    }
    public void ExitState()
    {

    }

    private void FlipPlayer()
    {
        facingRight = !facingRight;

        Vector3 newRotation = transform.eulerAngles;
        
        newRotation.y += 180f;
        
        transform.eulerAngles = newRotation;
    }
}