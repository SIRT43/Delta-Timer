using UnityEngine;

namespace FTGAMEStudio.InitialSolution.Timers
{
    public class TimerableBehaviour<TTimer, TDescriptive> : MonoBehaviour
        where TTimer : DescriptableTimer<TDescriptive>
        where TDescriptive : TimerDescriptive, new()
    {
        [SerializeField, Min(0.01f)] protected float period = 10;
        public TDescriptive descriptive = new();

        [Space]
        [SerializeField] protected string timerName = "New Timer";
        [SerializeField, Min(0)] protected float current = 0;

#if UNITY_EDITOR
        /// <summary>
        /// 请不要使用本值，它仅在 Editor 工作。
        /// </summary>
        [Header("Editor")]
        [SerializeField] protected TimerState state = TimerState.Stopped;
#endif

        public TTimer timer;

        protected virtual void Awake()
        {
            descriptive.onStart += OnTiemrStart;
            descriptive.onPause += OnTiemrPause;
            descriptive.onStop += OnTiemrStop;
            descriptive.onDestroy += OnTiemrDestroy;
        }

        protected virtual void OnTiemrStart() { }
        protected virtual void OnTiemrPause() { }
        protected virtual void OnTiemrStop() { }
        protected virtual void OnTiemrDestroy() => Destroy(this);

        protected virtual void OnDestroy() => timer.Destroy();

#if UNITY_EDITOR
        private void Update()
        {
            current = timer.Current;
            state = timer.State;
        }

        private void OnValidate()
        {
            if (!Application.isPlaying || timer == null) return;

            timer.Period = period;
            timer.name = timerName;
            timer.Current = current;
        }
#endif
    }
}
