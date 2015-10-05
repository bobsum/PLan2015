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

namespace Plan2015.Score.ScoreBoard.Scenes
{
    public class SchoolScene : Scene
    {
        public ListBox SchoolListBox { get; private set; }

        public override void LoadContent(IContentManager contentManager)
        {
            SchoolListBox = this.Find<ListBox>("SchoolListBox");
            SchoolListBox.Comparer = Compare;
        }

        private bool Compare(INode a, INode b)
        {
            return ((School)a).Score.Amount < ((School)b).Score.Amount;
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
