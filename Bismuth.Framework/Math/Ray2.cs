using System;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework
{
    public struct Ray2 : IEquatable<Ray2>
    {
        public Vector2 Position;
        public Vector2 Direction;

        public Ray2(Vector2 position, Vector2 direction)
        {
            Position = position;
            Direction = direction;
        }

        public static bool operator ==(Ray2 a, Ray2 b)
        {
            return
                a.Position.X == b.Position.X &&
                a.Position.Y == b.Position.Y &&
                a.Direction.X == b.Direction.X &&
                a.Direction.Y == b.Direction.Y;
        }

        public static bool operator !=(Ray2 a, Ray2 b)
        {
            return
                a.Position.X != b.Position.X ||
                a.Position.Y != b.Position.Y ||
                a.Direction.X != b.Direction.X ||
                a.Direction.Y != b.Direction.Y;
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is Ray2) return Equals((Ray2)obj);
            return false;
        }

        public bool Equals(Ray2 other)
        {
            return
                this.Position.X == other.Position.X &&
                this.Position.Y == other.Position.Y &&
                this.Direction.X == other.Direction.X &&
                this.Direction.Y == other.Direction.Y;
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode() + Direction.GetHashCode();
        }

        public float? Intersects(BoundingBox2 box)
        {
            throw new NotImplementedException();
        }

        public float? Intersects(BoundingCircle circle)
        {
            float? result;
            Intersects(ref circle, out result);
            return result;
        }

        public float? Intersects(Line2 line)
        {
            throw new NotImplementedException();
        }

        public void Intersects(ref BoundingBox2 box, out float? result)
        {
            throw new NotImplementedException();
        }

        public void Intersects(ref BoundingCircle circle, out float? result)
        {
            Vector2 d = Direction;
            Vector2 f = Position - circle.Center;

            float a = Vector2.Dot(d, d);
            float b = Vector2.Dot(f, d) * 2;
            float c = Vector2.Dot(f, f) - circle.Radius * circle.Radius;

            float discriminant = b * b - 4 * a * c;
            if (discriminant < 0)
            {
                // No intersection.
                result = null;
            }
            else
            {
                // Ray didn't totally miss circle,
                // so there is a solution to
                // the equation.

                discriminant = (float)Math.Sqrt(discriminant);

                result = (-b - discriminant) / (2 * a);
            }
        }

        public void Intersects(ref Line2 line, out float? result)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return string.Format("{{Position:{0} Direction:{1}}}", Position, Direction);
        }
    }
}
