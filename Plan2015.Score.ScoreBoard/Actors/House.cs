using Bismuth.Framework.Composite;
using Bismuth.Framework.Content;
using Bismuth.Framework.GameObjects;
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
    public class House : Actor, ISprite, IPrimitive
    {
        public SpriteFont Font { get; private set; }
        public INode NamePosition { get; set; }
        public INode ScorePosition { get; set; }

        public HouseScore Score { get; set; }

        public override void LoadContent(IContentManager contentManager)
        {
            NamePosition = this.Find("Name");
            ScorePosition = this.Find("Score");

            Font = contentManager.Load<SpriteFont>("Fonts/Gabriola");
        }

        public void Draw(ISpriteBatch spriteBatch)
        {
            DrawName(spriteBatch);
            DrawScore(spriteBatch);
        }

        private void DrawName(ISpriteBatch spriteBatch)
        {
            Vector2 measure = Font.MeasureString(Score.Name);
            Vector2 origin = new Vector2(0, measure.Y * 0.5f);

            spriteBatch.DrawString(Font, Score.Name, NamePosition.WorldPosition, Color.White, WorldRotation, origin, WorldScale, SpriteEffects.None, 0);
        }

        private void DrawScore(ISpriteBatch spriteBatch)
        {
            string scoreString = Score.Amount.ToString();

            Vector2 measure = Font.MeasureString(scoreString);
            Vector2 origin = new Vector2(measure.X, measure.Y * 0.5f);

            spriteBatch.DrawString(Font, scoreString, ScorePosition.WorldPosition, Color.White, WorldRotation, origin, WorldScale, SpriteEffects.None, 0);
        }

        public void Draw(PrimitiveBatch primitiveBatch)
        {
        }
    }
}
