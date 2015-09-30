using Bismuth.Framework.Animations;
using Bismuth.Framework.Composite;
using Bismuth.Framework.Content;
using Bismuth.Framework.GameObjects;
using Bismuth.Framework.Primitives;
using Bismuth.Framework.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plan2015.Score.ScoreBoard.Actors
{
    public class Swap : Actor, ISprite, IPrimitive
    {
        public INode A { get; private set; }
        public INode B { get; private set; }

        public Animation Animation { get; set; }

        public override void LoadContent(IContentManager contentManager)
        {
            A = this.Find("A");
            B = this.Find("B");

            Animation = contentManager.Load<Animation>("Animations/SchoolSwap", true);
            Animation.Bind(this);
        }

        public override void Update(GameTime gameTime)
        {
            Animation.Update(gameTime);
        }

        public void Draw(ISpriteBatch spriteBatch)
        {
        }

        public void Draw(PrimitiveBatch primitiveBatch)
        {
        }
    }
}
