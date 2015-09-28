using Bismuth.Framework.Assets.Composite;
using Bismuth.Framework.Composite;
using Bismuth.Framework.Content;
using Bismuth.Framework.Particles;
using Microsoft.Xna.Framework.Content;

namespace Bismuth.Framework.Assets.Particles
{
    public class ParticleEmitterAsset : NodeAsset
    {
        public string ParticleSystemName { get; set; }
        public bool IsEnabled { get; set; }
        public float EmitRate { get; set; }

        protected override INode CreateInstance()
        {
            return new ParticleEmitter();
        }

        protected override void LoadProperties(IContentManager contentManager, INode node)
        {
            base.LoadProperties(contentManager, node);

            ParticleEmitter emitter = (ParticleEmitter)node;
            emitter.ParticleSystemName = ParticleSystemName;
            emitter.IsEnabled = IsEnabled;
            emitter.EmitRate = EmitRate;
        }
    }
}
