using System;

namespace Core.StateMachine
{
    /// <summary>
    /// Used by StateMachine do not inherit this.
    /// </summary>
    /// <typeparam name="TStateID"></typeparam>
    public interface IState<TStateID> where TStateID : Enum
    {
        TStateID GetID();
        void Enter(StateMachine<TStateID> machine);
        void Exit(StateMachine<TStateID> machine);
        void Tick(StateMachine<TStateID> machine);
        void InvokeState(StateMachine<TStateID> machine);
    }
}
