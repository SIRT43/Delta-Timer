using FTGAMEStudio.InitialFramework;
using FTGAMEStudio.InitialFramework.Collections.Generic;
using System;

namespace FTGAMEStudio.InitialSolution.Timers
{
    public enum TimerState : byte
    {
        Running,
        Paused,
        Stopped,
        Destroy
    }

    public interface IControllableTimer : IOrganized
    {
        void Start();
        void Pause();
        void Stop();
        void Destroy();
    }

    public abstract class ControllableTimer : TimerBase, IControllableTimer, IStatusable<TimerState>
    {
        public Guid Guid { get; } = Guid.NewGuid();
        public string name;

        protected ControllableTimer(float period, float current = 0, string name = null) : base(period, current)
        {
            this.name = name ?? Guid.ToString();
            TimerOrganize.organize.RegValue(this);
        }

        public virtual TimerState State { get; protected set; } = TimerState.Stopped;

        public virtual void Start()
        {
            if (State == TimerState.Running) return;
            State = TimerState.Running;

            TimerOrganize.organize.RegMetadata(Guid);
        }

        public virtual void Pause()
        {
            if (State == TimerState.Paused) return;
            State = TimerState.Paused;

            TimerOrganize.organize.RemoveMetadata(Guid);
        }

        public virtual void Stop()
        {
            if (State == TimerState.Stopped) return;
            State = TimerState.Stopped;

            TimerOrganize.organize.RemoveMetadata(Guid);
            Reset(0);
        }

        public virtual void Destroy()
        {
            if (State == TimerState.Destroy) return;
            State = TimerState.Destroy;

            TimerOrganize.organize.RemoveValue(Guid);
        }

        ~ControllableTimer() => Destroy();
    }
}
