using Bismuth.Framework.Composite;

namespace Bismuth.Framework.Sprites
{
    public interface ISprite : INode
    {
        void Draw(ISpriteBatch spriteBatch);
    }
}
