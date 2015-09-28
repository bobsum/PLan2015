using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bismuth.Framework.Composite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bismuth.Framework.Sprites
{
    public class Sprite : Node, ISprite
    {
        public Sprite()
        {
            Color = Color.White;
        }
        public Sprite(Texture2D texture)
        {
            Texture = texture;
            Rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            Color = Color.White;
        }
        public Sprite(Texture2D texture, Rectangle rectangle)
        {
            Texture = texture;
            Rectangle = rectangle;
            Color = Color.White;
        }
        public Sprite(Texture2D texture, Vector2 origin)
        {
            Texture = texture;
            Rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            Origin = origin;
            Color = Color.White;
        }
        public Sprite(Texture2D texture, Rectangle rectangle, Vector2 origin)
        {
            Texture = texture;
            Rectangle = rectangle;
            Origin = origin;
            Color = Color.White;
        }

        public Texture2D Texture { get; set; }
        public Rectangle Rectangle { get; set; }
        public Vector2 Origin { get; set; }
        public Color Color { get; set; }

        public void Draw(ISpriteBatch spriteBatch)
        {
            if (Texture != null)
            {
                Vector2 origin = Origin;
                SpriteEffects effects = SpriteEffects.None;

                if (WorldFlipX)
                {
                    effects |= SpriteEffects.FlipHorizontally;
                    origin.X = Rectangle.Width - origin.X;
                }

                if (WorldFlipY)
                {
                    effects |= SpriteEffects.FlipVertically;
                    origin.Y = Rectangle.Height - origin.Y;
                }

                spriteBatch.Draw(Texture, WorldPosition, Rectangle, Color * WorldOpacity, WorldRotation, origin, new Vector2(WorldScaleX, WorldScaleY), effects, 0.0f);
            }
        }

        public override bool HitTest(Vector2 position)
        {
            if (IsVisible && Texture != null)
            {
                Vector2 localPosition = Vector2.Transform(position, Matrix.Invert(WorldTransform));
                Vector2 textureCoordinate = localPosition + Origin;

                BoundingBox2 textureBox = new BoundingBox2(Vector2.Zero, new Vector2(Texture.Width, Texture.Height));
                Rectangle r = textureBox.Intersection(new BoundingBox2(Rectangle)).ToRectangle();

                int x = (int)textureCoordinate.X;
                int y = (int)textureCoordinate.Y;

                // TODO: Optimize this!
                if (x >= 0 && y >= 0 && x < r.Width && y < r.Height)
                {
                    int[] data = new int[r.Width * r.Height];
                    Texture.GetData(0, r, data, 0, data.Length);
                    if (data[x + y * r.Width] != 0) return true;
                }
            }

            return false;
        }

        public override BoundingBox2 GetBounds()
        {
            return new BoundingBox2(0, 0, Rectangle.Width, Rectangle.Height) - Origin;
        }

        public override INode Clone()
        {
            INode node = new Sprite();
            CopyTo(node);
            return node;
        }

        protected override void CopyTo(INode node)
        {
            base.CopyTo(node);

            Sprite sprite = (Sprite)node;
            sprite.Texture = Texture;
            sprite.Rectangle = Rectangle;
            sprite.Origin = Origin;
            sprite.Color = Color;
        }
    }
}
