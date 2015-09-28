using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework
{
    public static class MathUtil
    {
        public static Vector2 Abs(Vector2 v)
        {
            return new Vector2(Math.Abs(v.X), Math.Abs(v.Y));
        }

        public static float Cross(Vector2 a, Vector2 b)
        {
            return a.X * b.Y - a.Y * b.X;
        }

        public static float Cross(ref Vector2 a, ref Vector2 b)
        {
            return a.X * b.Y - a.Y * b.X;
        }

        public static Vector2 Skew(Vector2 v)
        {
            return new Vector2(-v.Y, v.X);
        }

        /// <summary>
        /// Projection of 'a' on 'b'.
        /// </summary>
        public static Vector2 Projection(Vector2 a, Vector2 b)
        {
            return (Vector2.Dot(a, b) / b.LengthSquared()) * b;
        }

        /// <summary>
        /// This is a approximate yet fast inverse square-root.
        /// </summary>
        public static float InvSqrt(float x)
        {
            FloatConverter convert = new FloatConverter();
            convert.x = x;
            float xhalf = 0.5f * x;
            convert.i = 0x5f3759df - (convert.i >> 1);
            x = convert.x;
            x = x * (1.5f - xhalf * x * x);
            return x;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct FloatConverter
        {
            [FieldOffset(0)]
            public float x;
            [FieldOffset(0)]
            public int i;
        }
    }
}
