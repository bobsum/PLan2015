using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework.Physics.VerletIntegration
{
    public class Verlet
    {
        public Vector2 Position;
        public Vector2 PreviousPosition;
        public float Mass
        {
            get { return _mass; }
            set { _mass = value; _inverseMass = 1.0f / value; }
        }
        private float _mass = 1;

        public float InverseMass { get { return _inverseMass; } }
        private float _inverseMass = 1;
    }
}
