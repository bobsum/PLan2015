namespace Bismuth.Framework.Physics.VerletIntegration.Constraints
{
    public interface IConstraint
    {
        bool IsEnabled { get; set; }
        void Resolve(float inverseIterations);
    }
}
