using System;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework
{
    /// <summary>
    /// Spherical coordinate in 3D space.
    /// </summary>
    public struct Polar3 : IEquatable<Polar3>
    {
        /// <summary>
        /// Radius.
        /// </summary>
        public float R;

        /// <summary>
        /// Angle around the up axis.
        /// </summary>
        public float Theta;

        /// <summary>
        /// Angle in the right, forward plane.
        /// </summary>
        public float Phi;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="r">Radius.</param>
        /// <param name="theta">Angle around the up axis.</param>
        /// <param name="phi">Angle in the right, forward plane.</param>
        public Polar3(float r, float theta, float phi)
        {
            R = r;
            Theta = theta;
            Phi = phi;
        }

        public static bool operator ==(Polar3 value1, Polar3 value2)
        {
            return value1.Equals(value2);
        }

        public static bool operator !=(Polar3 value1, Polar3 value2)
        {
            return !value1.Equals(value2);
        }

        public bool Equals(Polar3 other)
        {
            return R == other.R && Theta == other.Theta && Phi == other.Phi;
        }

        public override bool Equals(object obj)
        {
            return base.Equals((Polar3)obj);
        }

        public override int GetHashCode()
        {
            return R.GetHashCode() + Theta.GetHashCode() + Phi.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{{R:{0} Theta:{1} Phi:{2}}}", R, Theta, Phi);
        }

        public Vector3 ToVector3()
        {
            Vector3 vector = new Vector3();
            vector.X = R * (float)Math.Sin(Theta) * (float)Math.Cos(Phi);
            vector.Y = R * (float)Math.Sin(Theta) * (float)Math.Sin(Phi);
            vector.Z = R * (float)Math.Cos(Theta);
            return vector;
        }

        /// <summary>
        /// Create a polar coordinate from a cartesian vector.
        /// </summary>
        public static Polar3 CreateFromVector(Vector3 vector)
        {
            float length = vector.Length();

            // DirectX defines up as (0,1,0), so we need to move some coords around.
            // Theta becomes the vertical angle against the Y axis.
            // Phi becomes the horizontal angle in the XZ plane.
            return new Polar3(length,
                (float)Math.Acos(vector.Y / length),
                (float)Math.Atan2(vector.X, vector.Z));
        }

        /// <summary>
        /// Latitude in radians.
        /// </summary>
        public float Latitude { get { return MathHelper.PiOver2 - Theta; } }

        /// <summary>
        /// Longitude in radians.
        /// </summary>
        public float Longitude { get { return Phi + MathHelper.Pi; } }

        /// <summary>
        /// Returns the xy miller projection.
        /// </summary>
        public Vector2 ProjectMiller()
        {
            Vector2 v = new Vector2();

            v.X = Longitude;
            v.Y = Latitude * 2 / 5;
            v.Y = v.Y + MathHelper.PiOver4;
            v.Y = (float)Math.Log(Math.Tan(v.Y));
            v.Y = v.Y * 1.25f;

            return v;
        }
    }
}
