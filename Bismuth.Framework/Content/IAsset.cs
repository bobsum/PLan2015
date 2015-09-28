using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bismuth.Framework.Content
{
    public interface IAsset
    {
        string Name { get; set; }
        object Load(IContentManager contentManager);
    }
}
