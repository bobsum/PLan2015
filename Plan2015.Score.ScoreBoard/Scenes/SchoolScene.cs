using Bismuth.Framework.Composite;
using Bismuth.Framework.Content;
using Bismuth.Framework.GameObjects;
using Bismuth.Framework.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Plan2015.Score.ScoreBoard.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plan2015.Score.ScoreBoard.Scenes
{
    public class SchoolScene : Scene
    {
        private readonly List<School> _schools = new List<School>();

        public override void LoadContent(IContentManager contentManager)
        {
            this.FindAll<School>(_schools);

            _schools[0].Find<Sprite>("Logo").Texture = contentManager.Load<Texture2D>("Textures/Agernholdt");
            _schools[1].Find<Sprite>("Logo").Texture = contentManager.Load<Texture2D>("Textures/Hardenberg");
            _schools[2].Find<Sprite>("Logo").Texture = contentManager.Load<Texture2D>("Textures/Ravnsborg");
        }
    }
}
