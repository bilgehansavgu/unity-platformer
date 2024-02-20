using UnityEngine;

namespace Platformer.Core.FSM
{
    public class StateCooldown
    {
        protected float treshold = 0f;
        protected bool tresholdReached => Time.time >= treshold;

        protected bool isUsed = false;

        /// <summary>
        /// Stores value of next possible time this can return true. Returns true first time it is called.
        /// </summary>
        /// <param name="treshold">Treshold time that should be passed before next true statement return.</param>
        /// <returns></returns>
        public virtual bool Check()
        {
            if (tresholdReached && !isUsed)
            {
                isUsed = true;
                return true;
            }
            return false;
        }
        public void Reset(float treshold)
        {
            this.treshold = Time.time + treshold;
            isUsed = false;
        }
    }
}
