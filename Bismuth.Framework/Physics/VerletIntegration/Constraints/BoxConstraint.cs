using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bismuth.Framework.Physics.VerletIntegration.Constraints
{
    public class BoxConstraint : IConstraint
    {
        public Verlet Target { get; set; }
        public BoundingBox2 Bounds { get; set; }

        public BoxConstraint() { }
        public BoxConstraint(Verlet target) { Target = target; }
        public BoxConstraint(Verlet target, BoundingBox2 bounds) { Target = target; Bounds = bounds; }

        public bool IsEnabled { get { return _isEnabled; } set { _isEnabled = value; } }
        private bool _isEnabled = true;

        public void Resolve(float inverseIterations)
        {
            if (Target.Position.X < Bounds.Min.X) Target.Position.X = Bounds.Min.X;
            if (Target.Position.X > Bounds.Max.X) Target.Position.X = Bounds.Max.X;
            if (Target.Position.Y < Bounds.Min.Y) Target.Position.Y = Bounds.Min.Y;
            if (Target.Position.Y > Bounds.Max.Y) Target.Position.Y = Bounds.Max.Y;
        }
    }
}
