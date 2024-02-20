using System;
using UnityEngine;

namespace Platformer.Core.FSM
{
    /// <summary>
    /// Base class for transitions used by AnyState.
    /// </summary>
    /// <typeparam name="TStateID"></typeparam>
    public abstract class Transition<TStateID> where TStateID : Enum
    {
        protected abstract TStateID NextState();
        public void Check(StateMachine<TStateID> machine)
        {
            if (Logic(machine))
            {
                Debug.Log(this + " true");
                machine.ChangeState(NextState());
            }
        }
        public abstract bool Logic(StateMachine<TStateID> machine);
    }
}
