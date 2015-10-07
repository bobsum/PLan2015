using Bismuth.Framework;
using Bismuth.Framework.Animations;
using Bismuth.Framework.Composite;
using Bismuth.Framework.Content;
using Bismuth.Framework.GameObjects;
using Bismuth.Framework.Particles;
using Bismuth.Framework.Primitives;
using Bismuth.Framework.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Plan2015.Score.Client;
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

        public SchoolScore Score { get; set; }

        public ListBox HouseListBox { get; set; }

        public ParticleEmitter MagicExplosionEmitter { get; private set; }

        public override void LoadContent(IContentManager contentManager)
        {
            ScorePosition = this.Find("Score");
            HouseListBox = this.Find<ListBox>("HouseListBox");
            HouseListBox.Comparer = HouseComparer;

            Font = contentManager.Load<SpriteFont>("Fonts/Gabriola200");
            Hover = contentManager.Load<Animation>("Animations/SchoolHover", true);
            Hover.Bind(this);
            Hover.Play();
            Hover.FrameTime = RandomHelper.Next(0f, 60f);
            Hover.Fps = RandomHelper.Next(9f, 11f);

            MagicExplosionEmitter = this.Find<ParticleEmitter>("MagicExplosionEmitter");
        }

        private bool HouseComparer(INode a, INode b)
        {
            return ((House)a).Score.Amount < ((House)b).Score.Amount;
        }

        public override void Update(GameTime gameTime)
        {
            Hover.Update(gameTime);
        }

        public virtual void Draw(ISpriteBatch spriteBatch)
        {
            string scoreString = Score.Amount.ToString();

            Vector2 measure = Font.MeasureString(scoreString);
            Vector2 origin = new Vector2(measure.X, measure.Y * 0.5f);

            spriteBatch.DrawString(Font, scoreString, ScorePosition.WorldPosition, Color.White, WorldRotation, origin, WorldScale, SpriteEffects.None, 0);
        }

        public virtual void Draw(PrimitiveBatch primitiveBatch)
        {
        }
    }
}
