using System;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework
{
    /// <summary>
    /// Polar coordinate in 2D space.
    /// </summary>
    public struct Polar2 : IEquatable<Polar2>
    {
        /// <summary>
        /// Radius.
        /// </summary>
        public float R;

        /// <summary>
        /// Angle.
        /// </summary>
        public float Theta;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="r">Radius.</param>
        /// <param name="theta">Angle.</param>
        public Polar2(float r, float theta)
        {
            R = r;
            Theta = theta;
        }

        public static bool operator ==(Polar2 value1, Polar2 value2)
        {
            return value1.Equals(value2);
        }

        public static bool operator !=(Polar2 value1, Polar2 value2)
        {
            return !value1.Equals(value2);
        }

        public bool Equals(Polar2 other)
        {
            return R == other.R && Theta == other.Theta;
        }

        public override bool Equals(object obj)
        {
            return base.Equals((Polar2)obj);
        }

        public override int GetHashCode()
        {
            return R.GetHashCode() + Theta.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{{R:{0} Theta:{1}}}", R, Theta);
        }

        public Vector2 ToVector2()
        {
            Vector2 vector = new Vector2();
            vector.X = R * (float)Math.Cos(Theta);
            vector.Y = R * (float)Math.Sin(Theta);
            return vector;
        }

        /// <summary>
        /// Create a polar coordinate from a cartesian vector.
        /// </summary>
        public static Polar2 CreateFromVector(Vector2 vector)
        {
            return new Polar2(vector.Length(), (float)Math.Atan2(vector.Y, vector.X));
        }
    }
}
