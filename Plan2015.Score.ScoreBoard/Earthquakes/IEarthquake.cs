using Microsoft.Xna.Framework;

namespace Plan2015.Score.ScoreBoard.Earthquakes
{
    public interface IEarthquake
    {
        Matrix Matrix { get; }
        bool IsFinished { get; }
        void Update(GameTime gameTime);
    }
}
