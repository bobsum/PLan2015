using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bismuth.Framework.Sprites;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework.Particles
{
    public interface IParticleSystem
    {
        Pool<Particle> Pool { get; set; }

        // NOTE: For performance tweeking.
        int ParticleCount { get; }

        void Emit(ParticleEmitter emitter);
        void Update(GameTime gameTime);

        // TODO: Include only on sprite based particle systems.
        void Draw(ISpriteBatch spriteBatch);

        void Clear();
    }
}
