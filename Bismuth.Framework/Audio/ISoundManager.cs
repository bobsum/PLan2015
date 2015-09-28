using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework.Audio
{
    public interface ISoundManager
    {
        void Update(GameTime gameTime);
        void PlayCue(string name);
        ICue GetCue(string name);
        ICue GetCue(string name, bool newInstance);
    }
}
