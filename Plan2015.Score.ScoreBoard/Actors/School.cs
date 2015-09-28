using Bismuth.Framework;
using Bismuth.Framework.Animations;
using Bismuth.Framework.Composite;
using Bismuth.Framework.Content;
using Bismuth.Framework.GameObjects;
using Bismuth.Framework.Primitives;
using Bismuth.Framework.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plan2015.Score.ScoreBoard.Actors
{
    public class School : Actor, ISprite, IPrimitive
    {
        public SpriteFont Font { get; private set; }
        public Animation Hover { get; private set; }

        public INode ScorePosition { get; set; }

        public string LogoTexture { get; set; }

        public override void LoadContent(IContentManager contentManager)
        {
            ScorePosition = this.Find("Score");

            Font = contentManager.Load<SpriteFont>("Fonts/Gabriola");
            Hover = contentManager.Load<Animation>("Animations/Hover", true);
            Hover.Bind(this);
            Hover.Play();
            Hover.FrameTime = RandomHelper.Next(0f, 60f);
            Hover.Fps = RandomHelper.Next(9f, 11f);
        }

        public override void Update(GameTime gameTime)
        {
            Hover.Update(gameTime);
        }

        public virtual void Draw(ISpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, "123", ScorePosition.WorldPosition, Color.White);
        }

        public virtual void Draw(PrimitiveBatch primitiveBatch)
        {
        }
    }
}
