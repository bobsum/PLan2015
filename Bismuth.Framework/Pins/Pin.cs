using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bismuth.Framework.Composite;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework.Pins
{
    public class Pin : Node, IPin
    {
        public string TargetName { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsRotationPinned { get; set; }
        public PinBendDirection BendDirection { get; set; }

        private INode _target;
        private INode _upper;
        private INode _lower;

        private float _upperLength, _upperRotationOffset;
        private float _lowerLength, _lowerRotationOffset;

        public void Bind(INode target)
        {
            if (target != null && target.Parent != null && target.Parent.Parent != null)
            {
                _target = target;
                _upper = target.Parent.Parent;
                _lower = target.Parent;

                _upperLength = _lower.Position.Length();
                _upperRotationOffset = (float)Math.Atan2(_lower.Position.Y, _lower.Position.X);

                _lowerLength = _target.Position.Length();
                _lowerRotationOffset = (float)Math.Atan2(_target.Position.Y, _target.Position.X);
            }
        }

        public void UpdateTarget()
        {
            if (IsEnabled && _target != null)
            {
                ComputeAngles();
            }
        }

        private void ComputeAngles()
        {
            // A
            // | \
            // |  C
            // | /
            // B

            Vector2 pA = _upper.Position;
            Vector2 pB = _upper.Parent != null ? GetRelativePosition(_upper.Parent) : WorldPosition;

            Vector2 d = pB - pA;
            float offsetA = (float)Math.Atan2(d.Y, d.X);

            float c = Vector2.Distance(pA, pB);
            float b = _upperLength;
            float a = _lowerLength;

            float A = 0;
            float C = MathHelper.Pi;

            if (c < a + b)
            {
                A = GetAngleA(a, b, c);
                C = GetAngleA(c, a, b);
            }

            if (BendDirection == PinBendDirection.Right)
            {
                A = offsetA - A;
                C = MathHelper.Pi - C;
            }
            else
            {
                A = offsetA + A;
                C = C - MathHelper.Pi;
            }

            _upper.Rotation = A - _upperRotationOffset;
            _lower.Rotation = C + _upperRotationOffset - _lowerRotationOffset;

            if (IsRotationPinned)
                _target.Rotation = WorldRotation - _lower.WorldRotation;
        }

        private float GetAngleA(float a, float b, float c)
        {
            return (float)Math.Acos((b * b + c * c - a * a) / (2 * b * c));
        }

        private Vector2 GetRelativePosition(INode relativeTo)
        {
            return Vector2.Transform(WorldPosition, Matrix.Invert(relativeTo.WorldTransform));
        }

        public override INode Clone()
        {
            INode node = new Pin();
            CopyTo(node);
            return node;
        }

        protected override void CopyTo(INode node)
        {
            base.CopyTo(node);

            Pin pin = (Pin)node;
            pin.TargetName = TargetName;
            pin.IsEnabled = IsEnabled;
            pin.IsRotationPinned = IsRotationPinned;
            pin.BendDirection = BendDirection;
        }
    }
}
