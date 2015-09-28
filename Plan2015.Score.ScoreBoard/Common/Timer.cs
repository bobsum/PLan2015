using Microsoft.Xna.Framework;

namespace Plan2015.Score.ScoreBoard
{
    public class Timer
    {
        public Timer(int interval, bool autoReset = true)
        {
            _interval = interval;
            _autoReset = autoReset;
        }

        public bool AutoReset { get { return _autoReset; } set { _autoReset = value; } }
        private bool _autoReset;

        public int Interval { get { return _interval; } set { _interval = value; } }
        private int _interval = 1000;

        public double ElapsedTime { get { return _elapsedTime; } }
        private double _elapsedTime = 0;

        public bool HasElapsed { get { return _elapsedTime > _interval; } }

        public void Reset()
        {
            _elapsedTime = 0;
        }

        public bool Tick(GameTime gameTime)
        {
            if (HasElapsed)
            {
                if (_autoReset) Reset();

                return true;
            }
            else
            {
                _elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;

                return false;
            }
        }
    }
}
