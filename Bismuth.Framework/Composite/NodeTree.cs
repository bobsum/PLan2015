using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework.Composite
{
    public static class NodeTree
    {
        public static void Update(INode node, GameTime gameTime)
        {
            node.Update(gameTime);
            for (int i = 0; i < node.Children.Count; i++)
            {
                Update(node.Children[i], gameTime);
            }
        }

        #region Find

        public static INode Find(this INode node, string name)
        {
            if (node.Name == name) return node;
            for (int i = 0; i < node.Children.Count; i++)
            {
                INode result = Find(node.Children[i], name);
                if (result != null) return result;
            }
            return null;
        }

        public static T Find<T>(this INode node) where T : INode
        {
            if (node is T) return (T)node;
            for (int i = 0; i < node.Children.Count; i++)
            {
                INode result = Find<T>(node.Children[i]);
                if (result != null) return (T)result;
            }
            return default(T);
        }

        public static T Find<T>(this INode node, string name) where T : INode
        {
            if (node is T && node.Name == name) return (T)node;
            for (int i = 0; i < node.Children.Count; i++)
            {
                INode result = Find<T>(node.Children[i], name);
                if (result != null) return (T)result;
            }
            return default(T);
        }

        public static void FindAll(this INode node, string name, ICollection<INode> result)
        {
            if (node.Name == name) result.Add(node);
            for (int i = 0; i < node.Children.Count; i++)
            {
                FindAll(node.Children[i], name, result);
            }
        }

        public static void FindAll<T>(this INode node, ICollection<T> result) where T : INode
        {
            if (node is T) result.Add((T)node);
            for (int i = 0; i < node.Children.Count; i++)
            {
                FindAll<T>(node.Children[i], result);
            }
        }

        public static void FindAll<T>(this INode node, string name, ICollection<T> result) where T : INode
        {
            if (node.Name == name) result.Add((T)node);
            for (int i = 0; i < node.Children.Count; i++)
            {
                FindAll<T>(node.Children[i], name, result);
            }
        }

        #endregion

        public static void Flatten(INode node, ICollection<INode> result)
        {
            result.Add(node);
            for (int i = 0; i < node.Children.Count; i++)
            {
                Flatten(node.Children[i], result);
            }
        }

        public static INode GetRoot(INode node)
        {
            if (node.Parent == null) return node;
            return GetRoot(node.Parent);
        }

        public static INode HitTest(INode node, Vector2 position)
        {
            // Iterates through the children in reverse order,
            // to hit test the front most node first.
            for (int i = node.Children.Count - 1; i >= 0; i--)
            {
                INode result = HitTest(node.Children[i], position);
                if (result != null) return result;
            }

            if (node.HitTest(position)) return node;

            return null;
        }

        public static BoundingBox2 GetBounds(INode node)
        {
            BoundingBox2 bounds = node.GetWorldBounds();
            for (int i = 0; i < node.Children.Count; i++)
            {
                bounds = bounds.Union(GetBounds(node.Children[i]));
            }
            return bounds;
        }

        public static INode Clone(INode node)
        {
            INode clone = node.Clone();
            for (int i = 0; i < node.Children.Count; i++)
            {
                clone.Children.Add(Clone(node.Children[i]));
            }
            return clone;
        }

        public static T Clone<T>(T node) where T : INode
        {
            INode clone = node.Clone();
            for (int i = 0; i < node.Children.Count; i++)
            {
                clone.Children.Add(Clone(node.Children[i]));
            }
            return (T)clone;
        }
    }
}
