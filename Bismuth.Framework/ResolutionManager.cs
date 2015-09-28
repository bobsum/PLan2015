using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bismuth.Framework
{
    /// <summary>
    /// The ResolutionManager is used to map the resolution used by the game (virtual resolution)
    /// the specefied, supported and/or optimal resolution of the device running the game (display resolution).
    /// </summary>
    public class ResolutionManager
    {
        private GraphicsDeviceManager _graphics;

        private int _displayWidth, _displayHeight;
        private int _virtualWidth, _virtualHeight;

        private Viewport _displayViewport;

        //private Viewport _virutalViewport;

        private bool _fullScreen;
        private Matrix _transformMatrix = Matrix.Identity;

        public Matrix TransformMatrix { get { return _transformMatrix; } }

        public ResolutionManager(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;
            _displayWidth = graphics.PreferredBackBufferWidth;
            _displayHeight = graphics.PreferredBackBufferHeight;
            _virtualWidth = graphics.PreferredBackBufferWidth;
            _virtualHeight = graphics.PreferredBackBufferHeight;
        }

        public void ApplyChanges()
        {
            _graphics.PreferredBackBufferWidth = _displayWidth;
            _graphics.PreferredBackBufferHeight = _displayHeight;

            _graphics.IsFullScreen = _fullScreen;
            _graphics.ApplyChanges();

            _displayWidth = _graphics.PreferredBackBufferWidth;
            _displayHeight = _graphics.PreferredBackBufferHeight;

            float aspectWidth = (float)_displayWidth / (float)_virtualWidth;
            float aspectHeight = (float)_displayHeight / (float)_virtualHeight;

            float aspect = Math.Min(aspectWidth, aspectHeight);

            _transformMatrix = Matrix.CreateScale(aspect, aspect, 1);

            float width = _virtualWidth * aspect;
            float height = _virtualHeight * aspect;

            float offsetX = (_displayWidth - width) * 0.5f;
            float offsetY = (_displayHeight - height) * 0.5f;

            _displayViewport = new Viewport((int)offsetX, (int)offsetY, (int)width, (int)height);

            _graphics.GraphicsDevice.Viewport = _displayViewport;
        }

        public void SetDisplayResolution(int width, int height, bool fullScreen)
        {
            _displayWidth = width;
            _displayHeight = height;
            _fullScreen = fullScreen;
        }

        public void SetVirtualResolution(int width, int height)
        {
            _virtualWidth = width;
            _virtualHeight = height;
        }

        public Viewport GetDisplayViewport()
        {
            return _displayViewport;
        }

        public Viewport GetDisplayViewport(Viewport virtualViewport)
        {
            BoundingBox2 virtualBox = new BoundingBox2(0, 0, _virtualWidth, _virtualHeight);

            BoundingBox2 box = virtualBox.Intersection(new BoundingBox2(virtualViewport.Bounds));

            box.Min = Vector2.Transform(box.Min, _transformMatrix);
            box.Max = Vector2.Transform(box.Max, _transformMatrix);

            Viewport displayViewport = new Viewport(box.ToRectangle());
            displayViewport.X += _displayViewport.X;
            displayViewport.Y += _displayViewport.Y;

            return displayViewport;
        }

        public Viewport GetVirtualViewport()
        {
            return new Viewport(0, 0, _virtualWidth, _virtualHeight);
        }

        public void SetViewport(Viewport virtualViewport)
        {
            _graphics.GraphicsDevice.Viewport = GetDisplayViewport(virtualViewport);
        }

        public void ResetViewport()
        {
            _graphics.GraphicsDevice.Viewport = _displayViewport;
        }
    }
}
