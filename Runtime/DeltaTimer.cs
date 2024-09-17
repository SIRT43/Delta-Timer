using InitialFramework.Collections.Generic;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace InitialSolution.Timers
{
    public static class DeltaTimer
    {
        private static GameObject timers;
        private static GameObject Timers
        {
            get
            {
                if (timers == null)
                {
                    timers = new("Delta Timer Hub");
                    UnityEngine.Object.DontDestroyOnLoad(timers);
                }

                return timers;
            }
        }

        public static readonly Organize<ControllableTimer> registry = new();


        public static ControllableTimer[] GetTimersWithName(string timerName)
        {
            List<ControllableTimer> timers = new();

            foreach (KeyValuePair<Guid, ControllableTimer> timer in registry)
            {
                if (timer.Value.timerName == timerName) timers.Add(timer.Value);
            }

            return timers.ToArray();
        }
        public static ControllableTimer GetTimerWithName(string timerName)
        {
            ControllableTimer[] timers = GetTimersWithName(timerName);
            return timers.Length == 0 ? null : timers[0];
        }



        public static TTimer Instantiate<TTimer, TDescriptive>(TDescriptive descriptive, float period, float current = 0, string timerName = null) where TTimer : DescriptableTimer<TDescriptive> where TDescriptive : TimerDescriptive
        {
            TTimer timer = Timers.AddComponent<TTimer>();

            timer.Descriptive = descriptive;
            timer.Period = period;
            timer.Current = current;
            timer.timerName = string.IsNullOrEmpty(timerName) ? timer.timerName : timerName;

            registry.RegValue(timer);
            timer.Descriptive.onClose += (timer) => registry.RemoveValue(timer.Guid);

            return timer;
        }

        public static ActionTimer Instantiate(ActionTimerDescriptive descriptive, float period, float current = 0) =>
            Instantiate<ActionTimer, ActionTimerDescriptive>(descriptive, period, current);

        public static EventTimer Instantiate(EventTimerDescriptive descriptive, float period, float current = 0) =>
            Instantiate<EventTimer, EventTimerDescriptive>(descriptive, period, current);



        public static ActionTimer Delay(UnityAction<ActionTimer> unityAction, float period, float current = 0) =>
            Instantiate(new ActionTimerDescriptive() { action = unityAction, closeOnStop = true, startOnCreate = true }, period, current);

        public static EventTimer Delay(UnityEvent<EventTimer> unityEvent, float period, float current = 0) =>
            Instantiate(new EventTimerDescriptive() { @event = unityEvent, closeOnStop = true, startOnCreate = true }, period, current);
    }
}
