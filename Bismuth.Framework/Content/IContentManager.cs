using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bismuth.Framework.Content
{
    public interface IContentManager
    {
        T Load<T>(string assetName);
        T Load<T>(string assetName, bool newInstance);

        void Unload();
    }
}
