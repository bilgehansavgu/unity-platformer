using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.StateMachine
{
    [System.Serializable]
    public class StateMachine<TStateID> where TStateID : Enum
    {
        public StateMachine()
        {
            if (this.states != null)
                this.states = null;
            this.states = new Dictionary<TStateID, IState<TStateID>>();
            this.anyState = null;
        }
        public StateMachine(Dictionary<TStateID, IState<TStateID>> states, TStateID initialState)
        {
            if (this.states != null)
                this.states = null;
            this.states = states;
            this.anyState = null;
            ChangeState(initialState);
        }
        public StateMachine(Dictionary<TStateID, IState<TStateID>> states, AnyState<TStateID> anyState, TStateID initialState)
        {
            if (this.states != null)
                this.states = null;
            this.states = states;
            this.anyState = anyState;
            ChangeState(initialState);
        }

        // State Holding
        Dictionary<TStateID, IState<TStateID>> states;
        AnyState<TStateID> anyState;

        public void RegisterState(IState<TStateID> state)
        {
            states.Add(state.GetID(), state); 
        }
        /// <summary>
        /// Returns the according IState from dictionary.
        /// </summary>
        /// <param name="stateID"></param>
        /// <returns></returns>
        public IState<TStateID> GetState(TStateID stateID)
        {
            return states[stateID];
        }

        // Program
        [SerializeField] TStateID currentState;

        /// <summary>
        /// Runs the states. Call this in Update of a MonoBehaviour class.
        /// </summary>
        public void Tick()
        {
            GetState(currentState)?.Tick(this);
            anyState?.Tick();
        }

        /// <summary>
        /// Changes the current state.
        /// </summary>
        /// <param name="nextState"></param>
        public void ChangeState(TStateID nextState)
        {
            Debug.Log("Exit: " + currentState);
            GetState(currentState)?.Exit(this);
            currentState = nextState;
            GetState(currentState)?.Enter(this);
            Debug.Log("Enter: " + nextState);
        }

        /// <summary>
        /// Do not use this unless it is absolutly needed, use ChangeState instead.
        /// This does not call Exit() of current state.
        /// </summary>
        /// <param name="nextState"></param>
        public void ChangeStateImmediate(TStateID nextState)
        {
            currentState = nextState;
            GetState(currentState)?.Enter(this);
            Debug.Log("Enter: " + nextState);
        }
    }
}
