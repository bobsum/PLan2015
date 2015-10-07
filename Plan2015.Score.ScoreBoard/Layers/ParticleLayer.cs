using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bismuth.Framework.Composite;
using Bismuth.Framework.Sprites;
using Bismuth.Framework.Particles;
using Microsoft.Xna.Framework;

namespace Plan2015.Score.ScoreBoard.Layers
{
    public class ParticleLayer : Node, ISprite
    {
        public string ParticleSystemAssetName { get; set; }
        public IParticleSystem ParticleSystem { get; set; }

        public void LoadContent(MainGame game)
        {
            ParticleSystem = game.ContentManager.Load<IParticleSystem>(ParticleSystemAssetName);
            ParticleSystem.Pool = game.Pool;
        }

        public override void Update(GameTime gameTime)
        {
            ParticleSystem.Update(gameTime);
        }

        public void Draw(ISpriteBatch spriteBatch)
        {
            ParticleSystem.Draw(spriteBatch);
        }

        public void Bind(INode node)
        {
            ParticleEmitter particleEmitter = node as ParticleEmitter;
            if (particleEmitter != null && particleEmitter.ParticleSystemName == Name)
            {
                particleEmitter.ParticleSystem = ParticleSystem;
            }

            for (int i = 0; i < node.Children.Count; i++)
            {
                Bind(node.Children[i]);
            }
        }
    }
}
