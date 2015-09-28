using Microsoft.Xna.Framework;

namespace Bismuth.Framework
{
    /// <summary>
    /// Defines a bounding-circle.
    /// </summary>
    public struct BoundingCircle
    {
        public Vector2 Center;
        public float Radius;

        public BoundingCircle(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        public static BoundingCircle operator +(BoundingCircle value1, Vector2 value2)
        {
            return new BoundingCircle(value1.Center + value2, value1.Radius);
        }

        public static BoundingCircle operator -(BoundingCircle value1, Vector2 value2)
        {
            return new BoundingCircle(value1.Center - value2, value1.Radius);
        }

        public static BoundingCircle operator +(Vector2 value1, BoundingCircle value2)
        {
            return new BoundingCircle(value1 + value2.Center, value2.Radius);
        }

        public static BoundingCircle operator -(Vector2 value1, BoundingCircle value2)
        {
            return new BoundingCircle(value1 - value2.Center, value2.Radius);
        }

        public bool Contains(Vector2 value)
        {
            return Vector2.DistanceSquared(value, Center) < Radius * Radius;
        }

        public bool Intersects(BoundingCircle value)
        {
            float totalRadius = value.Radius + Radius;
            return Vector2.DistanceSquared(value.Center, Center) < totalRadius * totalRadius;
        }

        public bool Intersects(BoundingBox2 value)
        {
            if (Center.X < value.Min.X && Center.Y < value.Min.Y)
                return Contains(value.Min);

            if (Center.X > value.Max.X && Center.Y < value.Min.Y)
                return Contains(new Vector2(value.Max.X, value.Min.Y));

            if (Center.X < value.Min.X && Center.Y > value.Max.Y)
                return Contains(new Vector2(value.Min.X, value.Max.Y));

            if (Center.X > value.Max.X && Center.Y > value.Max.Y)
                return Contains(value.Max);

            return value.Intersects(BoundingBox());
        }

        public bool Intersects(BoundingBox2 value, out Vector2? corner)
        {
            corner = null;

            if (Center.X < value.Min.X && Center.Y < value.Min.Y)
                corner = value.Min;

            if (Center.X > value.Max.X && Center.Y < value.Min.Y)
                corner = new Vector2(value.Max.X, value.Min.Y);

            if (Center.X < value.Min.X && Center.Y > value.Max.Y)
                corner = new Vector2(value.Min.X, value.Max.Y);

            if (Center.X > value.Max.X && Center.Y > value.Max.Y)
                corner = value.Max;

            if (corner != null)
                return Contains(corner.Value);

            return value.Intersects(BoundingBox());
        }

        public BoundingBox2 BoundingBox()
        {
            return new BoundingBox2(-Radius, Radius) + Center;
        }
    }
}
