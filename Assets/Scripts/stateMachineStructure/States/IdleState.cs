using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : MonoBehaviour, IPlayerState
{
    private Animator animator;
    private Rigidbody2D rb;
    public PlayerStateInputs inputHandler;
    public PlayerStateMachine stateMachine;
    public SquareAttackState squareAttack;
    public TransactionState transactionState;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        inputHandler = GetComponent<PlayerStateInputs>();
        stateMachine = GetComponent<PlayerStateMachine>();
        squareAttack = GetComponent<SquareAttackState>();
        transactionState = GetComponent<TransactionState>();
    }

    public void EnterState()
    {
        Debug.Log("IdleEnterState");
       // animator.Play("idle");
    }

    public void UpdateState()
    {
        if (inputHandler.MoveInputValue.x != 0)
        {
            transactionState.targetState = GetComponent<MovementState>();
            // Transition to transaction state first, then to movement state
            stateMachine.TransitionToStateWithTransaction();
        }

        if (inputHandler.jumpTriggered)
        {
            transactionState.targetState = GetComponent<JumpState>();

            // Transition to transaction state first, then to jump state
            stateMachine.TransitionToStateWithTransaction();
        }

        if (inputHandler.attackSquareActionTriggered && !squareAttack.isAttacking)
        {
            transactionState.targetState = GetComponent<SquareAttackState>();

            // Transition to transaction state first, then to square attack state
            stateMachine.TransitionToStateWithTransaction();
        }

        if (inputHandler.attackTriangleActionTriggered)
        {
            transactionState.targetState = GetComponent<TriangleAttackState>();

            // Transition to transaction state first, then to triangle attack state
            stateMachine.TransitionToStateWithTransaction();
        }
    }

    public void ExitState()
    {

    }
}
