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
        }

        private void Initialized()
        {
            foreach (SchoolScore schoolScore in ScoreClient.SchoolScores)
            {
                School school = Game.ContentManager.Load<School>("Actors/School", true);
                school.Score = schoolScore;
                school.Find<Sprite>("Logo").Texture = Game.ContentManager.Load<Texture2D>("Textures/" + schoolScore.Name);

                foreach (HouseScore houseScore in schoolScore.HouseScores)
                {
                    House house = Game.ContentManager.Load<House>("Actors/House", true);
                    house.Score = houseScore;
                    school.HouseListBox.Add(house);
                }

                Root.SchoolListBox.Add(school);
            }
        }

        public override void LoadContent()
        {
            ScoreClient = new ScoreClient("http://192.168.1.78");
            //ScoreClient = new ScoreClientMock();
            ScoreClient.Initialized = Initialized;

            Root = Game.ContentManager.Load<SchoolScene>("Scenes/SchoolScene");
            ScoreClient.Start();
        }

        public override void Update(GameTime gameTime)
        {
            ScoreClientMock scoreClientMock = ScoreClient as ScoreClientMock;
            if (scoreClientMock != null) scoreClientMock.Update(gameTime);

            NodeTree.Update(Root, gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.Black);

            Game.SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Root.EarthquakeManager.Matrix * Game.ResolutionManager.TransformMatrix);

            SpriteTree.Draw(Root, Game.XnaSpriteBatch);

            Game.SpriteBatch.End();
        }
    }
}
