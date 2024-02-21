using System;

namespace Core.FSM
{
    /// <summary>
    /// Used by StateMachine do not inherit this.
    /// </summary>
    /// <typeparam name="TStateID"></typeparam>
    public interface IState<TStateID> where TStateID : Enum
    {
        TStateID GetID();
        void InvokeEnter(StateMachine<TStateID> machine);
        void InvokeExit(StateMachine<TStateID> machine);
        void Tick(StateMachine<TStateID> machine);
        void InvokeStateTrigger(StateMachine<TStateID> machine);
        bool IsReady();
    }
}
