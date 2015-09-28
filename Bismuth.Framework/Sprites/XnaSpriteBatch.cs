using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bismuth.Framework.Sprites
{
    public class XnaSpriteBatch : ISpriteBatch
    {
        private readonly SpriteBatch _spriteBatch;
        private Vector2[] _translationBuffer = new Vector2[1000];
        private int _translationCount = 0;
        private Rectangle[] _scissorRectangleBuffer = new Rectangle[1000];
        private int _scissorRectangleCount = 0;
        private Matrix _transformMatrix = Matrix.Identity;

        public XnaSpriteBatch(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }

        public void Begin(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState, Effect effect, Matrix transformMatrix)
        {
            _transformMatrix = transformMatrix;
            _spriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);
        }

        public void End()
        {
            _spriteBatch.End();
        }

        public void PushTranslation(Vector2 position)
        {
            if (_translationCount > 0)
                _translationBuffer[_translationCount] = _translationBuffer[_translationCount - 1] + position;
            else
                _translationBuffer[_translationCount] = position;

            _translationCount++;
        }

        public void PopTranslation()
        {
            _translationCount--;
        }

        public void PushScissorRectangle(Rectangle rectangle)
        {
            if (_transformMatrix != Matrix.Identity)
            {
                BoundingBox2 b = new BoundingBox2(rectangle);
                b.Min = Vector2.Transform(b.Min, _transformMatrix);
                b.Max = Vector2.Transform(b.Max, _transformMatrix);
                rectangle = b.ToRectangle();
            }

            Viewport v = _spriteBatch.GraphicsDevice.Viewport;
            rectangle.X += v.X;
            rectangle.Y += v.Y;

            _scissorRectangleBuffer[_scissorRectangleCount] = _spriteBatch.GraphicsDevice.ScissorRectangle;
            _scissorRectangleCount++;
            _spriteBatch.GraphicsDevice.ScissorRectangle = rectangle;
        }

        public void PopScissorRectangle()
        {
            _scissorRectangleCount--;
            _spriteBatch.GraphicsDevice.ScissorRectangle = _scissorRectangleBuffer[_scissorRectangleCount];
        }

        public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        {
            if (_translationCount > 0)
                position += _translationBuffer[_translationCount - 1];

            _spriteBatch.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
        }

        public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            if (_translationCount > 0)
                position += _translationBuffer[_translationCount - 1];

            _spriteBatch.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
        }

        public void DrawString(SpriteFont font, string text, Vector2 position, Color color)
        {
            if (_translationCount > 0)
                position += _translationBuffer[_translationCount - 1];

            _spriteBatch.DrawString(font, text, position, color);
        }
    }
}
