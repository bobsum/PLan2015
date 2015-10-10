using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Bismuth.Framework;
using Plan2015.Score.ScoreBoard.Settings;
using Bismuth.Framework.Particles;
using Plan2015.Score.ScoreBoard.Scenes;
using Bismuth.Framework.Audio;

namespace Plan2015.Score.ScoreBoard
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame : BismuthGame
    {
        public Configuration Configuration { get; set; }
        public Pool<Particle> Pool { get; private set; }
        public static ISoundManager SoundManager { get; private set; }

        public MainScene MainScene { get; private set; }

        public MainGame()
        {
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            IsMouseVisible = false;

            Configuration = Configuration.Load();

            VideoSettings display = Configuration.Video;

            if (display.AutoDetectResolution)
            {
                DisplayMode displayMode = GraphicsDevice.Adapter.CurrentDisplayMode;
                display.Width = displayMode.Width;
                display.Height = displayMode.Height;
            }

            ResolutionManager.SetDisplayResolution(display.Width, display.Height, display.IsFullScreen);
            //ResolutionManager.SetVirtualResolution(1280, 720);
            ResolutionManager.SetVirtualResolution(1600, 900);
            ResolutionManager.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();

            Pool = new Pool<Particle>();
            SoundManager = new XactSoundManager("Content//Audio//Plan2015.xgs", "Content//Audio//Plan2015.xwb", "Content//Audio//Plan2015.xsb");

            SceneManager.AddScene("Main", MainScene = new MainScene(this));
            MainScene.LoadContent();
            MainScene.Initialize();

            SceneManager.ChangeScene("Main");
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
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
