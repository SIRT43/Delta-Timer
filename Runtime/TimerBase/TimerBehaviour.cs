using UnityEngine;

namespace InitialSolution.Timers
{
    public interface ITimerable
    {
        /// <summary>
        /// 周期，计数器间隔多长时间 <see cref="Invoke"/> 一次。
        /// </summary>
        float Period { get; set; }
        float Current { get; }

        /// <summary>
        /// 调用本计时器的事件。
        /// </summary>
        void Invoke();
    }

    public interface ITimerUpdater
    {
        float GetDelta();
        void UpdateTimer();
    }

    /// <summary>
    /// <see cref="TimerBehaviour"/> 是所有 DeltaTimer 派生自的基类。
    /// </summary>
    public abstract class TimerBehaviour : MonoBehaviour, ITimerable, ITimerUpdater
    {
        [SerializeField] private float period = 10;
        public virtual float Period { get => period; set => period = value; }

        [SerializeField] private float current = 0;
        public virtual float Current { get => current; set => current = value; }



        public virtual void UpdateTimer()
        {
            Current += GetDelta();

            if (Current < Period) return;

            int invokeCount = (int)(Current / Period);
            Current %= Period;

            for (int current = 0; current < invokeCount; current++) Invoke();
            OnReset(invokeCount);
        }

        public virtual float GetDelta() => Time.deltaTime;

        protected virtual void OnReset(int invokeCount) { }
        public abstract void Invoke();



        protected virtual void Update() => UpdateTimer();
    }
}
