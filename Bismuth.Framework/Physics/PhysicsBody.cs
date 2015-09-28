using System;
using Bismuth.Framework.Physics.Shapes;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework.Physics
{
    /// <summary>
    /// The Body class handles all physics of motion:
    /// position, velocity, rotation, angular rotation, force, angular force, etc.
    /// </summary>
    public class PhysicsBody
    {
        public object Owner { get; private set; }

        public PhysicsBody() { }
        public PhysicsBody(object owner) { Owner = owner; }
        public PhysicsBody(IShape shape) { Shape = shape; }

        public void ComputeMomentOfInertia()
        {
            if (Shape != null)
            {
                MomentOfInertia = Shape.ComputeMomentOfInertia(Mass);
            }
            else
            {
                MomentOfInertia = 1.0f;
            }
        }

        public IShape Shape { get { return _shape; } set { _shape = value; } }
        private IShape _shape;

        /// <summary>
        /// True if the body should ignore the gravisy forces of the physics simulator.
        /// </summary>
        public bool IgnoreGravity { get { return _ignoreGravity; } set { _ignoreGravity = value; } }
        private bool _ignoreGravity = false;

        /// <summary>
        /// If false, the body will not be updated in the physics simulator.
        /// </summary>
        public bool IsEnabled { get { return _isEnabled; } set { _isEnabled = value; } }
        private bool _isEnabled = true;

        /// <summary>
        /// The bounding box of the body, used to do fast collision detection.
        /// NOTE: Only used by the TileMapFramework.
        /// </summary>
        public BoundingBox2 BoundingBox;

        /// <summary>
        /// The position of the body.
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// The velocity of the body.
        /// </summary>
        public Vector2 Velocity;

        /// <summary>
        /// The force that will be applied in the physics simulator update loop.
        /// </summary>
        public Vector2 Force;

        /// <summary>
        /// Makes the body gradually stop moving.
        /// </summary>
        public Vector2 Damping = Vector2.One;

        /// <summary>
        /// Gets or sets the rotation of the body.
        /// </summary>
        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = MathHelper.WrapAngle(value); }
            //set
            //{
            //    while (_rotation < 0) _rotation += MathHelper.TwoPi;
            //    while (_rotation >= MathHelper.TwoPi) _rotation -= MathHelper.TwoPi;
            //}
        }
        private float _rotation;

        /// <summary>
        /// Gets or sets the velocety of the rotation of the body.
        /// </summary>
        public float AngularVelocity { get { return _angularVelocity; } set { _angularVelocity = value; } }
        private float _angularVelocity;

        /// <summary>
        /// Gets or sets the force of the rotation of the body.
        /// </summary>
        public float AngularForce { get { return _angularForce; } set { _angularForce = value; } }
        private float _angularForce;

        /// <summary>
        /// Gets or sets the moment of inertia of the body.
        /// </summary>
        public float MomentOfInertia
        {
            get { return _momentOfInertia; }
            set
            {
                _momentOfInertia = value;
                _inverseMomentOfInertia = 1.0f / value;
            }
        }
        private float _momentOfInertia = 1.0f;

        /// <summary>
        /// Gets the inverse moment of inertia of the body.
        /// </summary>
        public float InverseMomentOfInertia { get { return _inverseMomentOfInertia; } }
        private float _inverseMomentOfInertia = 1.0f;

        /// <summary>
        /// Gets or sets the mass of the body.
        /// </summary>
        public float Mass
        {
            get { return _mass; }
            set
            {
                _mass = value;
                _inverseMass = 1.0f / value;
            }
        }
        private float _mass = 1.0f;

        /// <summary>
        /// Gets the inverse mass of the body, whics is calculated when the mass is set.
        /// </summary>
        public float InverseMass { get { return _inverseMass; } }
        private float _inverseMass = 1.0f;

        /// <summary>
        /// Gets or sets the elasticity of the body.
        /// </summary>
        public float Elasticity { get { return _elasticity; } set { _elasticity = value; } }
        private float _elasticity = 0.8f;

        /// <summary>
        /// Gets or sets the elasticity of the body.
        /// </summary>
        public float Friction { get { return _friction; } set { _friction = value; } }
        private float _friction = 1.0f;

        /// <summary>
        /// True if the body should always stice to the same position.
        /// Collision with other bodies, and gravity will not move this body.
        /// </summary>
        public bool IsFixed { get { return _isFixed; } set { _isFixed = value; } }
        private bool _isFixed = false;

        /// <summary>
        /// The old position of the body.
        /// </summary>
        public Vector2 PreviousPosition;
        public float PreviousRotation;
        public Vector2 PreviousVelocity;

        public void ApplyForce(float amount)
        {
            Force.X += (float)Math.Cos(Rotation) * amount;
            Force.Y += (float)Math.Sin(Rotation) * amount;
        }

        public void ApplyRotationForce(float amount)
        {
            AngularForce += amount;
        }

        public void ApplyForce(Vector2 amount)
        {
            Force += amount;
        }

        public void ApplyForce(Vector2 point, Vector2 amount)
        {
            float torque = MathUtil.Cross(point, amount);

            Force += amount;
            AngularForce += torque;

            // System.Diagnostics.Debug.WriteLine("Point: " + point.ToString() + " Amount: " + amount.ToString() + " Torque: " + torque);
        }

        public void ApplyForceAtLocalPoint(Vector2 point, Vector2 amount)
        {
            Matrix rotationMatrix = Matrix.CreateRotationZ(Rotation);

            Vector2 localPoint = Vector2.Transform(point, rotationMatrix);
            Vector2 localAmount = Vector2.Transform(amount, rotationMatrix);

            ApplyForce(localPoint, localAmount);
        }

        public void ApplyForceAtWorldPoint(Vector2 point, Vector2 amount)
        {
            Vector2 localPoint = point - Position;

            ApplyForce(localPoint, amount);
        }
    }
}
