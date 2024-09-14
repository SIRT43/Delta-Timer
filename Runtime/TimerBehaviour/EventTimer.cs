using System;
using UnityEngine;
using UnityEngine.Events;

namespace InitialSolution.Timers
{
    [Serializable]
    public class EventTimerDescriptive : TimerDescriptive
    {
        public UnityEvent<EventTimer> @event;
    }

    [AddComponentMenu("Initial Solution/Timers/Event Timer"), Serializable]
    public class EventTimer : DescriptableTimer<EventTimerDescriptive>
    {
        public override void Invoke() => Descriptive.@event?.Invoke(this);
    }
}
