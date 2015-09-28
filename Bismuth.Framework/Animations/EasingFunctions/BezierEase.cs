using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework.Animations.EasingFunctions
{
    public class BezierEase : EasingFunctionBase
    {
        public Vector2 ControlPoint1 { get { return _controlPoint1; } set { _controlPoint1 = value; } }
        private Vector2 _controlPoint1 = Vector2.Zero;
        public Vector2 ControlPoint2 { get { return _controlPoint2; } set { _controlPoint2 = value; } }
        private Vector2 _controlPoint2 = Vector2.One;

        protected override float EaseIn(float normalizedTime)
        {
            float min = 0;
            float max = 1;

            float t = (min + max) * 0.5f;

            Vector2 p = CubicBezier(Vector2.Zero, _controlPoint1, _controlPoint2, Vector2.One, t);

            int counter = 0;
            while (Math.Abs(p.X - normalizedTime) > 0.0001f)
            {
                if (p.X < normalizedTime)
                    min = t;
                else
                    max = t;

                t = (min + max) * 0.5f;
                p = CubicBezier(Vector2.Zero, _controlPoint1, _controlPoint2, Vector2.One, t);

                counter++;
                if (counter > 100) break;
            }

            return p.Y;
        }

        private Vector2 CubicBezier(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
        {
            float a = 1f - t;

            return a * a * a * p0 + 3 * a * a * t * p1 + 3 * a * t * t * p2 + t * t * t * p3;
        }
    }
}
