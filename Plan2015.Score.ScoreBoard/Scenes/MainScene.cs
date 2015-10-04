using Bismuth.Framework.Composite;
using Bismuth.Framework.Scenes;
using Bismuth.Framework.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Plan2015.Score.Client;
using Plan2015.Score.ScoreBoard.Actors;
using Plan2015.Score.ScoreBoard.Mocks;
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

        public SchoolScene Root { get; private set; }

        public IScoreClient ScoreClient { get; private set; }

        public override void Initialize()
        {
            ScoreClient = new ScoreClientMock();
            ScoreClient.SchoolScoreAdded = SchoolScoreAdded;
        }

        private void SchoolScoreAdded(SchoolScore schoolScore)
        {
            School school = Game.ContentManager.Load<School>("Actors/School", true);
            school.SchoolScore = schoolScore;
            // TODO: Added to list box of ScoreScene.
        }

        public override void LoadContent()
        {
            Root = Game.ContentManager.Load<SchoolScene>("Scenes/SchoolScene");
        }

        public override void Update(GameTime gameTime)
        {
            ScoreClientMock scoreClientMock = ScoreClient as ScoreClientMock;
            if (scoreClientMock != null) scoreClientMock.Update(gameTime);

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
