using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bismuth.Framework.Assets.Composite;
using Bismuth.Framework.Composite;
using Bismuth.Framework.Primitives;

namespace Bismuth.Framework.Assets.Primitives
{
    public class CirclePrimitiveAsset : NodeAsset
    {
        public float Radius { get; set; }

        protected override INode CreateInstance()
        {
            return new CirclePrimitive();
        }

        protected override void LoadProperties(Content.IContentManager contentManager, INode node)
        {
            base.LoadProperties(contentManager, node);

            CirclePrimitive circlePrimitive = (CirclePrimitive)node;
            circlePrimitive.Radius = Radius;
        }
    }
}
