using System;

public interface IState<TStateID> where TStateID : Enum
{
    void Enter(StateMachine<TStateID> machine);
    void Exit(StateMachine<TStateID> machine);
    void UpdateState(StateMachine<TStateID> machine);
    void InvokeState(StateMachine<TStateID> machine);
}
