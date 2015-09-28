using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework
{
    public struct Line2
    {
        public Vector2 P1;
        public Vector2 P2;

        public Line2(Vector2 p1, Vector2 p2)
        {
            P1 = p1;
            P2 = p2;
        }

        public Vector2 Normal()
        {
            return MathUtil.Skew(Vector2.Normalize(P2 - P1));
        }

        public void Translate(float offset)
        {
            Vector2 normal = Normal();
            P1 += normal * offset;
            P2 += normal * offset;
        }

        public static Line2 Translate(Line2 value, float offset)
        {
            Vector2 normal = value.Normal();
            return new Line2(value.P1 + normal * offset, value.P2 + normal * offset);
        }

        public Vector2 Intersection(Line2 value)
        {
            Vector2 result;
            if (Intersection(ref this, ref value, out result)) return result;
            return P1;
        }

        public bool Intersection(Line2 value, out Vector2 result)
        {
            return Intersection(ref this, ref value, out result);
        }

        public static bool Intersection(Line2 value1, Line2 value2, out Vector2 result)
        {
            return Intersection(ref value1, ref value2, out result);
        }

        public static bool Intersection(ref Line2 value1, ref Line2 value2, out Vector2 result)
        {
            float a1 = value1.P2.Y - value1.P1.Y;
            float b1 = value1.P1.X - value1.P2.X;
            float c1 = a1 * value1.P1.X + b1 * value1.P1.Y;

            float a2 = value2.P2.Y - value2.P1.Y;
            float b2 = value2.P1.X - value2.P2.X;
            float c2 = a2 * value2.P1.X + b2 * value2.P1.Y;

            float det = a1 * b2 - a2 * b1;
            if (det == 0)
            {
                result = Vector2.Zero;
                return false;
            }
            else
            {
                det = 1.0f / det;
                float x = (b2 * c1 - b1 * c2) * det;
                float y = (a1 * c2 - a2 * c1) * det;
                result = new Vector2(x, y);
                return true;
            }
        }
    }
}
