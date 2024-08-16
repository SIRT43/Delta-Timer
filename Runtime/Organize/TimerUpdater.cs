using FTGAMEStudio.InitialFramework;
using System.Collections.Generic;
using UnityEngine;

namespace FTGAMEStudio.InitialSolution.Timers
{
    [AddComponentMenu("Initial Solution/Timers/Timer Updater")]
    public class TimerUpdater : UniqueBehaviour
    {
#if UNITY_EDITOR
        [Header("Editor")]
        public bool displayDeltaTiemr = false;
        public List<DeltaTimerDisplay> deltaTimers;
#endif

        protected override void Awake()
        {
            base.Awake();

            enabled = !TimerOrganize.organize.IsEmpty;
            TimerOrganize.organize.OnMetadataCountChanged += OnUpdateTargetChanged;
        }

        private void Update()
        {
            TimerOrganize.organize.TraverseMetadata();

#if UNITY_EDITOR
            if (displayDeltaTiemr) deltaTimers = TimerOrganize.organize.GetTimerDisplay();
#endif
        }

        private void OnUpdateTargetChanged(int count, bool isEmpty) => enabled = !isEmpty;

        protected override void OnDestroy()
        {
            base.OnDestroy();
            TimerOrganize.organize.OnMetadataCountChanged -= OnUpdateTargetChanged;
        }
    }
}
