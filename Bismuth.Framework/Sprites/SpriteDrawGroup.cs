using System.Collections.Generic;
using Bismuth.Framework.Composite;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework.Sprites
{
    public class SpriteDrawGroup : Node, ISpriteDrawGroup
    {
        private class ZIndexComparer : Comparer<int>
        {
            private readonly INode[] _buffer;

            public ZIndexComparer(INode[] buffer)
            {
                _buffer = buffer;
            }

            public override int Compare(int x, int y)
            {
                if (_buffer[x].ZIndex < _buffer[y].ZIndex) return -1;
                if (_buffer[x].ZIndex > _buffer[y].ZIndex) return 1;
                if (x < y) return -1;
                if (x > y) return 1;
                return 0;
            }
        }

        private readonly ZIndexComparer _zIndexComparer;
        private readonly List<int> _indexList = new List<int>();
        private readonly INode[] _buffer = new INode[1000];
        private int _bufferCount = 0;

        public SpriteDrawGroup()
        {
            _zIndexComparer = new ZIndexComparer(_buffer);
        }

        public bool IsScissorTestEnabled { get; set; }
        public Rectangle ScissorRectangle { get; set; }

        private void Push(INode node)
        {
            if (node.IsVisible)
            {
                _buffer[_bufferCount] = node;
                _bufferCount++;

                if (node is SpriteDrawGroup) return;

                for (int i = 0; i < node.Children.Count; i++)
                {
                    Push(node.Children[i]);
                }
            }
        }

        private bool HasChildrenChanged()
        {
            if (_indexList.Count != _bufferCount)
            {
                _indexList.Clear();
                for (int i = 0; i < _bufferCount; i++)
                {
                    _indexList.Add(i);
                }
                return true;
            }

            int previousZIndex = int.MinValue;
            int previousIndex = -1;
            for (int i = 0; i < _indexList.Count; i++)
            {
                int index = _indexList[i];

                INode node = _buffer[index];
                if (node.ZIndex < previousZIndex) return true;
                if (node.ZIndex == previousZIndex && index < previousIndex) return true;

                previousZIndex = node.ZIndex;
                previousIndex = index;
            }

            return false;
        }

        public void Draw(ISpriteBatch spriteBatch)
        {
            if (IsScissorTestEnabled)
            {
                Rectangle scissorRectangle = ScissorRectangle;
                scissorRectangle.X += (int)WorldPosition.X;
                scissorRectangle.Y += (int)WorldPosition.Y;
                spriteBatch.PushScissorRectangle(scissorRectangle);
            }

            _bufferCount = 0;
            for (int i = 0; i < Children.Count; i++)
            {
                Push(Children[i]);
            }

            if (HasChildrenChanged())
            {
                //Console.WriteLine("Sort!");
                _indexList.Sort(_zIndexComparer);
            }

            for (int i = 0; i < _indexList.Count; i++)
            {
                INode node = _buffer[_indexList[i]];
                ISprite sprite = node as ISprite;
                if (sprite != null)
                    sprite.Draw(spriteBatch);
            }

            if (IsScissorTestEnabled)
            {
                spriteBatch.PopScissorRectangle();
            }
        }

        public override INode Clone()
        {
            INode node = new SpriteDrawGroup();
            CopyTo(node);
            return node;
        }

        protected override void CopyTo(INode node)
        {
            base.CopyTo(node);

            SpriteDrawGroup spriteDrawGroup = (SpriteDrawGroup)node;
            spriteDrawGroup.IsScissorTestEnabled = IsScissorTestEnabled;
            spriteDrawGroup.ScissorRectangle = ScissorRectangle;
        }
    }
}
