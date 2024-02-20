using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class MovementState : MonoBehaviour, IPlayerState
{

    private Animator animator;
    private Rigidbody2D rb;
    private bool facingRight = true;

    [SerializeField] private float moveSpeed = 5f;
    [FormerlySerializedAs("inputHandler")] public PlayerStateInputs_old inputOldHandler;
    public PlayerStateMachine stateMachine;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inputOldHandler = GetComponent<PlayerStateInputs_old>();
        animator = GetComponent<Animator>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void EnterState()
    {
        animator.Play("Walk");
    }

    public void UpdateState()
    {
        rb.velocity = new Vector2(inputOldHandler.MoveInputValue.x * moveSpeed, rb.velocity.y);
        
        if (inputOldHandler.MoveInputValue.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            //FlipPlayer();
        }
        else if (inputOldHandler.MoveInputValue.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            //FlipPlayer();
        }
        
        if (inputOldHandler.MoveInputValue.x == 0)
        {
            stateMachine.SetState(GetComponent<IdleState>());
        }
        if (inputOldHandler.jumpTriggered)
        {
            stateMachine.SetState(GetComponent<JumpState>());
        }
        if (inputOldHandler.attackSquareActionTriggered)
        {
            stateMachine.SetState(GetComponent<SquareAttackState>());
        }
        if (inputOldHandler.attackTriangleActionTriggered)
        {
            stateMachine.SetState(GetComponent<TriangleAttackState>());
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