using Bismuth.Framework.Audio;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plan2015.Score.ScoreBoard.Audio
{
    public class NoSoundManager : ISoundManager
    {
        public void Update(GameTime gameTime)
        {
        }

        public void PlayCue(string name)
        {
        }

        public ICue GetCue(string name)
        {
            return new NoCue();
        }

        public ICue GetCue(string name, bool newInstance)
        {
            return new NoCue();
        }
    }
}
