using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bismuth.Framework.Sprites;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework.Particles.Systems
{
    public class CompositeParticleSystem : IParticleSystem
    {
        public List<IParticleSystem> ParticleSystems { get { return _particleSystems; } }
        private readonly List<IParticleSystem> _particleSystems = new List<IParticleSystem>();
        private Pool<Particle> _pool;

        public Pool<Particle> Pool
        {
            get { return _pool; }
            set
            {
                _pool = value;
                for (int i = 0; i < _particleSystems.Count; i++)
                {
                    _particleSystems[i].Pool = _pool;
                }
            }
        }

        public int ParticleCount
        {
            get
            {
                int count = 0;
                for (int i = 0; i < _particleSystems.Count; i++)
                {
                    count += _particleSystems[i].ParticleCount;
                }
                return count;
            }
        }

        public void Emit(ParticleEmitter emitter)
        {
            for (int i = 0; i < _particleSystems.Count; i++)
            {
                _particleSystems[i].Emit(emitter);
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < _particleSystems.Count; i++)
            {
                _particleSystems[i].Update(gameTime);
            }
        }

        public void Draw(ISpriteBatch spriteBatch)
        {
            for (int i = 0; i < _particleSystems.Count; i++)
            {
                _particleSystems[i].Draw(spriteBatch);
            }
        }

        public void Clear()
        {
            for (int i = 0; i < _particleSystems.Count; i++)
            {
                _particleSystems[i].Clear();
            }
        }
    }
}
