namespace Bismuth.Framework.Animations.Timelines
{
    public abstract class Timeline : ITimeline
    {
        public float BeginTime { get; set; }
        public float EndTime { get; set; }

        public float Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                _inverseDuration = 1.0f / value;
            }
        }

        private float _duration;
        private float _inverseDuration;

        public FillBehavior FillBehavior { get; set; }

        public abstract void Update(float time);

        protected float NormalizeTime(float time)
        {
            // Normalizing time.
            time -= BeginTime;
            time *= _inverseDuration;

            if (FillBehavior == FillBehavior.HoldEnd)
            {
                if (time > 1.0f) time = 1.0f;
            }
            else if (FillBehavior == FillBehavior.Repeat)
            {
                time = time % 1.0f;
            }
            else if (FillBehavior == FillBehavior.ReverseAndRepeat)
            {
                // 0 = even, 1 = odd
                int f = (int)time & 1;

                time = time % 1.0f;

                if (f == 1) time = 1.0f - time;
            }

            return time;
        }
    }
}
