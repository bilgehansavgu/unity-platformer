using System;

/// <summary>
/// Base class for states.
/// </summary>
/// <typeparam name="TStateID"></typeparam>
public abstract class StateBase<TStateID> : IState<TStateID> where TStateID : Enum
{
    /// <summary>
    /// used by StateMachine this should return the according enum for the state
    /// </summary>
    /// <returns></returns>
    public abstract TStateID GetID();
    /// <summary>
    /// Called once on entering state
    /// </summary>
    /// <param name="machine"></param>
    public abstract void Enter(StateMachine<TStateID> machine);
    /// <summary>
    /// called once in on exiting state
    /// </summary>
    /// <param name="machine"></param>
    public abstract void Exit(StateMachine<TStateID> machine);
    /// <summary>
    /// called every frame
    /// </summary>
    /// <param name="machine"></param>
    public void Tick(StateMachine<TStateID> machine)
    {
        Act(machine);
        Decide(machine);
    }
    /// <summary>
    /// Contains functionality to be called every frame
    /// </summary>
    /// <param name="machine"></param>
    protected abstract void Act(StateMachine<TStateID> machine);
    /// <summary>
    /// Contains logic for changing states
    /// </summary>
    /// <param name="machine"></param>
    protected abstract void Decide(StateMachine<TStateID> machine);

    /// <summary>
    /// State onTrigger
    /// </summary>
    /// <param name="machine"></param>
    public virtual void InvokeState(StateMachine<TStateID> machine)
    {
        // This should remain empty if state doesn't have
    }
}