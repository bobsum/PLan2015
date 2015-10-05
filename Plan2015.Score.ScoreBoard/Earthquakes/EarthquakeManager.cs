using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Plan2015.Score.ScoreBoard.Earthquakes
{
    public class EarthquakeManager
    {
        private List<IEarthquake> _pushed = new List<IEarthquake>();

        public Matrix Matrix
        {
            get
            {
                Matrix matrix = Matrix.Identity;
                for (int i = 0; i < _pushed.Count; i++)
                    matrix *= _pushed[i].Matrix;

                return matrix;
            }
        }

        public void Push(IEarthquake earthquake)
        {
            _pushed.Add(earthquake);
        }

        public void Update(GameTime gameTime)
        {
            for (int i = _pushed.Count - 1; i >= 0; i--)
            {
                if (_pushed[i].IsFinished)
                {
                    _pushed.RemoveAt(i);
                }
                else
                {
                    _pushed[i].Update(gameTime);
                }
            }
        }
    }
}
