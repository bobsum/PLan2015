using Bismuth.Framework.Assets.Composite;
using Bismuth.Framework.Composite;
using Bismuth.Framework.Content;
using Bismuth.Framework.GameObjects;

namespace Bismuth.Framework.Assets.GameObjects
{
    public class GameObjectAsset : NodeAsset
    {
        protected override INode CreateInstance()
        {
            return new GameObject();
        }

        protected override void LoadProperties(IContentManager contentManager, INode node)
        {
            base.LoadProperties(contentManager, node);

            GameObject gameObject = (GameObject)node;

            gameObject.LoadContent(contentManager);
        }
    }
}
