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
            SchoolListBox.Comparer = SchoolComparer;
            SchoolListBox.Swapped = (a, b) =>
                {
                    EarthquakeManager.Push(new EarthquakeY(20));
                    EarthquakeManager.Push(new EarthquakeX(15));
                    ((School)b).MagicExplosionEmitter.Emit();
                };
        }

        private bool SchoolComparer(INode a, INode b)
        {
            if (EarthquakeManager.Matrix != Matrix.Identity) return false;

            return ((School)a).Score.Amount < ((School)b).Score.Amount;
        }

        public override void Update(GameTime gameTime)
        {
            bool anyHouseListBoxIsSorting = false;
            for (int i = 0; i < SchoolListBox.Items.Count; i++)
            {
                School school = SchoolListBox.Items[i].Content as School;
                if (school != null)
                {
                    school.HouseListBox.UpdateSort(gameTime);
                    if (school.HouseListBox.IsSorting) anyHouseListBoxIsSorting = true;
                }
            }

            if (!anyHouseListBoxIsSorting)
            {
                SchoolListBox.UpdateSort(gameTime);
            }

            EarthquakeManager.Update(gameTime);
        }
    }
}
