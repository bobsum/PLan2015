using Bismuth.Framework.Composite;

namespace Bismuth.Framework.Sprites
{
    public static class SpriteTree
    {
        public static void Draw(INode node, ISpriteBatch spriteBatch)
        {
            if (node.IsVisible)
            {
                ISprite sprite = node as ISprite;
                if (sprite != null)
                {
                    sprite.Draw(spriteBatch);
                    // A sprite draw group draws the sub-tree itself.
                    if (sprite is ISpriteDrawGroup) return;
                }

                for (int i = 0; i < node.Children.Count; i++)
                {
                    Draw(node.Children[i], spriteBatch);
                }
            }
        }
    }
}
