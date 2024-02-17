using UnityEngine;
using UnityEngine.InputSystem;

public class TransactionState : MonoBehaviour, IPlayerState
{
    public IPlayerState targetState;
    
    private Animator animator;
    private Rigidbody2D rb;
    public PlayerStateInputs inputHandler;
    public PlayerStateMachine stateMachine;
    public SquareAttackState squareAttack;
    
    public void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        inputHandler = GetComponent<PlayerStateInputs>();
        stateMachine = GetComponent<PlayerStateMachine>();
        targetState = GetComponent<IdleState>();
    }
    public void EnterState()
    {
        Debug.Log("TransactionState");
        this.targetState = targetState;
        // Perform any actions needed before transitioning to the target state
        // Example: Check data, set values, etc.
        
        // After performing actions, transition to the target state
        GetComponent<PlayerStateMachine>().SetState(targetState);
    }

    public void UpdateState()
    {
        // No update logic needed for the transaction state
    }

    public void ExitState()
    {
        // No exit logic needed for the transaction state
    }
    
}