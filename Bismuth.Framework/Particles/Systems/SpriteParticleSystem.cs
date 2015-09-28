using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bismuth.Framework.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bismuth.Framework.Particles.Systems
{
    public enum ParticleAnimationMode
    {
        Animated,
        AnimatedWithRandomStartFrame,
        AnimatedOverDuration,
        RandomFrame
    }

    public enum ParticleColorMode
    {
        Animated,
        Random
    }

    public enum InitialVelocityMode
    {
        Angle,
        AwayFromCenter
    }

    public enum RotationMode
    {
        Custom,
        Velocity
    }

    public class SpriteParticleSystem : IParticleSystem
    {
        public Pool<Particle> Pool { get; set; }
        private readonly LinkedList<Particle> _particles = new LinkedList<Particle>();

        public int ParticleCount { get { return _particles.Count; } }

        public SpriteFrame[] SpriteFrames { get; set; }
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

        public float FadeInDuration
        {
            get { return _fadeInDuration; }
            set
            {
                _fadeInDuration = value;
                _inverseFadeInDuration = _fadeInDuration != 0 ? 1.0f / _fadeInDuration : 0;
            }
        }

        public float FadeOutDuration
        {
            get { return _fadeOutDuration; }
            set
            {
                _fadeOutDuration = value;
                _inverseFadeOutDuration = _fadeOutDuration != 0 ? 1.0f / _fadeOutDuration : 0;
            }
        }

        private float _fadeInDuration;
        private float _fadeOutDuration;
        private float _inverseFadeInDuration;
        private float _inverseFadeOutDuration;

        public void Emit(ParticleEmitter emitter)
        {
            int emitCount = EmitCount.GetValue();
            for (int i = 0; i < emitCount; i++)
            {
                Particle particle = Pool.Fetch();
                _particles.AddLast(particle.Node);

                Emit(emitter, particle);
            }
        }

        protected virtual void Emit(ParticleEmitter emitter, Particle particle)
        {
            Matrix transform = Matrix.CreateRotationZ(emitter.WorldRotation);

            // Generating spawn position.
            float emitAngle = RandomHelper.Next(0, MathHelper.TwoPi);
            float emitRadius = EmitRadius.GetValue();

            Vector2 p = EmitPosition.GetValue() + new Polar2(emitRadius, emitAngle).ToVector2();

            // Generating initial velocity.
            float initialAngle = EmitAngle.GetValue();
            float initialVelocity = InitialVelocity.GetValue();

            Vector2 v;
            if (InitialVelocityMode == InitialVelocityMode.AwayFromCenter && p != Vector2.Zero)
            {
                v = Vector2.Normalize(p) * initialVelocity;
            }
            else
            {
                v = new Polar2(initialVelocity, initialAngle).ToVector2();
            }

            particle.Position = emitter.WorldPosition + Vector2.Transform(p, transform);
            particle.Rotation = emitter.WorldRotation + InitialRotation.GetValue();
            particle.Velocity = Vector2.Transform(v, transform);
            particle.AngularVelocity = InitialAngularVelocity.GetValue();
            particle.Duration = Duration.GetValue();
            particle.Timer = 0;
            particle.Scale = InitialScale.GetValue();

            if (SpriteFrames != null)
                particle.Data = RandomHelper.Next(0, SpriteFrames.Length);

            if (ColorMode == ParticleColorMode.Random)
                particle.Color = Colors[RandomHelper.Next(0, Colors.Length)].ToVector4();
            else
                particle.Color = Colors[0].ToVector4();
        }

        public void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            LinkedListNode<Particle> node = _particles.First;

            while (node != null)
            {
                Particle particle = node.Value;
                node = node.Next;

                particle.Timer += elapsedTime;

                Update(gameTime, particle);

                if (particle.Timer > particle.Duration)
                {
                    _particles.Remove(particle.Node);
                    Pool.Insert(particle);
                }
            }
        }

        protected virtual void Update(GameTime gameTime, Particle particle)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (Damping > 0)
            {
                particle.Velocity *= Damping;
            }

            particle.Velocity += Gravity * elapsedTime;
            particle.Position += particle.Velocity * elapsedTime * 0.001f;

            if (RotationMode == RotationMode.Velocity)
            {
                particle.Rotation = (float)Math.Atan2(particle.Velocity.Y, particle.Velocity.X);
            }
            else
            {
                particle.Rotation += particle.AngularVelocity * elapsedTime * 0.001f;
            }

            particle.Scale *= ScaleFactor;

            if (ColorMode == ParticleColorMode.Animated && Colors.Length > 1 && particle.Duration > 0)
            {
                int l = Colors.Length - 1;
                float p = particle.Timer / particle.Duration * l;
                int i = (int)p;
                float f = p - i;

                if (i < l)
                    particle.Color = Colors[i].ToVector4() * (1 - f) + Colors[i + 1].ToVector4() * f;
                else
                    particle.Color = Colors[l].ToVector4();
            }
        }

        protected void GetFadeColor(Particle particle, out Vector4 color)
        {
            color = particle.Color;

            float timeLeft = particle.Duration - particle.Timer;

            if (particle.Timer < _fadeInDuration)
            {
                color *= particle.Timer * _inverseFadeInDuration;
            }

            if (timeLeft < _fadeOutDuration)
            {
                color *= timeLeft * _inverseFadeOutDuration;
            }
        }

        public void Draw(ISpriteBatch spriteBatch)
        {
            LinkedListNode<Particle> node = _particles.First;

            while (node != null)
            {
                Particle particle = node.Value;
                node = node.Next;
                Draw(spriteBatch, particle);
            }
        }

        protected virtual void Draw(ISpriteBatch spriteBatch, Particle particle)
        {
            Vector4 color;
            GetFadeColor(particle, out color);

            int frameIndex = 0;

            if (AnimationMode == ParticleAnimationMode.Animated)
                frameIndex = 0;
            //frameIndex = (int)(particle.Timer * Animation.FrameTimeSpan) % Animation.Frames.Length;

            else if (AnimationMode == ParticleAnimationMode.AnimatedWithRandomStartFrame)
                frameIndex = 0;
            //frameIndex = (int)(particle.Timer * Animation.FrameTimeSpan + particle.Data) % Animation.Frames.Length;

            else if (AnimationMode == ParticleAnimationMode.AnimatedOverDuration)
                frameIndex = (int)((particle.Timer / particle.Duration) * SpriteFrames.Length);

            else if (AnimationMode == ParticleAnimationMode.RandomFrame)
                frameIndex = particle.Data;

            if (frameIndex >= SpriteFrames.Length) frameIndex = SpriteFrames.Length - 1;

            SpriteFrame frame = SpriteFrames[frameIndex];

            spriteBatch.Draw(frame.Texture, particle.Position, frame.Rectangle, new Color(color), particle.Rotation, frame.Origin, particle.Scale, SpriteEffects.None, 0.0f);
        }

        public void Clear()
        {
            LinkedListNode<Particle> node = _particles.First;

            while (node != null)
            {
                Particle particle = node.Value;
                node = node.Next;

                _particles.Remove(particle.Node);
                Pool.Insert(particle);
            }
        }
    }
}
