namespace FTGAMEStudio.InitialSolution.Timers
{
    public interface ITimerable
    {
        /// <summary>
        /// ���ڣ�����������೤ʱ�临λһ�Ρ�
        /// </summary>
        float Period { get; }
        float Current { get; }

        /// <summary>
        /// �����ʱ����
        /// <br>������� <seealso cref="Current"/></br>
        /// </summary>
        void Reset(float target);

        void Invoke();
    }

    public interface IDeltaTimer
    {
        void Update(float delta);
    }

    /// <summary>
    /// ��ʱ�����࣬����ʱ������������
    /// </summary>
    public abstract class TimerBase : ITimerable, IDeltaTimer
    {
        public virtual float Period { get; set; }
        public virtual float Current { get; set; }

        protected TimerBase(float period, float current = 0)
        {
            Period = period;
            Current = current;
        }

        public virtual void Update(float delta)
        {
            Current += delta;
            TryReset();
        }

        public virtual void Reset(float target) => Current = target;

        protected virtual void TryReset()
        {
            if (Current < Period) return;

            Reset(Current - Period);
            OnReset();
            Invoke();
        }

        public abstract void OnReset();
        public abstract void Invoke();
    }
}
