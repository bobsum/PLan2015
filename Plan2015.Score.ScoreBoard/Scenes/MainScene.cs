using Bismuth.Framework.Composite;
using Bismuth.Framework.Scenes;
using Bismuth.Framework.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plan2015.Score.ScoreBoard.Scenes
{
    public class MainScene : Scene
    {
        public MainScene(MainGame game)
        {
            Game = game;
        }

        public MainGame Game { get; private set; }

        public INode Root { get; private set; }

        public override void Initialize()
        {
            
        }

        public override void LoadContent()
        {
            Root = Game.ContentManager.Load<INode>("Scenes/SchoolScene");
        }

        public override void Update(GameTime gameTime)
        {
            NodeTree.Update(Root, gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.CornflowerBlue);

            Game.SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Game.ResolutionManager.TransformMatrix);

            SpriteTree.Draw(Root, Game.XnaSpriteBatch);

            Game.SpriteBatch.End();
        }
    }
}
