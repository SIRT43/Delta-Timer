using System;
using UnityEngine;
using UnityEngine.Events;

namespace InitialSolution.Timers
{
    [Serializable]
    public class ActionTimerDescriptive : TimerDescriptive
    {
        public UnityAction<ActionTimer> action;
    }

    [AddComponentMenu("Initial Solution/Timers/Action Timer"), Serializable]
    public class ActionTimer : DescriptableTimer<ActionTimerDescriptive>
    {
        public override void Invoke() => Descriptive.action?.Invoke(this);
    }
}
