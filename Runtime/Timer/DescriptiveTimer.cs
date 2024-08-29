using System;
using UnityEngine;

namespace FTGAMEStudio.InitialSolution.Timers
{
    public interface IDescriptableTimer<T> where T : TimerDescriptive
    {
        T Descriptive { get; }
    }

    [Serializable]
    public abstract class TimerDescriptive
    {
        public bool repeat = false;

        [Space]
        public bool startOnCreate = true;
        public bool destroyOnStop = true;

        [Space]
        public bool invokeOnStart = false;
        public bool invokeOnPause = false;
        public bool invokeOnStop = false;
        public bool invokeOnDestroy = false;

        public Action onStart;
        public Action onPause;
        public Action onStop;
        public Action onDestroy;
    }

    public abstract class DescriptableTimer<T> : ControllableTimer, IDescriptableTimer<T> where T : TimerDescriptive
    {
        public T Descriptive { get; }

        protected DescriptableTimer(float period, T descriptive, float current = 0, string name = null) : base(period, current, name)
        {
            Descriptive = descriptive;

            if (Descriptive.startOnCreate) Start();
        }

        public override void OnReset()
        {
            if (!Descriptive.repeat) Stop();
        }

        public override void Start()
        {
            base.Start();

            if (Descriptive.invokeOnStart) Invoke();
            Descriptive.onStart?.Invoke();
        }

        public override void Pause()
        {
            base.Pause();

            if (Descriptive.invokeOnPause) Invoke();
            Descriptive.onPause?.Invoke();
        }

        public override void Stop()
        {
            base.Stop();

            if (Descriptive.invokeOnStop) Invoke();
            Descriptive.onStop?.Invoke();

            if (Descriptive.destroyOnStop) Destroy();
        }

        public override void Destroy()
        {
            base.Destroy();

            if (Descriptive.invokeOnDestroy) Invoke();
            Descriptive.onDestroy?.Invoke();
        }
    }
}
