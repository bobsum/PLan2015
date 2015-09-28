using Microsoft.Xna.Framework;

namespace Bismuth.Framework.Physics.VerletIntegration.Constraints
{
    public class FixedConstraint : IConstraint
    {
        public Verlet Target { get; set; }
        public Vector2 Position { get; set; }

        public FixedConstraint() { }
        public FixedConstraint(Verlet target) { Target = target; }

        /// <summary>
        /// If false, the joint will not be updated in the physics simulator.
        /// </summary>
        public bool IsEnabled { get { return _isEnabled; } set { _isEnabled = value; } }
        private bool _isEnabled = true;

        public void Resolve(float inverseIterations)
        {
            Target.Position = Position;
        }
    }
}
