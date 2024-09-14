using InitialFramework;
using InitialFramework.Collections.Generic;
using System;

namespace InitialSolution.Timers
{
    public enum TimerState : byte
    {
        Running,
        Paused,
        Stopped,
        Closed
    }

    public interface IControllableTimer
    {
        bool StartTimer();
        bool PauseTimer();
        bool StopTimer();
        bool CloseTimer();
    }

    public abstract class ControllableTimer : TimerBehaviour, IOrganized, IControllableTimer, IStatusable<TimerState>
    {
        public Guid Guid { get; } = Guid.NewGuid();


        private TimerState state = TimerState.Stopped;
        public virtual TimerState State { get => state; protected set => state = value; }



        public virtual bool StartTimer()
        {
            if (State is TimerState.Running or TimerState.Closed) return false;
            State = TimerState.Running;

            enabled = true;
            return true;
        }

        public virtual bool PauseTimer()
        {
            if (State is TimerState.Paused or TimerState.Closed) return false;
            State = TimerState.Paused;

            enabled = false;
            return true;
        }

        public virtual bool StopTimer()
        {
            if (State is TimerState.Stopped or TimerState.Closed) return false;
            State = TimerState.Stopped;

            enabled = false;
            Current = 0;

            return true;
        }

        public virtual bool CloseTimer()
        {
            if (State == TimerState.Closed) return false;
            State = TimerState.Closed;

            Destroy(this);
            return true;
        }



        protected virtual void Start() => enabled = false;
    }
}
