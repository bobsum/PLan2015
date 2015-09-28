using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bismuth.Framework.Composite;
using Bismuth.Framework.GameObjects;

namespace Bismuth.Framework.Assets.GameObjects
{
    public class SceneAsset : GameObjectAsset
    {
        protected override INode CreateInstance()
        {
            return new Scene();
        }
    }
}
