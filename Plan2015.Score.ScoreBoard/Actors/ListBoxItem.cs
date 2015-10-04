using Bismuth.Framework.Composite;
using Bismuth.Framework.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plan2015.Score.ScoreBoard.Actors
{
    public class ListBoxItem : Actor
    {
        private INode _content;

        public INode Content
        {
            get { return _content; }
            set
            {
                if (_content != value)
                {
                    if (_content != null) Children.Remove(_content);
                    _content = value;
                    if (_content != null) Children.Add(_content);
                }
            }
        }
    }
}
