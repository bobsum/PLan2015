using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bismuth.Framework.Physics.VerletIntegration.Constraints;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework.Physics.VerletIntegration
{
    public class VerletSimulator
    {
        public VerletSimulator(int iterations)
        {
            Iterations = iterations;
        }

        public int Iterations { get; set; }

        public List<Verlet> Verlets { get { return _verlets; } }
        private readonly List<Verlet> _verlets = new List<Verlet>();

        public List<IConstraint> Constraints { get { return _constraints; } }
        private readonly List<IConstraint> _constraints = new List<IConstraint>();

        public void Update(GameTime gameTime)
        {
            float inverseIterations = 1.0f / (float)Iterations;

            for (int i = 0; i < Iterations; i++)
            {
                for (int j = 0; j < _constraints.Count; j++)
                {
                    if (_constraints[j].IsEnabled)
                        _constraints[j].Resolve(inverseIterations);
                }
            }
        }
    }
}
