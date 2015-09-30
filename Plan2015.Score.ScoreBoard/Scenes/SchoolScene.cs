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
        private readonly List<School> _schools = new List<School>();

        private INode _schoolsLayer;
        private Swap _swap;

        private bool _isSwapping = false;

        public override void LoadContent(IContentManager contentManager)
        {
            this.FindAll<School>(_schools);

            _schools[0].Find<Sprite>("Logo").Texture = contentManager.Load<Texture2D>("Textures/Agernholdt");
            _schools[1].Find<Sprite>("Logo").Texture = contentManager.Load<Texture2D>("Textures/Hardenberg");
            _schools[2].Find<Sprite>("Logo").Texture = contentManager.Load<Texture2D>("Textures/Ravnsborg");

            _schoolsLayer = this.Find("SchoolsLayer");

            _swap = contentManager.Load<Swap>("Actors/SchoolSwap");
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyPressedOnce(Keys.D1)) _schools[0].Score++;
            if (ks.IsKeyPressedOnce(Keys.D2)) _schools[1].Score++;
            if (ks.IsKeyPressedOnce(Keys.D3)) _schools[2].Score++;

            if (_swap.Animation.State == AnimationState.Playing)
            {

            }
            else if (_swap.Animation.State == AnimationState.Finished)
            {

                School a = (School)_swap.A.Children[0];
                School b = (School)_swap.B.Children[0];

                a.Position = _swap.A.WorldPosition;
                b.Position = _swap.B.WorldPosition;

                _schoolsLayer.Children.Remove(_swap);
                _swap.A.Children.Remove(a);
                _swap.B.Children.Remove(b);


                _schoolsLayer.Children.Add(a);
                _schoolsLayer.Children.Add(b);

                _swap.Animation.Reset();
            }
            else
            {
                for (int i = 1; i < 3; i++)
                {
                    School a = _schools[i - 1];
                    School b = _schools[i];
                    if (a.Score < b.Score)
                    {
                        _swap.Animation.Reset();
                        _swap.Animation.Play();

                        _schoolsLayer.Children.Remove(a);
                        _schoolsLayer.Children.Remove(b);
                        _schoolsLayer.Children.Add(_swap);

                        _swap.A.Children.Add(a);
                        _swap.B.Children.Add(b);

                        _swap.Position = (a.Position + b.Position) * 0.5f;
                        a.Position = Vector2.Zero;
                        b.Position = Vector2.Zero;

                        _schools[i - 1] = b;
                        _schools[i] = a;

                        break;
                    }
                }
            }
        }
    }
}
