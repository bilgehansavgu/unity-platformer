using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class IdleState : MonoBehaviour, IPlayerState
{
    private Animator animator;
    private Rigidbody2D rb;
    [FormerlySerializedAs("inputHandler")] public PlayerStateInputs_old inputOldHandler;
    public PlayerStateMachine stateMachine;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        inputOldHandler = GetComponent<PlayerStateInputs_old>();
        stateMachine = GetComponent<PlayerStateMachine>();
    }

    public void EnterState()
    {
        Debug.Log("IdleEnterState");
        animator.Play("Idle");
    }
    

    public void UpdateState()
    {
        if (inputOldHandler.MoveInputValue.x != 0)
        {
            stateMachine.SetState(GetComponent<MovementState>());
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
}