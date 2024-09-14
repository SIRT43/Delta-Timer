#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

namespace InitialSolution.Timers
{
    [Serializable]
    public struct DeltaTimerInfo
    {
        public static DeltaTimerInfo[] GetTiemrInfos(IEnumerable<KeyValuePair<Guid, ControllableTimer>> timers)
        {
            List<DeltaTimerInfo> infos = new();

            foreach (KeyValuePair<Guid, ControllableTimer> pair in timers)
            {
                infos.Add(new()
                {
                    guid = pair.Value.Guid.ToString(),
                    state = pair.Value.State,
                    period = pair.Value.Period,
                    current = pair.Value.Current
                });
            }

            return infos.ToArray();
        }



        public string guid;
        public TimerState state;

        [Space]
        public float period;
        public float current;
    }

    [AddComponentMenu("Initial Solution/Timers/Delta Timer Display")]
    public class DeltaTimerDisplay : MonoBehaviour
    {
        public DeltaTimerInfo[] infos;

        private void Update() => infos = DeltaTimerInfo.GetTiemrInfos(DeltaTimer.registry);
    }
}
#endif
