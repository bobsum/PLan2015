using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bismuth.Framework.Composite;

namespace Bismuth.Framework.Particles
{
    public interface IParticleEmitter : INode
    {
        string ParticleSystemName { get; set; }
        IParticleSystem ParticleSystem { get; set; }

        bool IsEnabled { get; set; }
        float EmitRate { get; set; }

        void Emit();
    }
}
