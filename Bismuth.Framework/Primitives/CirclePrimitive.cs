using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bismuth.Framework.Composite;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework.Primitives
{
    public class CirclePrimitive : Node, IPrimitive
    {
        public float Radius { get; set; }

        public void Draw(PrimitiveBatch primitiveBatch)
        {
            PrimitiveBrush pb = new PrimitiveBrush();
            pb.BorderAlignment = BorderAlignment.Center;
            pb.BorderThickness = 4;
            pb.BorderColor = Color.Red;
            pb.FillColor = Color.Transparent;

            primitiveBatch.DrawCircle(Vector2.Zero, Radius, pb, WorldTransform);
        }

        public override INode Clone()
        {
            INode node = new CirclePrimitive();
            return node;
        }

        protected override void CopyTo(INode node)
        {
            base.CopyTo(node);

            CirclePrimitive circlePrimitive = (CirclePrimitive)node;
            circlePrimitive.Radius = Radius;
        }
    }
}
