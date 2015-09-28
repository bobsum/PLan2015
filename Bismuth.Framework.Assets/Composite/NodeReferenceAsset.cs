using Bismuth.Framework.Composite;
using Bismuth.Framework.Content;

namespace Bismuth.Framework.Assets.Composite
{
    public class NodeReferenceAsset : NodeAsset
    {
        public string NodeAssetName { get; set; }

        public override object Load(IContentManager contentManager)
        {
            INode node = contentManager.Load<INode>(NodeAssetName, true);
            LoadProperties(contentManager, node);
            return node;
        }
    }
}
