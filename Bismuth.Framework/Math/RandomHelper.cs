using System;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework
{
    public static class RandomHelper
    {
        private static Random _random = new Random();

        public static void Seed()
        {
            _random = new Random();
        }

        public static void Seed(int value)
        {
            _random = new Random(value);
        }

        public static int Next(int min, int max)
        {
            return _random.Next(min, max);
        }

        public static float Next(float min, float max)
        {
            return (float)_random.NextDouble() * (max - min) + min;
        }

        public static double Next(double min, double max)
        {
            return _random.NextDouble() * (max - min) + min;
        }

        public static Vector2 Next(Vector2 min, Vector2 max)
        {
            return new Vector2(Next(min.X, max.X), Next(min.Y, max.Y));
        }
    }

    public struct RandomInt
    {
        public int Min, Max;

        public RandomInt(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public RandomInt(int value)
        {
            Min = value;
            Max = value;
        }

        public int GetValue()
        {
            return RandomHelper.Next(Min, Max);
        }
    }

    public struct RandomFloat
    {
        public float Min, Max;

        public RandomFloat(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public RandomFloat(float value)
        {
            Min = value;
            Max = value;
        }

        public float GetValue()
        {
            return RandomHelper.Next(Min, Max);
        }
    }

    public struct RandomDouble
    {
        public double Min, Max;

        public RandomDouble(double min, double max)
        {
            Min = min;
            Max = max;
        }

        public RandomDouble(double value)
        {
            Min = value;
            Max = value;
        }

        public double GetValue()
        {
            return RandomHelper.Next(Min, Max);
        }
    }

    public struct RandomVector2
    {
        public Vector2 Min, Max;

        public RandomVector2(Vector2 min, Vector2 max)
        {
            Min = min;
            Max = max;
        }

        public RandomVector2(Vector2 value)
        {
            Min = value;
            Max = value;
        }

        public Vector2 GetValue()
        {
            return RandomHelper.Next(Min, Max);
        }
    }
}
