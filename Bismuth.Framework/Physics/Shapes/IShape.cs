using Bismuth.Framework;

namespace Bismuth.Framework.Physics.Shapes
{
    public interface IShape
    {
        BoundingBox2 BoundingBox { get; }
        float ComputeMomentOfInertia(float mass);
    }
}
