using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Bismuth.Framework.Content;
using Bismuth.Framework.Input;
using Bismuth.Framework.Scenes;
using Bismuth.Framework.Sprites;
using Bismuth.Framework.Primitives;

namespace Bismuth.Framework
{
    public class BismuthGame : Game
    {
        public GraphicsDeviceManager Graphics { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }
        public ISpriteBatch XnaSpriteBatch { get; private set; }
        public PrimitiveBatch PrimitiveBatch { get; private set; }

        public ResolutionManager ResolutionManager { get; private set; }
        public BismuthContentManager ContentManager { get; private set; }
        public SceneManager SceneManager { get; private set; }

        public BismuthGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            ResolutionManager = new ResolutionManager(Graphics);

            Content.RootDirectory = "Content";

            ContentManager = new BismuthContentManager(Services);
            ContentManager.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures,
            // and a new PrimitiveBatch, which can be used to draw primitives.
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            XnaSpriteBatch = new XnaSpriteBatch(SpriteBatch);
            PrimitiveBatch = new PrimitiveBatch(GraphicsDevice, Services);

            SceneManager = new SceneManager(this);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Updates the input state to the new state of the mouse, keyboard and controllers.
            InputState.Update(gameTime);

            SceneManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            SceneManager.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
