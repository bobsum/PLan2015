using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bismuth.Framework.Composite;
using Bismuth.Framework.Content;

namespace Bismuth.Framework.GameObjects
{
    public class GameObject : Node
    {
        public virtual void Initialize()
        {
        }

        public virtual void LoadContent(IContentManager contentManager)
        {
        }
    }
}
