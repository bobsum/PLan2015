using Bismuth.Framework.Assets.Composite;
using Bismuth.Framework.Composite;
using Bismuth.Framework.Content;
using Bismuth.Framework.Sprites;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework.Assets.Sprites
{
    public class SpriteDrawGroupAsset : NodeAsset
    {
        public bool IsScissorTestEnabled { get; set; }
        public Rectangle ScissorRectangle { get; set; }

        protected override INode CreateInstance()
        {
            return new SpriteDrawGroup();
        }

        protected override void LoadProperties(IContentManager contentManager, INode node)
        {
            base.LoadProperties(contentManager, node);

            SpriteDrawGroup spriteDrawGroup = (SpriteDrawGroup)node;
            spriteDrawGroup.IsScissorTestEnabled = IsScissorTestEnabled;
            spriteDrawGroup.ScissorRectangle = ScissorRectangle;
        }
    }
}
