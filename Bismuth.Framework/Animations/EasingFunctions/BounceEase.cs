using System;

namespace Bismuth.Framework.Animations.EasingFunctions
{
    public class BounceEase : EasingFunctionBase
    {
        public int Bounces { get { return _bounces; } set { _bounces = value; } }
        private int _bounces = 3;

        public float Bounciness { get { return _bounciness; } set { _bounciness = value; } }
        private float _bounciness = 2.0f;

        protected override float EaseIn(float normalizedTime)
        {
            float y = Math.Max(0.0f, _bounces);
            float bounciness = _bounciness;
            if (bounciness < 1.0f || IsOne(bounciness))
            {
                bounciness = 1.001f;
            }

            float a = (float)Math.Pow(bounciness, y);
            float b = 1.0f - bounciness;
            float c = ((1.0f - a) / b) + (a * 0.5f);
            float d = normalizedTime * c;
            float e = (float)Math.Floor(Math.Log((-d * (1.0f - bounciness)) + 1.0f, bounciness));
            float f = e + 1.0f;
            float g = (1.0f - (float)Math.Pow(bounciness, e)) / (b * c);
            float h = (1.0f - (float)Math.Pow(bounciness, f)) / (b * c);
            float i = (g + h) * 0.5f;
            float j = normalizedTime - i;
            float k = i - g;
            return ((-(float)Math.Pow(1.0f / bounciness, y - e) / (k * k)) * (j - k)) * (j + k);
        }

        private bool IsOne(float value)
        {
            //return Math.Abs(value - 1.0f) < 2.2204460492503131E-15;
            return Math.Abs(value - 1.0f) < float.Epsilon;
        }
    }
}
