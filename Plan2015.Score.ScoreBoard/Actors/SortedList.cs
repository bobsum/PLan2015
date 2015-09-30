using Bismuth.Framework;
using Bismuth.Framework.Animations;
using Bismuth.Framework.Composite;
using Bismuth.Framework.GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plan2015.Score.ScoreBoard.Actors
{
    public class SortedList : Actor
    {
        private readonly List<INode> _items = new List<INode>();
        public List<INode> Items { get { return _items; } }

        public BoundingBox2 Area { get; set; }

        public Func<INode, INode, bool> Comparer { get; set; }

        public INode _swap;

        public Animation _animation;

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < Children.Count; i++)
            {

            }

            for (int i = 1; i < 3; i++)
            {
                INode a = Children[i - 1];
                INode b = Children[i];
                if (Comparer(a, b))
                {
                    //_swap.Animation.Reset();
                    //_swap.Animation.Play();

                    //_schoolsLayer.Children.Remove(a);
                    //_schoolsLayer.Children.Remove(b);
                    //_schoolsLayer.Children.Add(_swap);

                    //_swap.A.Children.Add(a);
                    //_swap.B.Children.Add(b);

                    //_swap.Position = (a.Position + b.Position) * 0.5f;
                    //a.Position = Vector2.Zero;
                    //b.Position = Vector2.Zero;

                    //_schools[i - 1] = b;
                    //_schools[i] = a;

                    break;
                }
            }
        }
    }
}
