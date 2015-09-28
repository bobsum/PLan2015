using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bismuth.Framework.Composite;

namespace Bismuth.Framework.Primitives
{
    public static class PrimitiveTree
    {
        public static void Draw(INode node, PrimitiveBatch primitiveBatch)
        {
            IPrimitive primitive = node as IPrimitive;
            if (primitive != null)
            {
                primitive.Draw(primitiveBatch);
            }

            for (int i = 0; i < node.Children.Count; i++)
            {
                Draw(node.Children[i], primitiveBatch);
            }
        }
    }
}
