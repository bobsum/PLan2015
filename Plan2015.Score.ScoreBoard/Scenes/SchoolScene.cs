using Bismuth.Framework.Composite;
using Bismuth.Framework.Content;
using Bismuth.Framework.GameObjects;
using Bismuth.Framework.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Plan2015.Score.ScoreBoard.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bismuth.Framework.Input;
using Bismuth.Framework.Animations;
using Plan2015.Score.ScoreBoard.Earthquakes;

namespace Plan2015.Score.ScoreBoard.Scenes
{
    public class SchoolScene : Scene
    {
        public SchoolScene()
        {
            EarthquakeManager = new EarthquakeManager();
        }

        public ListBox SchoolListBox { get; private set; }
        public EarthquakeManager EarthquakeManager { get; private set; }

        public override void LoadContent(IContentManager contentManager)
        {
            SchoolListBox = this.Find<ListBox>("SchoolListBox");
            SchoolListBox.Comparer = Compare;
            SchoolListBox.Swapped = () =>
                {
                    EarthquakeManager.Push(new EarthquakeY(20));
                    EarthquakeManager.Push(new EarthquakeX(15));
                };
        }

        private bool Compare(INode a, INode b)
        {
            if (EarthquakeManager.Matrix != Matrix.Identity) return false;

            return ((School)a).Score.Amount < ((School)b).Score.Amount;
        }

        public override void Update(GameTime gameTime)
        {
            EarthquakeManager.Update(gameTime);
        }
    }
}
