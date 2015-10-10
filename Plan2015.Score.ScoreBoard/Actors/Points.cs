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
using System.Threading.Tasks;

namespace Plan2015.Score.ScoreBoard.Actors
{
    public class Points : Actor, ISprite, IPrimitive
    {
        public Animation Animation { get; set; }
        public int Value { get; set; }
        public SpriteFont Font { get; set; }
        public INode NumberPosition { get; set; }
        public string ValueString { get; set; }

        public void Setup(IContentManager contentManager, int value)
        {
            NumberPosition = new Node();
            NumberPosition.Name = "Root";
            Children.Add(NumberPosition);

            Value = value;
            ValueString = Value.ToString();
            if (Value > 0) ValueString = "+" + ValueString;

            Font = contentManager.Load<SpriteFont>("Fonts/Gabriola200");
            Animation = contentManager.Load<Animation>(value < 0 ? "Animations/PointsDown" : "Animations/PointsUp", true);
            Animation.Bind(this);
            Animation.Play();
        }

        public override void Update(GameTime gameTime)
        {
            Animation.Update(gameTime);
            if (Animation.State == AnimationState.Finished)
            {
                Parent.Children.Remove(this);
            }
        }

        public virtual void Draw(ISpriteBatch spriteBatch)
        {
            Color color = Value > 0 ? Color.LightGreen : Color.Red;
            color *= NumberPosition.Opacity;

            Vector2 measure = Font.MeasureString(ValueString);
            Vector2 origin = new Vector2(measure.X, measure.Y) * 0.5f;

            spriteBatch.DrawString(Font, ValueString, NumberPosition.WorldPosition, color, NumberPosition.WorldRotation, origin, NumberPosition.WorldScale, SpriteEffects.None, 0);
        }

        public virtual void Draw(PrimitiveBatch primitiveBatch)
        {
        }
    }
}
