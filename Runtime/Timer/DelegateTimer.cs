using System;
using UnityEngine.Events;

namespace FTGAMEStudio.InitialSolution.Timers
{
    [Serializable]
    public class ActionTimerDescriptive : TimerDescriptive
    {
        public UnityAction<ActionTimer> action;
    }

    [Serializable]
    public class EventTimerDescriptive : TimerDescriptive
    {
        public UnityEvent<EventTimer> @event;
    }

    public class ActionTimer : DescriptableTimer<ActionTimerDescriptive>
    {
        public ActionTimer(float period, ActionTimerDescriptive descriptive, float current = 0, string name = null) : base(period, descriptive, current, name) { }

        public override void Invoke() => Descriptive.action?.Invoke(this);
    }

    public class EventTimer : DescriptableTimer<EventTimerDescriptive>
    {
        public EventTimer(float period, EventTimerDescriptive descriptive, float current = 0, string name = null) : base(period, descriptive, current, name) { }

        public override void Invoke() => Descriptive.@event?.Invoke(this);
    }
}
