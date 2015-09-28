using Bismuth.Framework.Assets.Composite;
using Bismuth.Framework.Composite;
using Bismuth.Framework.Content;
using Bismuth.Framework.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bismuth.Framework.Assets.Sprites
{
    public class SpriteUsingTextureAsset : NodeAsset
    {
        public string TextureAssetName { get; set; }
        public Rectangle Rectangle { get; set; }
        public Vector2 Origin { get; set; }
        public Color Color { get; set; }

        protected override INode CreateInstance()
        {
            return new Sprite();
        }

        protected override void LoadProperties(IContentManager contentManager, INode node)
        {
            base.LoadProperties(contentManager, node);

            Sprite sprite = (Sprite)node;
            sprite.Texture = contentManager.Load<Texture2D>(TextureAssetName);
            sprite.Rectangle = Rectangle;
            sprite.Origin = Origin;
            sprite.Color = Color;
        }
    }
}
