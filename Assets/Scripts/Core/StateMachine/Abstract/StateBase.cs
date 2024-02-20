using System;

namespace Platformer.Core.FSM
{
    /// <summary>
    /// Base class for states.
    /// </summary>
    /// <typeparam name="TStateID"></typeparam>
    public abstract class StateBase<TStateID> : IState<TStateID> where TStateID : Enum
    {
        #region Abstract
        protected bool isActive = false;

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
        /// Contains functionality to be called every frame
        /// </summary>
        /// <param name="machine"></param>
        protected abstract void Act(StateMachine<TStateID> machine);
        /// <summary>
        /// Contains logic for changing states
        /// </summary>
        /// <param name="machine"></param>
        protected abstract void Decide(StateMachine<TStateID> machine);
        #endregion

        /// <summary>
        /// State onTrigger
        /// </summary>
        /// <param name="machine"></param>
        public virtual void InvokeStateTrigger(StateMachine<TStateID> machine)
        {
            // This should remain empty if state doesn't have
        }

        /// <summary>
        /// Sets up a cooldown if needed
        /// </summary>
        /// <param name="tresholdTime"> Cooldown treshold will set on exit of the stated. Give value in seconds</param>
        public void SetCooldown(float tresholdTime)
        {
            if (tresholdTime > 0)
            {
                this.cooldownTreshold = tresholdTime;
                cooldown = new StateCooldown();
            }
            else
                cooldown = null;
        }

        float cooldownTreshold = 0;
        StateCooldown cooldown;
        
        /// <summary>
        /// Checks the state if it is ready to enter
        /// </summary>
        /// <returns></returns>
        public bool IsReady()
        {
            if (cooldown == null)
                return true;

            return cooldown.Check();
        }

        /// <summary>
        /// Invoked from StateMachine. Calls the Enter function on the states Enter() and avtivates the state.
        /// </summary>
        /// <param name="machine"></param>
        public void InvokeEnter(StateMachine<TStateID> machine)
        {
            Enter(machine);
            isActive = true;
        }
        public void InvokeExit(StateMachine<TStateID> machine)
        {
            isActive = false;
            Exit(machine);
            cooldown?.Reset(cooldownTreshold);
        }

        /// <summary>
        /// called every frame
        /// </summary>
        /// <param name="machine"></param>
        public void Tick(StateMachine<TStateID> machine)
        {
            if (!isActive)
                return;
            Act(machine);
            Decide(machine);
        }
    }
}
