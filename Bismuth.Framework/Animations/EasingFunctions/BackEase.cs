using System;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework.Animations.EasingFunctions
{
    public class BackEase : EasingFunctionBase
    {
        public float Amplitude { get { return _amplitude; } set { _amplitude = value; } }
        private float _amplitude = 2.0f;

        protected override float EaseIn(float normalizedTime)
        {
            float a = Math.Max(0.0f, _amplitude);
            return (float)Math.Pow(normalizedTime, 3.0f) - (a * normalizedTime * (float)Math.Sin(MathHelper.Pi * normalizedTime));
        }
    }
}
