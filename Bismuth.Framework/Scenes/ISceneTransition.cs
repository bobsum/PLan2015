using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework.Scenes
{
    public interface ISceneTransition
    {
        bool IsFinished { get; }
        void Begin(Scene currentScene, Scene nextScene);
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
    }
}
