using UnityEngine;

namespace Platformer.Tween.Timer
{
    /// <summary>
    /// Simple cooldown waits until given treshold has reached
    /// </summary>
    public class CooldownTimer
    {
        protected float treshold = 0f;
        protected bool tresholdReached => Time.time >= treshold;

        /// <summary>
        /// Stores value of next possible time this can return true. Returns true first time it is called.
        /// </summary>
        /// <param name="treshold">Treshold time that should be passed before next true statement return.</param>
        /// <returns></returns>
        public virtual bool CheckCooldown(float treshold)
        {
            if (tresholdReached)
            {
                Reset(treshold);
                return true;
            }
            return false;
        }
        private void Reset(float treshold)
        {
            this.treshold = Time.time + treshold;
        }
    }
}
