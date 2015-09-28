using System;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework
{
    /// <summary>
    /// Defines an axis-aligned bounding-box in 2D.
    /// It is similar to the Microsoft.Xna.Framework.BoundingBox,
    /// but uses Vector2 instead of Vector3.
    /// </summary>
    public struct BoundingBox2
    {
        public Vector2 Min;
        public Vector2 Max;

        public BoundingBox2(Vector2 min, Vector2 max)
        {
            Min = min;
            Max = max;
        }

        public BoundingBox2(float min, float max)
        {
            Min = new Vector2(min);
            Max = new Vector2(max);
        }

        public BoundingBox2(float minX, float minY, float maxX, float maxY)
        {
            Min = new Vector2(minX, minY);
            Max = new Vector2(maxX, maxY);
        }

        public BoundingBox2(Rectangle rectangle)
        {
            Min = new Vector2(rectangle.X, rectangle.Y);
            Max = new Vector2(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);
        }

        public static BoundingBox2 operator +(BoundingBox2 value1, Vector2 value2)
        {
            return new BoundingBox2(value1.Min + value2, value1.Max + value2);
        }

        public static BoundingBox2 operator -(BoundingBox2 value1, Vector2 value2)
        {
            return new BoundingBox2(value1.Min - value2, value1.Max - value2);
        }

        public static BoundingBox2 operator +(Vector2 value1, BoundingBox2 value2)
        {
            return new BoundingBox2(value1 + value2.Min, value1 + value2.Max);
        }

        public static BoundingBox2 operator -(Vector2 value1, BoundingBox2 value2)
        {
            return new BoundingBox2(value1 - value2.Min, value1 - value2.Max);
        }

        public bool Contains(Vector2 value)
        {
            return value.Y > Min.Y && value.Y < Max.Y && value.X > Min.X && value.X < Max.X;
        }

        public bool Intersects(BoundingBox2 value)
        {
            return value.Max.Y > Min.Y && value.Min.Y < Max.Y && value.Max.X > Min.X && value.Min.X < Max.X;
        }

        public void Intersects(ref BoundingBox2 value, out bool result)
        {
            result = value.Max.Y > Min.Y && value.Min.Y < Max.Y && value.Max.X > Min.X && value.Min.X < Max.X;
        }

        public static bool Intersects(BoundingBox2 value1, BoundingBox2 value2)
        {
            return value1.Max.Y > value2.Min.Y && value1.Min.Y < value2.Max.Y && value1.Max.X > value2.Min.X && value1.Min.X < value2.Max.X;
        }

        public void Extend(Vector2 value)
        {
            if (value.X < Min.X) Min.X = value.X;
            if (value.Y < Min.Y) Min.Y = value.Y;
            if (value.X > Max.X) Max.X = value.X;
            if (value.Y > Max.Y) Max.Y = value.Y;
        }

        public void Extend(Vector2[] values)
        {
            for (int i = 0; i < values.Length; i++)
                Extend(values[i]);
        }

        public BoundingBox2 Union(BoundingBox2 value)
        {
            if (Min.X < value.Min.X) value.Min.X = Min.X;
            if (Max.X > value.Max.X) value.Max.X = Max.X;
            if (Min.Y < value.Min.Y) value.Min.Y = Min.Y;
            if (Max.Y > value.Max.Y) value.Max.Y = Max.Y;

            return value;
        }

        public BoundingBox2 Intersection(BoundingBox2 value)
        {
            if (Min.X > value.Min.X) value.Min.X = Min.X;
            if (Max.X < value.Max.X) value.Max.X = Max.X;
            if (Min.Y > value.Min.Y) value.Min.Y = Min.Y;
            if (Max.Y < value.Max.Y) value.Max.Y = Max.Y;

            return value;
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle((int)Min.X, (int)Min.Y, (int)Max.X - (int)Min.X, (int)Max.Y - (int)Min.Y);
            //return new Rectangle((int)Min.X, (int)Min.Y, (int)(Max.X - Min.X), (int)(Max.Y - Min.Y));
        }

        public override string ToString()
        {
            return string.Format("{{Min:{0} Max:{1}}}", Min, Max);
        }

        public Vector2 Center()
        {
            return (Min + Max) * 0.5f;
        }

        public void GetCorners(Vector2[] corners)
        {
            if (corners.Length < 4) throw new ArgumentOutOfRangeException("corners", "Must be at least 4 corners.");

            corners[0] = Min;
            corners[1] = new Vector2(Max.X, Min.Y);
            corners[2] = Max;
            corners[3] = new Vector2(Min.X, Max.Y);
        }

        public Vector2[] GetCorners()
        {
            Vector2[] corners = new Vector2[4];
            GetCorners(corners);
            return corners;
        }

        public static Vector2[] Transform(BoundingBox2 value, Matrix matrix)
        {
            Vector2[] corners = new Vector2[4];
            Transform(value, matrix, corners);
            return corners;
        }

        public static void Transform(BoundingBox2 value, Matrix matrix, Vector2[] corners)
        {
            value.GetCorners(corners);

            for (int i = 0; i < 4; i++)
                corners[i] = Vector2.Transform(corners[i], matrix);
        }
    }
}
