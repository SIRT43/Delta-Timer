using UnityEngine;

namespace FTGAMEStudio.InitialSolution.Timers
{
    [AddComponentMenu("Initial Solution/Timers/Event Timer Behaviour")]
    public class EventTimerBehaviour : TimerableBehaviour<EventTimer, EventTimerDescriptive>
    {
        private void Start() => timer = new(period, descriptive, current, timerName);
    }
}
