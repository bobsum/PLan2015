using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bismuth.Framework.Composite;

namespace Bismuth.Framework.Pins
{
    public static class PinTree
    {
        public static void Bind(INode node)
        {
            Bind(node, node);
        }

        public static void Bind(INode node, INode root)
        {
            IPin pin = node as IPin;
            if (pin != null && !string.IsNullOrEmpty(pin.TargetName))
            {
                INode target = NodeTree.Find(root, pin.TargetName);
                pin.Bind(target);
            }

            for (int i = 0; i < node.Children.Count; i++)
            {
                Bind(node.Children[i], root);
            }
        }

        public static void UpdateTarget(INode node)
        {
            IPin pin = node as IPin;
            if (pin != null)
            {
                pin.UpdateTarget();
            }

            for (int i = 0; i < node.Children.Count; i++)
            {
                UpdateTarget(node.Children[i]);
            }
        }
    }
}
