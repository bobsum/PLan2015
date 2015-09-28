using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework.Particles
{
    public class Particle
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public float Rotation;
        public float AngularVelocity;
        public Vector4 Color;
        public float Scale;
        public float Timer;
        public float Duration;
        public int Data;

        public LinkedListNode<Particle> Node { get; private set; }

        public Particle()
        {
            Node = new LinkedListNode<Particle>(this);
        }
    }
}
