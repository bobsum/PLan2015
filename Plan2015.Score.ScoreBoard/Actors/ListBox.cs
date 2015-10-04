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
    public class ListBox : Actor
    {
        private readonly List<ListBoxItem> _items = new List<ListBoxItem>();
        public List<ListBoxItem> Items { get { return _items; } }

        public BoundingBox2 Area { get; set; }

        public Func<INode, INode, bool> Comparer { get; set; }

        public INode _swap;

        public Animation _animation;

        public override void Update(GameTime gameTime)
        {
            for (int i = 1; i < _items.Count; i++)
            {
                ListBoxItem itemA = _items[i - 1];
                ListBoxItem itemB = _items[i];
                INode contentA = itemA.Content;
                INode contentB = itemB.Content;

                if (Comparer(contentA, contentB))
                {
                    itemA.Content = null;
                    itemB.Content = null;

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
