using Bismuth.Framework.Assets.Composite;
using Bismuth.Framework.Composite;
using Bismuth.Framework.Content;
using Bismuth.Framework.Sprites;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework.Assets.Sprites
{
    public class SpriteAsset : NodeAsset
    {
        public string FrameAssetName { get; set; }
        public Color Color { get; set; }

        protected override INode CreateInstance()
        {
            return new Sprite();
        }

        protected override void LoadProperties(IContentManager contentManager, INode node)
        {
            base.LoadProperties(contentManager, node);

            SpriteFrame frame = contentManager.Load<SpriteFrame>(FrameAssetName);

            Sprite sprite = (Sprite)node;
            sprite.Texture = frame.Texture;
            sprite.Rectangle = frame.Rectangle;
            sprite.Origin = frame.Origin;
            sprite.Color = Color;
        }
    }
}
