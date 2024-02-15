using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : MonoBehaviour, IPlayerState
{
    private Animator animator;
    private Rigidbody2D rb;
    public PlayerStateInputs inputHandler;
    public PlayerStateMachine stateMachine;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        inputHandler = GetComponent<PlayerStateInputs>();
        stateMachine = GetComponent<PlayerStateMachine>();
    }

    public void EnterState()
    {
        Debug.Log("IdleEnterState");
        animator.Play("idle");
    }
    

    public void UpdateState()
    {
        if (inputHandler.MoveInputValue.x != 0)
        {
            stateMachine.SetState(GetComponent<MovementState>());
        }

        if (inputHandler.jumpTriggered)
        {
            stateMachine.SetState(GetComponent<JumpState>());
        }

        if (inputHandler.attackSquareActionTriggered)
        {
            stateMachine.SetState(GetComponent<SquareAttackState>());
        }
        if (inputHandler.attackTriangleActionTriggered)
        {
            stateMachine.SetState(GetComponent<TriangleAttackState>());
        }
    }

    public void ExitState()
    {

    }
}