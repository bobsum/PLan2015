using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework
{
    public struct BoundingCapsule2
    {
        public Vector2 Min;
        public Vector2 Max;
        public float Radius;

        public BoundingCapsule2(Vector2 min, Vector2 max, float radius)
        {
            Min = min;
            Max = max;
            Radius = radius;
        }

        public static BoundingCapsule2 operator +(BoundingCapsule2 value1, Vector2 value2)
        {
            return new BoundingCapsule2(value1.Min + value2, value1.Max + value2, value1.Radius);
        }

        public static BoundingCapsule2 operator -(BoundingCapsule2 value1, Vector2 value2)
        {
            return new BoundingCapsule2(value1.Min - value2, value1.Max - value2, value1.Radius);
        }

        public static BoundingCapsule2 operator +(Vector2 value1, BoundingCapsule2 value2)
        {
            return new BoundingCapsule2(value1 + value2.Min, value1 + value2.Max, value2.Radius);
        }

        public static BoundingCapsule2 operator -(Vector2 value1, BoundingCapsule2 value2)
        {
            return new BoundingCapsule2(value1 - value2.Min, value1 - value2.Max, value2.Radius);
        }

        public bool Intersects(BoundingCircle circle)
        {
            float totalRadius = Radius + circle.Radius;
            return Distance(circle).LengthSquared() < totalRadius * totalRadius;
        }

        public bool Intersects(BoundingCapsule2 capsule)
        {
            return Intersects(this, capsule);
        }

        public static bool Intersects(BoundingCapsule2 value1, BoundingCapsule2 value2)
        {
            float totalRadius = value1.Radius + value2.Radius;
            return Distance(value1, value2).LengthSquared() < totalRadius * totalRadius;
        }

        public Vector2 Distance(BoundingCircle circle)
        {
            return PointToLine(Min, Max, circle.Center);
        }

        public Vector2 Distance(BoundingCapsule2 capsule)
        {
            return Distance(this, capsule);
        }

        public static Vector2 Distance(BoundingCapsule2 value1, BoundingCapsule2 value2)
        {
            Vector2 d1 = -PointToLine(value2.Min, value2.Max, value1.Min);
            Vector2 d2 = -PointToLine(value2.Min, value2.Max, value1.Max);
            Vector2 d3 = PointToLine(value1.Min, value1.Max, value2.Min);
            Vector2 d4 = PointToLine(value1.Min, value1.Max, value2.Max);

            return MinLength(MinLength(d1, d2), MinLength(d3, d4));
        }

        private static Vector2 MinLength(Vector2 value1, Vector2 value2)
        {
            return value2.LengthSquared() < value1.LengthSquared() ? value2 : value1;
        }

        private static Vector2 PointToLine(Vector2 a, Vector2 b, Vector2 p)
        {
            Vector2 a1 = p - a;
            Vector2 a2 = b - a;

            if (Vector2.Dot(a1, a2) <= 0) return a1;

            Vector2 b1 = p - b;
            Vector2 b2 = a - b;

            if (Vector2.Dot(b1, b2) <= 0) return b1;

            Vector2 p1 = MathUtil.Projection(a1, a2);
            Vector2 p2 = a1 - p1;

            return p2;
        }

        public static DistanceInfo DistanceInfo(BoundingCapsule2 value1, BoundingCapsule2 value2)
        {
            float l1, l2, l3, l4;

            Vector2 d1 = -PointToLine(value2.Min, value2.Max, value1.Min, out l1);
            Vector2 d2 = -PointToLine(value2.Min, value2.Max, value1.Max, out l2);
            Vector2 d3 = PointToLine(value1.Min, value1.Max, value2.Min, out l3);
            Vector2 d4 = PointToLine(value1.Min, value1.Max, value2.Max, out l4);

            DistanceInfo di1 = new DistanceInfo(d1, 0, l1);
            DistanceInfo di2 = new DistanceInfo(d2, 1, l2);
            DistanceInfo di3 = new DistanceInfo(d3, l3, 0);
            DistanceInfo di4 = new DistanceInfo(d4, l4, 1);

            return MinLength(MinLength(di1, di2), MinLength(di3, di4));
        }

        private static DistanceInfo MinLength(DistanceInfo value1, DistanceInfo value2)
        {
            return value2.Distance.LengthSquared() < value1.Distance.LengthSquared() ? value2 : value1;
        }

        private static Vector2 PointToLine(Vector2 a, Vector2 b, Vector2 p, out float l)
        {
            Vector2 a1 = p - a;
            Vector2 a2 = b - a;

            if (Vector2.Dot(a1, a2) <= 0) { l = 0; return a1; }

            Vector2 b1 = p - b;
            Vector2 b2 = a - b;

            if (Vector2.Dot(b1, b2) <= 0) { l = 1; return b1; }

            Vector2 p1 = MathUtil.Projection(a1, a2);
            Vector2 p2 = a1 - p1;

            if (a2.X * a2.X > a2.Y * a2.Y)
            {
                l = p1.X > 0 ? p1.X / a2.X : 0;
            }
            else
            {
                l = p1.Y > 0 ? p1.Y / a2.Y : 0;
            }

            return p2;
        }
    }

    public struct DistanceInfo
    {
        public Vector2 Distance;
        public float Location1;
        public float Location2;

        public DistanceInfo(Vector2 distance, float location1, float location2)
        {
            Distance = distance;
            Location1 = location1;
            Location2 = location2;
        }
    }
}
