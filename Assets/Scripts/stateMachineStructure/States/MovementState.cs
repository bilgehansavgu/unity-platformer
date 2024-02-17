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
    public TransactionState transactionState;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inputHandler = GetComponent<PlayerStateInputs>();
        animator = GetComponent<Animator>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        transactionState = GetComponent<TransactionState>();

    }

    public void EnterState()
    {
        animator.Play("walk_R_animation");
    }

    public void UpdateState()
    {
        rb.velocity = new Vector2(inputHandler.MoveInputValue.x * moveSpeed, rb.velocity.y);
        
        if (inputHandler.MoveInputValue.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            //FlipPlayer();
        }
        else if (inputHandler.MoveInputValue.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            //FlipPlayer();
        }
        
        if (inputHandler.MoveInputValue.x == 0)
        {
            transactionState.targetState = GetComponent<IdleState>();

           // stateMachine.SetState(GetComponent<IdleState>());
           stateMachine.TransitionToStateWithTransaction();

        }

        if (inputHandler.jumpTriggered)
        {
            transactionState.targetState = GetComponent<JumpState>();

//            stateMachine.SetState(GetComponent<JumpState>());
            stateMachine.TransitionToStateWithTransaction();

        }
        if (inputHandler.attackSquareActionTriggered)
        {
            //stateMachine.SetState(GetComponent<SquareAttackState>());
        }
        if (inputHandler.attackTriangleActionTriggered)
        {
           // stateMachine.SetState(GetComponent<TriangleAttackState>());
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