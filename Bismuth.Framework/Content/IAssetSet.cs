using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bismuth.Framework.Content
{
    public interface IAssetSet : IAsset, IEnumerable<IAsset>
    {
        object Load(IContentManager contentManager, string assetName);
    }
}
