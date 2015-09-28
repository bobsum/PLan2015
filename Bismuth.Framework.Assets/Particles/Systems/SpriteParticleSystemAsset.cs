using Bismuth.Framework.Content;
using Bismuth.Framework.Particles.Systems;
using Bismuth.Framework.Sprites;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework.Assets.Particles.Systems
{
    public class SpriteParticleSystemAsset : ParticleSystemAsset
    {
        public string SpriteSheetAssetName { get; set; }
        public Color[] Colors { get; set; }

        public ParticleAnimationMode AnimationMode { get; set; }
        public ParticleColorMode ColorMode { get; set; }
        public InitialVelocityMode InitialVelocityMode { get; set; }
        public RotationMode RotationMode { get; set; }

        public RandomInt EmitCount { get; set; }
        public RandomVector2 EmitPosition { get; set; }
        public RandomFloat EmitRadius { get; set; }
        public RandomFloat EmitAngle { get; set; }

        public RandomFloat InitialVelocity { get; set; }
        public RandomFloat InitialRotation { get; set; }
        public RandomFloat InitialAngularVelocity { get; set; }
        public RandomFloat InitialScale { get; set; }

        public RandomFloat Duration { get; set; }
        public Vector2 Gravity { get; set; }
        public float Damping { get; set; }
        public float ScaleFactor { get; set; }

        public float FadeInDuration { get; set; }
        public float FadeOutDuration { get; set; }

        public override object Load(IContentManager contentManager)
        {
            SpriteParticleSystem spriteParticleSystem = new SpriteParticleSystem();
            spriteParticleSystem.SpriteFrames = contentManager.Load<SpriteFrame[]>(SpriteSheetAssetName);
            spriteParticleSystem.Colors = Colors;
            spriteParticleSystem.AnimationMode = AnimationMode;
            spriteParticleSystem.ColorMode = ColorMode;
            spriteParticleSystem.InitialVelocityMode = InitialVelocityMode;
            spriteParticleSystem.RotationMode = RotationMode;
            spriteParticleSystem.EmitCount = EmitCount;
            spriteParticleSystem.EmitPosition = EmitPosition;
            spriteParticleSystem.EmitRadius = EmitRadius;
            spriteParticleSystem.EmitAngle = EmitAngle;
            spriteParticleSystem.InitialVelocity = InitialVelocity;
            spriteParticleSystem.InitialRotation = InitialRotation;
            spriteParticleSystem.InitialAngularVelocity = InitialAngularVelocity;
            spriteParticleSystem.InitialScale = InitialScale;
            spriteParticleSystem.Duration = Duration;
            spriteParticleSystem.Gravity = Gravity;
            spriteParticleSystem.Damping = Damping;
            spriteParticleSystem.ScaleFactor = ScaleFactor;
            spriteParticleSystem.FadeInDuration = FadeInDuration;
            spriteParticleSystem.FadeOutDuration = FadeOutDuration;
            return spriteParticleSystem;
        }
    }
}
