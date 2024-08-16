using UnityEngine;

namespace FTGAMEStudio.InitialSolution.Timers
{
    [AddComponentMenu("Initial Solution/Timers/Action Timer Behaviour")]
    public class ActionTimerBehaviour : TimerableBehaviour<ActionTimer, ActionTimerDescriptive>
    {
        private void Start() => timer = new(period, descriptive, current, timerName);
    }
}
