using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bismuth.Framework.Composite;

namespace Bismuth.Framework.Primitives
{
    public interface IPrimitive : INode
    {
        void Draw(PrimitiveBatch primitiveBatch);
    }
}
