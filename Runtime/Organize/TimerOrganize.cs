using FTGAMEStudio.InitialFramework.Collections.Generic;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FTGAMEStudio.InitialSolution.Timers
{
#if UNITY_EDITOR
    [Serializable]
    public struct DeltaTimerDisplay
    {
        public string guid;
        public string name;

        [Space]
        public float period;
        public float current;

        [Space]
        public TimerState state;
    }
#endif

    public class TimerOrganize : MetadataOrganize<ControllableTimer>
    {
        public readonly static TimerOrganize organize = new();

        public T GetTimer<T>(Guid id) where T : ControllableTimer
        {
            if (GetValue(id) is T deltaTimer) return deltaTimer;
            return null;
        }

        public List<T> GetWithName<T>(string name) where T : ControllableTimer
        {
            List<T> deltaTimers = new();

            foreach (Guid id in registry.Keys)
            {
                ControllableTimer deltaTimer = GetValue(id);
                if (deltaTimer is T targetTimer)
                {
                    if (targetTimer.name == name) deltaTimers.Add(targetTimer);
                }
            }

            return deltaTimers.Count == 0 ? null : deltaTimers;
        }

        protected override void OnTraverseMetadata(KeyValuePair<Guid, ControllableTimer> pair)
        {
            if (pair.Value == null) return;
            pair.Value.Update(Time.deltaTime);
        }

#if UNITY_EDITOR
        public List<DeltaTimerDisplay> GetTimerDisplay()
        {
            List<DeltaTimerDisplay> deltaTimerDisplay = new();

            foreach (Guid id in registry.Keys)
            {
                ControllableTimer controller = GetValue(id);
                deltaTimerDisplay.Add(new()
                {
                    guid = controller.Guid.ToString(),
                    name = controller.name,
                    period = controller.Period,
                    current = controller.Current,
                    state = controller.State,
                });
            }

            return deltaTimerDisplay;
        }
#endif
    }
}
