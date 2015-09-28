using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bismuth.Framework.Composite;

namespace Bismuth.Framework.Particles
{
    public static class ParticleEmitterTree
    {
        public static void Bind(INode node, IDictionary<string, IParticleSystem> particleSystems)
        {
            IParticleEmitter emitter = node as IParticleEmitter;
            if (emitter != null && !string.IsNullOrEmpty(emitter.ParticleSystemName))
            {
                IParticleSystem particleSystem;
                if (particleSystems.TryGetValue(emitter.ParticleSystemName, out particleSystem))
                {
                    emitter.ParticleSystem = particleSystem;
                }
            }

            for (int i = 0; i < node.Children.Count; i++)
            {
                Bind(node.Children[i], particleSystems);
            }
        }
    }
}
