using System;

public abstract class StateBase<TStateID> : IState<TStateID> where TStateID : Enum
{
    public abstract void Enter(StateMachine<TStateID> machine);

    public abstract void Exit(StateMachine<TStateID> machine);

    public void UpdateState(StateMachine<TStateID> machine)
    {
        Act(machine);
        Decide(machine);
    }

    protected abstract void Act(StateMachine<TStateID> machine);

    protected abstract void Decide(StateMachine<TStateID> machine);
    
    public virtual void InvokeState(StateMachine<TStateID> machine)
    {

    }
}