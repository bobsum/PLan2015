using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework.Physics
{
    /// <summary>
    /// The PhysicsSimulator class keeps track of all bodies, links, etc.
    /// and simulates movement and collision.
    /// </summary>
    public class PhysicsSimulator : IPhysicsSimulator
    {
        public Vector2 Gravity = Vector2.Zero;
        public List<PhysicsBody> Bodies { get { return _bodies; } }
        private List<PhysicsBody> _bodies = new List<PhysicsBody>();

        public PhysicsSimulator() { }
        public PhysicsSimulator(Vector2 gravity)
        {
            Gravity = gravity;
        }

        public void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            float inverseElapsedTime = 1.0f / elapsedTime;

            for (int i = 0; i < _bodies.Count; i++)
            {
                PhysicsBody body = _bodies[i];

                if (!body.IsEnabled || body.IsFixed) continue;

                // TODO: Possible optimazition. Don't check collision if moved distance is smaller than x.
                body.PreviousPosition = body.Position;
                body.PreviousRotation = body.Rotation;

                // Apply friction.
                //physicsObject.Velocity *= (float)Math.Pow(body.Friction, elapsedTime);
                //physicsObject.AngularVelocity *= (float)Math.Pow(body.Friction, elapsedTime);

                // Apply damping.
                //body.Velocity.X *= (float)Math.Pow(body.Damping.X, elapsedTime);
                //body.Velocity.Y *= (float)Math.Pow(body.Damping.Y, elapsedTime);
                //body.Damping = Vector2.One;

                // Apply gravity.
                if (!body.IgnoreGravity)
                    body.Force += body.Mass * Gravity;

                // Apply forces.
                body.Velocity += body.Force * body.InverseMass * elapsedTime;
                body.Force = Vector2.Zero;
                body.AngularVelocity += body.AngularForce * body.InverseMomentOfInertia * elapsedTime;
                body.AngularForce = 0.0f;

                // Apply velocity.
                body.Position += body.Velocity * elapsedTime * 0.001f;
                body.Rotation += body.AngularVelocity * elapsedTime * 0.001f;

                //foreach (ICollisionSpace space in body.CollisionSpaces)
                //{
                //    space.DoCollisions(body);
                //}
            }
        }

        //public void Draw(PrimitiveBatch primitiveBatch)
        //{
        //    PrimitiveBrush b = new PrimitiveBrush
        //    {
        //        FillColor = Color.Transparent,
        //        BorderColor = Color.Red,
        //        BorderThickness = 12
        //    };

        //    foreach (PhysicsBody body in _bodies)
        //        primitiveBatch.DrawCircle(body.Position, 8, b);

        //    b.BorderColor = Color.Turquoise;

        //    foreach (DistanceConstraint link in _constraints)
        //        primitiveBatch.DrawLine(link.A.Position, link.B.Position, b);
        //}
    }
}
