using UnityEngine;

namespace InitialSolution.Timers
{
    public interface ITimerable
    {
        /// <summary>
        /// ���ڣ�����������೤ʱ�� <see cref="Invoke"/> һ�Ρ�
        /// </summary>
        float Period { get; set; }
        float Current { get; }

        /// <summary>
        /// ���ñ���ʱ�����¼���
        /// </summary>
        void Invoke();
    }

    public interface ITimerUpdater
    {
        float GetDelta();
        void UpdateTimer();
    }

    /// <summary>
    /// <see cref="TimerBehaviour"/> ������ DeltaTimer �����ԵĻ��ࡣ
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
