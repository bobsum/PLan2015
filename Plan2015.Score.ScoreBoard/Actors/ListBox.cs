using Bismuth.Framework;
using Bismuth.Framework.Animations;
using Bismuth.Framework.Composite;
using Bismuth.Framework.Content;
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

        public Func<INode, INode, bool> Comparer { get; set; }

        private INode _swapRoot;
        private ListBoxItem _swapA;
        private ListBoxItem _swapB;
        private ListBoxItem _itemA;
        private ListBoxItem _itemB;

        private Animation _swapAnimation;

        public string SwapAnimation { get; set; }

        public ListBox()
        {
            _swapRoot = new Node { Name = "Root" };
            _swapRoot.Children.Add(_swapA = new ListBoxItem { Name = "A" });
            _swapRoot.Children.Add(_swapB = new ListBoxItem { Name = "B" });
        }

        public override void LoadContent(IContentManager contentManager)
        {
            this.FindAll<ListBoxItem>(_items);

            _swapAnimation = contentManager.Load<Animation>(SwapAnimation, true);
            _swapAnimation.Bind(_swapRoot);
        }

        public void Add(INode node)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Content == null)
                {
                    _items[i].Content = node;
                    return;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (_swapAnimation.State == AnimationState.Playing)
            {
                _swapAnimation.Update(gameTime);
            }
            else if (_swapAnimation.State == AnimationState.Finished)
            {
                INode contentA = _swapA.Content;
                INode contentB = _swapB.Content;

                _swapA.Content = null;
                _swapB.Content = null;

                _itemA.Content = contentB;
                _itemB.Content = contentA;

                Children.Remove(_swapRoot);

                _swapAnimation.Reset();
            }
            else
            {
                for (int i = 1; i < _items.Count; i++)
                {
                    _itemA = _items[i - 1];
                    _itemB = _items[i];
                    INode contentA = _itemA.Content;
                    INode contentB = _itemB.Content;

                    if (contentA != null && contentB != null && Comparer(contentA, contentB))
                    {
                        _itemA.Content = null;
                        _itemB.Content = null;

                        _swapAnimation.Reset();
                        _swapAnimation.Play();

                        _swapA.Content = contentA;
                        _swapB.Content = contentB;

                        _swapRoot.Position = (_itemA.Position + _itemB.Position) * 0.5f;

                        Children.Add(_swapRoot);

                        _swapAnimation.Update(gameTime);

                        break;
                    }
                }
            }
        }
    }
}
