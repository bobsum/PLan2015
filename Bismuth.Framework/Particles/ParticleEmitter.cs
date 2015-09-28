using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bismuth.Framework.Composite;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework.Particles
{
    public class ParticleEmitter : Node, IParticleEmitter
    {
        private float _timer = 0;

        public ParticleEmitter()
        {
            IsEnabled = true;
        }

        public string ParticleSystemName { get; set; }
        public IParticleSystem ParticleSystem { get; set; }

        public bool IsEnabled { get; set; }
        public float EmitRate { get; set; }

        public void Emit()
        {
            if (ParticleSystem != null)
            {
                ParticleSystem.Emit(this);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (IsEnabled && EmitRate > 0)
            {
                float elapsedTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                _timer += elapsedTime;

                while (_timer > EmitRate)
                {
                    _timer -= EmitRate;
                    Emit();
                }
            }
        }

        public override INode Clone()
        {
            INode node = new ParticleEmitter();
            CopyTo(node);
            return node;
        }

        protected override void CopyTo(INode node)
        {
            base.CopyTo(node);

            ParticleEmitter emitter = (ParticleEmitter)node;
            emitter.ParticleSystemName = ParticleSystemName;
            emitter.ParticleSystem = ParticleSystem;
            emitter.EmitRate = EmitRate;
        }
    }
}
