using System;
using Microsoft.Xna.Framework;

namespace Plan2015.Score.ScoreBoard.Earthquakes
{
    public class EarthquakeY : IEarthquake
    {
        private Vector3 _translation;

        private float _timer;
        private float _amount;
        private float _duration;
        private float _speed = 0.03f;

        private bool _isFinished = true;
        public bool IsFinished { get { return _isFinished; } }

        public EarthquakeY(float amount)
        {
            _timer = 0;
            _amount = amount;
            _isFinished = false;
        }

        public EarthquakeY(float amount, float duration, float speed)
        {
            _timer = 0;
            _amount = amount;
            _isFinished = false;
            _duration = duration;
            _speed = speed;
        }

        public Matrix Matrix
        {
            get { return Matrix.CreateTranslation(_translation); }
        }

        public void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (!_isFinished)
            {
                _timer += _speed * elapsedTime;

                if (_duration > 0)
                {
                    _duration -= elapsedTime;
                }
                else
                {
                    _amount -= 0.02f * elapsedTime;
                }

                if (_amount > 0)
                {
                    _translation.Y = (int)(Math.Sin(_timer) * _amount);
                }
                else
                {
                    _isFinished = true;
                }
            }
        }
    }
}
