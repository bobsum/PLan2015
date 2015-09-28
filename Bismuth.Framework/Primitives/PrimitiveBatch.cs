using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bismuth.Framework.Primitives
{
    #region Vertex Definition

    /// <summary>
    /// Describes a custom vertex format structure which is used for rendering primitives.
    /// It contains position, border color, fill color, and one set of texture coordinates.
    /// </summary>
    [Serializable]
    public struct VertexPositionBorderFillTexture : IVertexType
    {
        public Vector3 Position;
        public Color BorderColor;
        public Color FillColor;
        public Vector2 TextureCoordinate;
        public static readonly VertexDeclaration VertexDeclaration = new VertexDeclaration
        (
            new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
            new VertexElement(12, VertexElementFormat.Color, VertexElementUsage.Color, 0),
            new VertexElement(16, VertexElementFormat.Color, VertexElementUsage.Color, 1),
            new VertexElement(20, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0)
        );

        public VertexPositionBorderFillTexture(Vector3 position, Color borderColor, Color fillColor, Vector2 textureCoordinate)
        {
            Position = position;
            BorderColor = borderColor;
            FillColor = fillColor;
            TextureCoordinate = textureCoordinate;
        }

        VertexDeclaration IVertexType.VertexDeclaration { get { return VertexDeclaration; } }
    }

    #endregion

    /// <summary>
    /// This enum describes how borders are aligned when a primitive is drawn.
    /// </summary>
    public enum BorderAlignment
    {
        Inner,
        Outer,
        Center
    }

    public struct PrimitiveBrush
    {
        public BorderAlignment BorderAlignment;
        public float BorderThickness;
        public Color BorderColor;
        public Color FillColor;
    }

    public enum PrimitiveBatchRenderMode
    {
        SoftEdges,
        HardEdges,
        WireFrame
    }

    /// <summary>
    /// Use the PrimitiveBatch to render ultra sharp primitives.
    /// </summary>
    public class PrimitiveBatch
    {
        private GraphicsDevice _graphicsDevice;
        private Effect _effect;
        private EffectParameter _viewportSizeParameter;
        private EffectParameter _transformMatrixParameter;
        private EffectTechnique _softEdgesTechnique;
        private EffectTechnique _hardEdgesTechnique;

        private VertexPositionBorderFillTexture[] _vertexBuffer = new VertexPositionBorderFillTexture[100000];
        private short[] _indexBuffer = new short[100000];

        private int _vertexCount = 0;
        private int _indexCount = 0;

        private Vector2[] _corners = new Vector2[4];

        public Matrix TransformMatrix { get { return _transformMatrix; } set { _transformMatrix = value; } }
        private Matrix _transformMatrix = Matrix.Identity;

        public Texture2D FillTexture { get; set; }
        public Texture2D BorderTexture { get; set; }

        private Color _borderColor;
        private Color _fillColor;
        private float _edge;

        public float EdgeSoftness { get { return _edgeSoftness; } set { _edgeSoftness = value; } }
        private float _edgeSoftness = 0.7f;

        /// <summary>
        /// Gets or sets how a batch is rendered.
        /// </summary>
        public PrimitiveBatchRenderMode RenderMode
        {
            get { return _renderMode; }
            set
            {
                if (_renderMode != value)
                {
                    _renderMode = value;
                    CreateRasterizerState();
                }
            }
        }
        private PrimitiveBatchRenderMode _renderMode = PrimitiveBatchRenderMode.SoftEdges;
        private RasterizerState _rasterizerState = new RasterizerState();

        public PrimitiveBatch(GraphicsDevice graphicsDevice, IServiceProvider services)
            : this(graphicsDevice, services, graphicsDevice.Viewport, 100000) { }

        public PrimitiveBatch(GraphicsDevice graphicsDevice, IServiceProvider services, int bufferSize)
            : this(graphicsDevice, services, graphicsDevice.Viewport, bufferSize) { }

        public PrimitiveBatch(GraphicsDevice graphicsDevice, IServiceProvider services, Viewport viewport, int bufferSize)
        {
            if (graphicsDevice == null) throw new ArgumentNullException("graphicsDevice");

            _graphicsDevice = graphicsDevice;

            ResourceContentManager content = new ResourceContentManager(services, Resources.ResourceManager);

            LoadEffect(content.Load<Effect>("PrimitiveEffect"));

            CreateRasterizerState();
        }

        public void LoadEffect(Effect effect)
        {
            _effect = effect;
            _viewportSizeParameter = _effect.Parameters["ViewportSize"];
            _transformMatrixParameter = _effect.Parameters["TransformMatrix"];
            _softEdgesTechnique = _effect.Techniques["SoftEdges"];
            _hardEdgesTechnique = _effect.Techniques["HardEdges"];
        }

        private void CreateRasterizerState()
        {
            _rasterizerState = new RasterizerState();
            _rasterizerState.CullMode = CullMode.None;
            _rasterizerState.FillMode = RenderMode == PrimitiveBatchRenderMode.WireFrame ? FillMode.WireFrame : FillMode.Solid;
        }

        private void ApplyTransformMatrix(int startIndex, int endIndex, Matrix transformMatrix)
        {
            //for (int i = startIndex; i < endIndex; i++)
            //{
            //    _vertexBuffer[i].Position = Vector3.Transform(_vertexBuffer[i].Position, transformMatrix);
            //}

            Vector2 n = Vector2.TransformNormal(Vector2.UnitX, transformMatrix);

            Matrix m = transformMatrix * Matrix.CreateScale(1, 1, n.Length());

            for (int i = startIndex; i < endIndex; i++)
            {
                _vertexBuffer[i].Position = Vector3.Transform(_vertexBuffer[i].Position, m);
            }
        }

        #region Draw Line Methods

        public void DrawLine(Vector2 start, Vector2 end, PrimitiveBrush brush)
        {
            int startIndex = _vertexCount;
            int lod = 16;

            _borderColor = brush.BorderColor;
            _fillColor = Color.Transparent;

            Vector2 t = end - start;
            t = Vector2.Normalize(new Vector2(-t.Y, t.X)) * brush.BorderThickness * 0.5f;

            Polar2 a = Polar2.CreateFromVector(t);

            _edge = brush.BorderThickness * 0.5f * _edgeSoftness;

            int s0 = _vertexCount;
            PushVertex(start);
            PushVertex(end);

            _edge = 0;

            int s1 = _vertexCount;
            PushVerticesForCircle(start, new Vector2(a.R), a.Theta, a.Theta + MathHelper.Pi, lod);
            int s2 = _vertexCount;
            PushVerticesForCircle(end, new Vector2(a.R), a.Theta + MathHelper.Pi, a.Theta + MathHelper.TwoPi, lod);
            int s3 = _vertexCount;

            PushIndicesForCircle(s0 + 0, s1, lod, false);
            PushIndicesForCircle(s0 + 1, s2, lod, false);

            PushIndicesForRectangle(s3 - 1, s1, s0 + 1, s0);
            PushIndicesForRectangle(s0 + 1, s0, s2, s2 - 1);
        }

        public void DrawLine(Vector2 start, Vector2 end, PrimitiveBrush brush, Matrix transformMatrix)
        {
            int startIndex = _vertexCount;

            DrawLine(start, end, brush);

            // Apply the transformation matrix.
            ApplyTransformMatrix(startIndex, _vertexCount, transformMatrix);
        }

        #endregion // Draw Line Methods

        #region Draw Ellipse and Circle Methods

        public void DrawCircle(Vector2 center, float radius, PrimitiveBrush brush)
        {
            // HACK: Untill the real cricle function is implemented.
            DrawRectangle(new BoundingBox2(-radius, radius) + center, new Vector2(radius), brush);
            //DrawCircle(center, radius, 0, 0, borderColor, fillColor);
        }

        public void DrawCircle(Vector2 center, float radius, PrimitiveBrush brush, Matrix transformMatrix)
        {
            // HACK: Untill the real cricle function is implemented.
            DrawRectangle(new BoundingBox2(-radius, radius) + center, new Vector2(radius), brush, transformMatrix);
            //DrawCircle(center, radius, 0, 0, borderColor, fillColor);
        }

        #endregion // Draw Ellipse and Circle Methods

        #region Draw Ploygon Methods

        public void DrawPolygon(Vector2[] points, PrimitiveBrush brush, Matrix transform)
        {
            for (int i = 1; i < points.Length; i++)
            {
                DrawLine(points[i - 1], points[i], brush, transform);
            }
            DrawLine(points[points.Length - 1], points[0], brush, transform);
        }

        #endregion // Draw Polygon Methods

        #region Draw Rectangle Methods

        public void DrawRectangle(Rectangle rectangle, Vector2 cornerRadius, PrimitiveBrush brush)
        {
            DrawRectangle(new BoundingBox2(rectangle), cornerRadius, brush);
        }

        public void DrawRectangle(Rectangle rectangle, Vector2 cornerRadius, PrimitiveBrush brush, Matrix transformMatrix)
        {
            DrawRectangle(new BoundingBox2(rectangle), cornerRadius, brush, transformMatrix);
        }

        public void DrawRectangle(BoundingBox2 boundingBox, Vector2 cornerRadius, PrimitiveBrush brush, Matrix transformMatrix)
        {
            int startIndex = _vertexCount;
            DrawRectangle(boundingBox, cornerRadius, brush);
            ApplyTransformMatrix(startIndex, _vertexCount, transformMatrix);
        }

        public void DrawRectangle(BoundingBox2 boundingBox, Vector2 cornerRadius, PrimitiveBrush brush)
        {
            int startIndex = _vertexCount;

            float bi = -1;

            Vector2 half = (boundingBox.Max - boundingBox.Min) * 0.5f;

            if (brush.BorderThickness == 0)
            {
                brush.BorderAlignment = BorderAlignment.Inner;
                brush.BorderThickness = Math.Min(half.X, half.Y);
                brush.BorderColor = brush.FillColor;
                bi = 1;
            }

            _borderColor = brush.BorderColor;
            _fillColor = brush.FillColor;

            bi *= brush.BorderThickness * _edgeSoftness;

            if (cornerRadius.X < 0) cornerRadius.X = 0;
            if (cornerRadius.Y < 0) cornerRadius.Y = 0;
            if (cornerRadius.X > half.X) cornerRadius.X = half.X;
            if (cornerRadius.Y > half.Y) cornerRadius.Y = half.Y;

            boundingBox.Min += cornerRadius;
            boundingBox.Max -= cornerRadius;
            boundingBox.GetCorners(_corners);

            // Setting the inner and outer border radius based on BorderAlignment.
            Vector2 innerRadius = cornerRadius;
            Vector2 outerRadius = cornerRadius;

            if (brush.BorderAlignment == BorderAlignment.Inner)
            {
                innerRadius -= new Vector2(brush.BorderThickness);
            }
            else if (brush.BorderAlignment == BorderAlignment.Outer)
            {
                outerRadius += new Vector2(brush.BorderThickness);
            }
            else
            {
                innerRadius -= new Vector2(brush.BorderThickness * 0.5f);
                outerRadius += new Vector2(brush.BorderThickness * 0.5f);
            }

            if (innerRadius.X < 0 && cornerRadius.X > half.X) innerRadius.X = 0;
            if (innerRadius.Y < 0 && cornerRadius.Y > half.Y) innerRadius.Y = 0;

            //int lod = (int)outerRadius;
            //int lod = (int)(2 * MathHelper.Pi * (cornerRadius.X + cornerRadius.Y) / 16);
            int lod = 16;

            if (Math.Min(cornerRadius.X, cornerRadius.Y) == 0)
            {
                BoundingBox2 innerBox = new BoundingBox2(boundingBox.Min - innerRadius, boundingBox.Max + innerRadius);
                BoundingBox2 outerBox = new BoundingBox2(boundingBox.Min - outerRadius, boundingBox.Max + outerRadius);

                _edge = bi;

                innerBox.GetCorners(_corners);
                PushVerticesForRectangle(_corners);

                _edge = 0;

                outerBox.GetCorners(_corners);
                PushVerticesForRectangle(_corners);

                if (_fillColor.A > 0)
                    PushIndicesForRectangle(startIndex + 3, startIndex + 0, startIndex + 2, startIndex + 1);

                PushIndicesForStrip(startIndex, startIndex + 4, 4, true);
            }
            else if (Math.Min(innerRadius.X, innerRadius.Y) <= 0)
            {
                //_borderColor = Color.GreenYellow;

                _edge = bi;

                // Center rectangle.
                PushVerticesForRectangle(_corners);

                if (_fillColor.A > 0)
                    PushIndicesForRectangle(startIndex + 3, startIndex, startIndex + 2, startIndex + 1);

                _edge = 0;

                // Create border.
                PushVerticesAndIndicesForBorder(startIndex, _corners, outerRadius, lod, true);

                // Reposition inner corner.
                _vertexBuffer[startIndex + 0].Position.X -= innerRadius.X;
                _vertexBuffer[startIndex + 0].Position.Y -= innerRadius.Y;
                _vertexBuffer[startIndex + 1].Position.X += innerRadius.X;
                _vertexBuffer[startIndex + 1].Position.Y -= innerRadius.Y;
                _vertexBuffer[startIndex + 2].Position.X += innerRadius.X;
                _vertexBuffer[startIndex + 2].Position.Y += innerRadius.Y;
                _vertexBuffer[startIndex + 3].Position.X -= innerRadius.X;
                _vertexBuffer[startIndex + 3].Position.Y += innerRadius.Y;
            }
            // If the inner radius is greater than zero, we need to create a inner and outer border.
            else if (Math.Min(innerRadius.X, innerRadius.Y) > 0)
            {
                //_borderColor = Color.Orange;

                _edge = bi;

                // Center rectangle.
                if (_fillColor.A > 0)
                {
                    PushVerticesForRectangle(_corners);
                    PushIndicesForRectangle(startIndex + 3, startIndex, startIndex + 2, startIndex + 1);
                }

                int innerStartIndex = _vertexCount;

                // Create inner border.
                PushVerticesAndIndicesForBorder(startIndex, _corners, innerRadius, lod, _fillColor.A > 0);

                _edge = 0;

                int outerStartIndex = _vertexCount;

                // Create outer border.
                PushVerticesForCircle(_corners[0], outerRadius, -MathHelper.Pi, -MathHelper.PiOver2, lod);
                PushVerticesForCircle(_corners[1], outerRadius, -MathHelper.PiOver2, 0, lod);
                PushVerticesForCircle(_corners[2], outerRadius, 0, MathHelper.PiOver2, lod);
                PushVerticesForCircle(_corners[3], outerRadius, MathHelper.PiOver2, MathHelper.Pi, lod);

                PushIndicesForStrip(innerStartIndex, outerStartIndex, lod * 4 + 4, true);
            }
        }

        #endregion // Draw Methods

        #region Push Vertices and Indices Methods

        /// <summary>
        /// Pushes a single vertex to the vertex buffer.
        /// </summary>
        public void PushVertex(Vector2 position)
        {
            _vertexBuffer[_vertexCount].Position = new Vector3(position, _edge);
            _vertexBuffer[_vertexCount].BorderColor = _borderColor;
            _vertexBuffer[_vertexCount].FillColor = _fillColor;
            _vertexCount++;
        }

        /// <summary>
        /// Given four corners, this method pushes vertices for a rectangel to the vertex buffer.
        /// </summary>
        private void PushVerticesForRectangle(Vector2[] corners)
        {
            for (int i = 0; i < corners.Length; i++)
            {
                PushVertex(corners[i]);
            }
        }

        /// <summary>
        /// This method pushes vertices for a circle to the vertex buffer.
        /// </summary>
        private void PushVerticesForCircle(Vector2 center, Vector2 radius, float minAngle, float maxAngle, int segmentCount)
        {
            float increment = (maxAngle - minAngle) / segmentCount;

            float angle = minAngle;

            for (int i = 0; i <= segmentCount; i++)
            {
                Vector2 vector = new Vector2();
                vector.X = (float)Math.Cos(angle) * radius.X;
                vector.Y = (float)Math.Sin(angle) * radius.Y;

                PushVertex(center + vector);
                angle += increment;
            }
        }

        /// <summary>
        /// Pushes four corner-indices for a rectangle to the index buffer.
        /// </summary>
        private void PushIndicesForRectangle(int i0, int i1, int i2, int i3)
        {
            _indexBuffer[_indexCount + 0] = (short)i0;
            _indexBuffer[_indexCount + 1] = (short)i1;
            _indexBuffer[_indexCount + 2] = (short)i2;

            _indexBuffer[_indexCount + 3] = (short)i3;
            _indexBuffer[_indexCount + 4] = (short)i2;
            _indexBuffer[_indexCount + 5] = (short)i1;

            _indexCount += 6;
        }

        /// <summary>
        /// Pushes indices for a circle to the index buffer.
        /// </summary>
        private void PushIndicesForCircle(int centerIndex, int startIndex, int segmentCount, bool wrap)
        {
            for (int i = 0; i < segmentCount; i++)
            {
                _indexBuffer[_indexCount + 0] = (short)centerIndex;
                _indexBuffer[_indexCount + 1] = (short)(startIndex + i);
                _indexBuffer[_indexCount + 2] = (short)(startIndex + i + 1);

                _indexCount += 3;
            }

            if (wrap)
            {
                // TODO: This does not work yet... think it through!
                _indexBuffer[_indexCount + 0] = (short)centerIndex;
                _indexBuffer[_indexCount + 1] = (short)segmentCount;
                _indexBuffer[_indexCount + 2] = (short)startIndex;
            }
        }

        /// <summary>
        /// Pushes indices for a strip of inner-outer-paired points to the index buffer.
        /// </summary>
        private void PushIndicesForStrip(int innerStartIndex, int outerStartIndex, int elementCount, bool wrap)
        {
            elementCount--;
            for (int i = 0; i < elementCount; i++)
            {
                PushIndicesForRectangle(
                    innerStartIndex + i,
                    outerStartIndex + i,
                    innerStartIndex + i + 1,
                    outerStartIndex + i + 1);
            }

            if (wrap)
            {
                PushIndicesForRectangle(
                    innerStartIndex + elementCount,
                    outerStartIndex + elementCount,
                    innerStartIndex,
                    outerStartIndex);
            }
        }

        /// <summary>
        /// A common method to add vertices and indices for a border. Used by the DrawRectangle method.
        /// </summary>
        private void PushVerticesAndIndicesForBorder(int startIndex, Vector2[] corners, Vector2 cornerRadius, int lod, bool createIndices)
        {
            int s0 = _vertexCount;
            PushVerticesForCircle(_corners[0], cornerRadius, -MathHelper.Pi, -MathHelper.PiOver2, lod);
            int s1 = _vertexCount;
            PushVerticesForCircle(_corners[1], cornerRadius, -MathHelper.PiOver2, 0, lod);
            int s2 = _vertexCount;
            PushVerticesForCircle(_corners[2], cornerRadius, 0, MathHelper.PiOver2, lod);
            int s3 = _vertexCount;
            PushVerticesForCircle(_corners[3], cornerRadius, MathHelper.PiOver2, MathHelper.Pi, lod);

            if (createIndices)
            {
                PushIndicesForCircle(startIndex + 0, s0, lod, false);
                PushIndicesForCircle(startIndex + 1, s1, lod, false);
                PushIndicesForCircle(startIndex + 2, s2, lod, false);
                PushIndicesForCircle(startIndex + 3, s3, lod, false);

                PushIndicesForRectangle(startIndex + 0, s0 + lod, startIndex + 1, s1);
                PushIndicesForRectangle(startIndex + 1, s1 + lod, startIndex + 2, s2);
                PushIndicesForRectangle(startIndex + 2, s2 + lod, startIndex + 3, s3);
                PushIndicesForRectangle(startIndex + 3, s3 + lod, startIndex + 0, s0);
            }
        }

        #endregion // Push Vertices and Indices Methods

        public void Apply()
        {
            _graphicsDevice.RasterizerState = _rasterizerState;

            if (RenderMode == PrimitiveBatchRenderMode.HardEdges || RenderMode == PrimitiveBatchRenderMode.WireFrame)
            {
                _effect.CurrentTechnique = _hardEdgesTechnique;
            }
            else
            {
                _effect.CurrentTechnique = _softEdgesTechnique;
            }

            Viewport viewport = _graphicsDevice.Viewport;
            _viewportSizeParameter.SetValue(new Vector2((float)viewport.Width, (float)viewport.Height));
            _transformMatrixParameter.SetValue(_transformMatrix);

            _effect.CurrentTechnique.Passes[0].Apply();

            if (_vertexCount > 0)
                _graphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, _vertexBuffer, 0, _vertexCount, _indexBuffer, 0, _indexCount / 3, VertexPositionBorderFillTexture.VertexDeclaration);

            VertexCount = _vertexCount;
            IndexCount = _indexCount;

            _vertexCount = 0;
            _indexCount = 0;
        }

        public int VertexCount { get; private set; }
        public int IndexCount { get; private set; }
    }
}
