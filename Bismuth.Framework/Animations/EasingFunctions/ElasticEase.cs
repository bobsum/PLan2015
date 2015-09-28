using System;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework.Animations.EasingFunctions
{
    public class ElasticEase : EasingFunctionBase
    {
        public float Oscillations { get { return _oscillations; } set { _oscillations = value; } }
        private float _oscillations = 3.0f;

        public float Springiness { get { return _springiness; } set { _springiness = value; } }
        private float _springiness = 5.0f;

        protected override float EaseIn(float normalizedTime)
        {
            float n = normalizedTime;
            float p = Math.Max(0.0f, _oscillations);
            float s = Math.Max(0.0f, _springiness);

            if (s != 0)
            {
                n = ((float)Math.Exp(s * normalizedTime) - 1.0f) / ((float)Math.Exp(s) - 1.0f);
            }

            return n * (float)Math.Sin((MathHelper.TwoPi * p + MathHelper.PiOver2) * normalizedTime);
        }
    }
}
