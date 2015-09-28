using Microsoft.Xna.Framework;

namespace Bismuth.Framework.Physics.VerletIntegration.Constraints
{
    public enum InfluenceMode
    {
        TwoWay,
        OneWayToA,
        OneWayToB
    }

    public class DistanceConstraint : IConstraint
    {
        public Verlet A { get; set; }
        public Verlet B { get; set; }

        public float Factor = 1.0f;

        /// <summary>
        /// If false, the joint will not be updated in the physics simulator.
        /// </summary>
        public bool IsEnabled { get { return _isEnabled; } set { _isEnabled = value; } }
        private bool _isEnabled = true;

        /// <summary>
        /// The max distance the two physics objects can be from eachother.
        /// </summary>
        public float MaxDistance { get { return _maxDistance; } set { _maxDistance = value; } }
        private float _maxDistance = 1.0f;

        public float MinDistance { get { return _minDistance; } set { _minDistance = value; } }
        private float _minDistance = 0.0f;

        /// <summary>
        /// If true, the link can not rotate.
        /// </summary>
        public bool IsStatic { get { return _isStatic; } set { _isStatic = value; } }
        private bool _isStatic = false;

        /// <summary>
        /// The way object A and B have an effect on each other.
        /// </summary>
        public InfluenceMode Influence { get { return _influence; } set { _influence = value; } }
        private InfluenceMode _influence = InfluenceMode.TwoWay;

        public DistanceConstraint() { }
        public DistanceConstraint(Verlet a, Verlet b, float distance)
        {
            A = a;
            B = b;
            MaxDistance = distance;
            MinDistance = distance;
        }

        public void Resolve(float inverseIterations)
        {
            Vector2 delta = B.Position - A.Position;
            float restLength = 0;

            if (delta.LengthSquared() > MaxDistance * MaxDistance)
            {
                restLength = MaxDistance;
            }
            else if (delta.LengthSquared() < MinDistance * MinDistance)
            {
                restLength = MinDistance;
            }

            if (restLength > 0)
            {
                float deltaLength = delta.Length();
                float totalInverseMass = A.InverseMass + B.InverseMass;
                float diff = (deltaLength - restLength) / (deltaLength * totalInverseMass);

                if (Influence == InfluenceMode.TwoWay)
                {
                    A.Position += A.InverseMass * delta * diff;
                    B.Position -= B.InverseMass * delta * diff;
                }
                else if (Influence == InfluenceMode.OneWayToA)
                {
                    A.Position += totalInverseMass * delta * diff;
                }
                else if (Influence == InfluenceMode.OneWayToB)
                {
                    B.Position -= totalInverseMass * delta * diff;
                }
            }
        }

        public void Resolve_(float inverseIterations)
        {
            Vector2 delta = B.Position - A.Position;
            float delta2 = delta.LengthSquared();
            float restLength = 0;

            if (delta2 > MaxDistance * MaxDistance)
            {
                restLength = MaxDistance;
            }
            else if (delta2 < MinDistance * MinDistance)
            {
                restLength = MinDistance;
            }

            if (restLength > 0)
            {
                float totalInverseMass = A.InverseMass + B.InverseMass;
                float restLength2 = restLength * restLength;
                float diff = restLength2 / (delta2 + restLength2) - 0.5f;
                diff *= -2.0f;
                delta *= diff;

                //delta *= (1.0f / (float)iterations) * 0.9f;

                A.Position += delta * (A.InverseMass / totalInverseMass);
                B.Position -= delta * (B.InverseMass / totalInverseMass);

                //Vector2 normal = link.A.Position - link.B.Position;
                //float m = normal.LengthSquared();
                //float restLength2 = restLength * restLength;
                //float diff = ((restLength2 - m) / m) * (1.0f / (float)LinkIterations) * 1.0f;
                //normal *= diff;

                //link.A.Position += normal;
                //link.B.Position -= normal;
            }
        }
    }
}
