using System;
using System.Collections.Generic;

namespace Platformer.Core.FSM
{
    /// <summary>
    /// Contains transitions from any state like death or taking damage
    /// </summary>
    /// <typeparam name="TStateID"></typeparam>
    public abstract class AnyState<TStateID> where TStateID : Enum
    {
        public AnyState(StateMachine<TStateID> machine)
        {
            this.machine = machine;
        }

        protected StateMachine<TStateID> machine;
        protected List<Transition<TStateID>> transitions;

        public void Tick()
        {
            if (transitions == null || transitions.Count == 0)
                return;
            for (int i = 0; i < transitions.Count; i++)
            {
                transitions[i].Check(machine);
            }
        }
    }
}
