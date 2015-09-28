using Bismuth.Framework;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework.Physics.Shapes
{
    public class CircleShape : IShape
    {
        public CircleShape(float radius)
        {
            Radius = radius;
        }

        public float Radius { get; set; }

        public BoundingBox2 BoundingBox
        {
            get { return new BoundingBox2(new Vector2(-Radius), new Vector2(Radius)); }
        }

        public float ComputeMomentOfInertia(float mass)
        {
            return 0.5f * mass * Radius * Radius;
        }
    }
}
