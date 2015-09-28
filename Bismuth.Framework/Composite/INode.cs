using Microsoft.Xna.Framework;

namespace Bismuth.Framework.Composite
{
    public interface INode
    {
        string Name { get; set; }

        INode Parent { get; set; }
        NodeList Children { get; }

        Matrix Transform { get; }
        Vector2 Position { get; set; }
        float Rotation { get; set; }
        float Scale { get; set; }
        float ScaleX { get; set; }
        float ScaleY { get; set; }
        float SkewX { get; set; }
        float SkewY { get; set; }
        bool FlipX { get; set; }
        bool FlipY { get; set; }
        float Opacity { get; set; }
        bool IsVisible { get; set; }
        int ZIndex { get; set; }

        Matrix WorldTransform { get; }
        Vector2 WorldPosition { get; }
        float WorldRotation { get; }
        float WorldScale { get; }
        float WorldScaleX { get; }
        float WorldScaleY { get; }
        bool WorldFlipX { get; }
        bool WorldFlipY { get; }
        float WorldOpacity { get; }

        void MakeDirty();
        void ResolveDirty();

        void Update(GameTime gameTime);
        bool HitTest(Vector2 position);

        BoundingBox2 GetBounds();
        BoundingBox2 GetWorldBounds();

        INode Clone();
    }
}
