namespace Tween.Timer
{
    /// <summary>
    /// Limits the fire rate with given rpm.
    /// </summary>
    public class FireRateTimer : CooldownTimer
    {
        float RPM
        {
            get
            {
                return roundsPerMinute;
            }
            set
            {
                roundsPerMinute = value;
                timeBetweenShots = 60 / roundsPerMinute;
            }
        }

        // Do not set these directly. Use RPM property instead.
        float roundsPerMinute;
        float timeBetweenShots;

        /// <summary>
        /// Check if action is possible
        /// </summary>
        /// <param name="roundPerMinute">Updates the RPM value if it has been changed.</param>
        /// <returns>Returns true when time treshold reached</returns>
        public override bool CheckCooldown(float roundPerMinute)
        {
            if (RPM != roundPerMinute)
                RPM = roundPerMinute;

            return base.CheckCooldown(timeBetweenShots);
        }
    }
}
