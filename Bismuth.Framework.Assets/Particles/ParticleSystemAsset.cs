using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bismuth.Framework.Content;
using Microsoft.Xna.Framework.Content;

namespace Bismuth.Framework.Assets.Particles
{
    public class ParticleSystemAsset : IAsset
    {
        [ContentSerializer(Optional = true)]
        public string Name { get; set; }

        [ContentSerializer(Optional = true)]
        public string Class { get; set; }

        public virtual object Load(IContentManager contentManager)
        {
            throw new NotImplementedException();
        }
    }
}
