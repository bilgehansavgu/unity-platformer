using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.StateMachine
{
    [System.Serializable]
    public class StateMachine<TStateID> where TStateID : Enum
    {
        public StateMachine(Dictionary<TStateID, IState<TStateID>> states, AnyState<TStateID> anyState, TStateID initialState)
        {
            if (this.states != null)
                this.states = null;
            this.states = states;
            this.anyState = anyState;
            ChangeState(initialState);
        }

        // State Database
        Dictionary<TStateID, IState<TStateID>> states;
        AnyState<TStateID> anyState;
        public void RegisterState(IState<TStateID> state)
        {
            states.Add(state.GetID(), state); 
        }
        public IState<TStateID> GetState(TStateID stateID)
        {
            return states[stateID];
        }

        // Program
        [SerializeField] TStateID currentState;
        public void Tick()
        {
            GetState(currentState)?.Tick(this);
            anyState.Tick();
        }
        public void ChangeState(TStateID nextState)
        {
            Debug.Log("Exit: " + currentState);
            GetState(currentState)?.Exit(this);
            currentState = nextState;
            GetState(currentState)?.Enter(this);
            Debug.Log("Enter: " + nextState);
        }
        public void ChangeStateImmediate(TStateID nextState)
        {
            currentState = nextState;
            GetState(currentState)?.Enter(this);
            Debug.Log("Enter: " + nextState);
        }
    }
    public interface IState<TStateID> where TStateID : Enum
    {
        TStateID GetID();
        void Enter(StateMachine<TStateID> machine);
        void Exit(StateMachine<TStateID> machine);
        void Tick(StateMachine<TStateID> machine);
    }
    public abstract class StateBase<TStateID> : IState<TStateID> where TStateID : Enum
    {
        public abstract TStateID GetID();
        public abstract void Enter(StateMachine<TStateID> machine);
        public abstract void Exit(StateMachine<TStateID> machine);
        public void Tick(StateMachine<TStateID> machine)
        {
            Act(machine);
            Decide(machine);
        }
        protected abstract void Act(StateMachine<TStateID> machine);
        protected abstract void Decide(StateMachine<TStateID> machine);
    }
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
