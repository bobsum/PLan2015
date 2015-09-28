using Microsoft.Xna.Framework;

namespace Bismuth.Framework.Composite
{
    public class Node : INode
    {
        public Node()
        {
            _children = new NodeList(this);
        }

        private string _name;

        private INode _parent;
        private readonly NodeList _children;

        private bool _isDirty;

        private Matrix _transform = Matrix.Identity;
        private Vector2 _position;
        private float _rotation;
        private float _scaleX = 1;
        private float _scaleY = 1;
        private float _skewX;
        private float _skewY;
        private bool _flipX;
        private bool _flipY;
        private float _opacity = 1;
        private bool _isVisible = true;
        private int _zIndex;

        private Matrix _worldTransform = Matrix.Identity;
        private Vector2 _worldPosition;
        private float _worldRotation;
        private float _worldScaleX = 1;
        private float _worldScaleY = 1;
        private bool _worldFlipX;
        private bool _worldFlipY;
        private float _worldOpacity = 1;

        public string Name { get { return _name; } set { _name = value; } }

        public INode Parent { get { return _parent; } set { if (_parent != value) { _parent = value; MakeDirty(); } } }
        public NodeList Children { get { return _children; } }

        public Matrix Transform { get { ResolveDirty(); return _transform; } }
        public Vector2 Position { get { return _position; } set { if (_position != value) { _position = value; MakeDirty(); } } }
        public float Rotation { get { return _rotation; } set { if (_rotation != value) { _rotation = value; MakeDirty(); } } }
        public float Scale { get { return _scaleX; } set { if (_scaleX != value || _scaleY != value) { _scaleX = _scaleY = value; MakeDirty(); } } }
        public float ScaleX { get { return _scaleX; } set { if (_scaleX != value) { _scaleX = value; MakeDirty(); } } }
        public float ScaleY { get { return _scaleY; } set { if (_scaleY != value) { _scaleY = value; MakeDirty(); } } }
        public float SkewX { get { return _skewX; } set { if (_skewX != value) { _skewX = value; MakeDirty(); } } }
        public float SkewY { get { return _skewY; } set { if (_skewY != value) { _skewY = value; MakeDirty(); } } }
        public bool FlipX { get { return _flipX; } set { if (_flipX != value) { _flipX = value; MakeDirty(); } } }
        public bool FlipY { get { return _flipY; } set { if (_flipY != value) { _flipY = value; MakeDirty(); } } }
        public float Opacity { get { return _opacity; } set { if (_opacity != value) { _opacity = value; MakeDirty(); } } }
        public bool IsVisible { get { return _isVisible; } set { _isVisible = value; } }
        public int ZIndex { get { return _zIndex; } set { _zIndex = value; } }

        public Matrix WorldTransform { get { ResolveDirty(); return _worldTransform; } }
        public Vector2 WorldPosition { get { ResolveDirty(); return _worldPosition; } }
        public float WorldRotation { get { ResolveDirty(); return _worldRotation; } }
        public float WorldScale { get { ResolveDirty(); return _worldScaleX; } }
        public float WorldScaleX { get { ResolveDirty(); return _worldScaleX; } }
        public float WorldScaleY { get { ResolveDirty(); return _worldScaleY; } }
        public bool WorldFlipX { get { ResolveDirty(); return _worldFlipX; } }
        public bool WorldFlipY { get { ResolveDirty(); return _worldFlipY; } }
        public float WorldOpacity { get { ResolveDirty(); return _worldOpacity; } }

        public void MakeDirty()
        {
            if (_isDirty) return;

            _isDirty = true;

            for (int i = 0; i < _children.Count; i++)
            {
                _children[i].MakeDirty();
            }
        }

        public void ResolveDirty()
        {
            if (!_isDirty) return;

            _isDirty = false;

            _transform = Matrix.CreateScale(_flipX ? -_scaleX : _scaleX, _flipY ? -_scaleY : _scaleY, 1) *
                         Matrix.CreateRotationZ(_rotation) *
                         Matrix.CreateTranslation(_position.X, _position.Y, 0);

            if (_parent != null)
            {
                _worldTransform = _transform * _parent.WorldTransform;
                _worldPosition = Vector2.Transform(_position, _parent.WorldTransform);
                _worldRotation = _parent.WorldRotation + (_parent.WorldFlipX ^ _parent.WorldFlipY ? -_rotation : _rotation);
                _worldScaleX = _parent.WorldScaleX * _scaleX;
                _worldScaleY = _parent.WorldScaleY * _scaleY;
                _worldFlipX = _parent.WorldFlipX ^ _flipX;
                _worldFlipY = _parent.WorldFlipY ^ _flipY;
                _worldOpacity = _parent.WorldOpacity * _opacity;
            }
            else
            {
                _worldTransform = _transform;
                _worldPosition = _position;
                _worldRotation = _rotation;
                _worldScaleX = _scaleX;
                _worldScaleY = _scaleY;
                _worldFlipX = _flipX;
                _worldFlipY = _flipY;
                _worldOpacity = _opacity;
            }
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual bool HitTest(Vector2 position)
        {
            return false;
        }

        public virtual BoundingBox2 GetBounds()
        {
            return new BoundingBox2(-16, 16);
        }

        private static Vector2[] _corners = new Vector2[4];
        public BoundingBox2 GetWorldBounds()
        {
            GetBounds().GetCorners(_corners);

            for (int i = 0; i < 4; i++)
            {
                _corners[i] = Vector2.Transform(_corners[i], WorldTransform);
            }

            BoundingBox2 bounds = new BoundingBox2(_corners[0], _corners[0]);

            for (int i = 1; i < 4; i++)
            {
                bounds.Extend(_corners[i]);
            }

            return bounds;
        }

        public virtual INode Clone()
        {
            INode node = new Node();
            CopyTo(node);
            return node;
        }

        protected virtual void CopyTo(INode node)
        {
            node.Name = Name;
            node.Position = Position;
            node.Rotation = Rotation;
            node.ScaleX = ScaleX;
            node.ScaleY = ScaleY;
            node.SkewX = SkewX;
            node.SkewY = SkewY;
            node.FlipX = FlipX;
            node.FlipY = FlipY;
            node.Opacity = Opacity;
            node.IsVisible = IsVisible;
            node.ZIndex = ZIndex;
        }
    }
}
