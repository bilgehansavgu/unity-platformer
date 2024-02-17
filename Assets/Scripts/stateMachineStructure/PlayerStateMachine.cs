using System;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private IPlayerState currentState;
    public IPlayerState targetState;

    public TransactionState transactionState;

    public void Start()
    {
        currentState = GetComponent<IdleState>();
        currentState.EnterState();
        transactionState = GetComponent<TransactionState>();
        // Ensure transactionState is properly assigned
        //transactionState = GetComponent<TransactionState>();
        if (transactionState == null)
        {
            Debug.LogError("TransactionState component not found!");
        }

    }

    public void SetState(IPlayerState state)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }

        currentState = state;
        currentState.EnterState();
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState();
        }
        else
        {
            SetState(GetComponent<IdleState>());
        }
    }
    
    public void TransitionToStateWithTransaction()
    {
        if (transactionState != null)
        {
            SetState(transactionState);
        }
        else
        {
            Debug.LogError("TransactionState not assigned!");
        }
    }
}