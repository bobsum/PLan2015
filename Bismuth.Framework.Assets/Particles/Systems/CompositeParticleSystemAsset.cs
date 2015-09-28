using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bismuth.Framework.Content;
using Bismuth.Framework.Particles;
using Bismuth.Framework.Particles.Systems;

namespace Bismuth.Framework.Assets.Particles.Systems
{
    public class CompositeParticleSystemAsset : ParticleSystemAsset
    {
        public List<ParticleSystemAsset> ParticleSystems { get; set; }

        public override object Load(IContentManager contentManager)
        {
            CompositeParticleSystem compositeParticleSystem = new CompositeParticleSystem();

            for (int i = 0; i < ParticleSystems.Count; i++)
            {
                compositeParticleSystem.ParticleSystems.Add((IParticleSystem)ParticleSystems[i].Load(contentManager));
            }

            return compositeParticleSystem;
        }
    }
}
