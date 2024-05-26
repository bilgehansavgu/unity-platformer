using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StateMachine<TStateID> where TStateID : Enum
{
    Dictionary<TStateID, IState<TStateID>> states;
    
    [SerializeField] TStateID currentState;
    
    public StateMachine(Dictionary<TStateID, IState<TStateID>> states, TStateID initialState)
    {
        if (this.states != null)
            this.states = null;
        this.states = states;
        ChangeState(initialState);
    }
    
    public IState<TStateID> GetState(TStateID stateID)
    {
        return states[stateID];
    }
    
    public void UpdateState()
    {
        GetState(currentState)?.UpdateState(this);
    }
    
    public void ChangeState(TStateID nextState)
    {
        if (showExitStateDebug)
            Debug.Log("Exit: " + currentState);
        GetState(currentState)?.Exit(this);
        currentState = nextState;
        GetState(currentState)?.Enter(this);
        if (showEnterStateDebug)
            Debug.Log("Enter: " + nextState);
    }
    
    public bool IsEqual(TStateID other)
    {
        return GetState(currentState) == GetState(other);
    }

    [SerializeField] bool showEnterStateDebug;
    [SerializeField] bool showExitStateDebug;
}
