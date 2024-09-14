using System;
using UnityEngine;

namespace InitialSolution.Timers
{
    public interface IDescriptableTimer<T> where T : TimerDescriptive
    {
        T Descriptive { get; set; }
    }

    public abstract class TimerDescriptive
    {
        public bool repeat = false;

        [Space]
        public bool startOnCreate = true;
        public bool closeOnStop = true;

        [Space]
        public bool invokeOnStart = false;
        public bool invokeOnPause = false;
        public bool invokeOnStop = false;
        public bool invokeOnClose = false;

        public Action<ControllableTimer> onStart;
        public Action<ControllableTimer> onPause;
        public Action<ControllableTimer> onStop;
        public Action<ControllableTimer> onClose;
    }

    public abstract class DescriptableTimer<T> : ControllableTimer, IDescriptableTimer<T> where T : TimerDescriptive
    {
        [SerializeField] private T descriptive;
        public T Descriptive { get => descriptive; set => descriptive = value; }



        public override bool StartTimer()
        {
            if (!base.StartTimer()) return false;

            if (Descriptive.invokeOnStart) Invoke();
            Descriptive.onStart?.Invoke(this);

            return true;
        }

        public override bool PauseTimer()
        {
            if (!base.PauseTimer()) return false;

            if (Descriptive.invokeOnPause) Invoke();
            Descriptive.onPause?.Invoke(this);

            return true;
        }

        public override bool StopTimer()
        {
            if (!base.StopTimer()) return false;

            if (Descriptive.invokeOnStop) Invoke();
            Descriptive.onStop?.Invoke(this);

            if (Descriptive.closeOnStop) CloseTimer();

            return true;
        }

        public override bool CloseTimer()
        {
            if (!base.CloseTimer()) return false;

            if (Descriptive.invokeOnClose) Invoke();
            Descriptive.onClose?.Invoke(this);

            return true;
        }



        protected override void Start()
        {
            base.Start();

            if (Descriptive.startOnCreate) StartTimer();
        }

        protected override void OnReset(int invokeCount)
        {
            if (!Descriptive.repeat) StopTimer();
        }
    }
}
